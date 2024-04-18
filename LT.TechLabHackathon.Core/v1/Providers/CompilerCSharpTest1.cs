using LT.TechLabHackathon.Core.v1.Contracts;
using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Shared.DTOs;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Core.v1.Providers
{
    public class CompilerCSharpTest1 : ICompiler
    {

        public async Task<(IEnumerable<object>, long)> CompileAsync(ChallengeDto challenge, string code)
        {
            try
            {
                //Include method in a class
                var classProgram = "public static class Program { @code }";
                classProgram = classProgram.Replace("@code", code);

                MetadataReference[] references = new MetadataReference[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Collections.Generic.List<char>).Assembly.Location)
                };

                var compilation = CSharpCompilation.Create("DynamicCode")
                    .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                    //.AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                    .AddReferences(references)
                    .AddSyntaxTrees(CSharpSyntaxTree.ParseText(classProgram));

                using var ms = new MemoryStream();
                EmitResult result = compilation.Emit(ms);
                List<object> listResponse = new List<object>();

                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error);

                    var response = string.Join(Environment.NewLine, failures.Select(f => f.GetMessage()));
                    listResponse.Add(response);
                }
                else
                {
                    var assembly = Assembly.Load(ms.ToArray());
                    var methodName = challenge.MethodName;
                    var entryPoint = assembly.GetType("Program")?.GetMethod(methodName);
                    if (entryPoint != null)
                    {
                        foreach (var validation in challenge.Validations)
                        {
                            var parameters = validation.InputParameters.OrderBy(ip => ip.Sequence).Select(ip => ip.InputValue as object).ToArray();
                            var output = entryPoint.Invoke(null, parameters);
                            var resultCompilation = string.Join(Environment.NewLine, $"Execution {validation.ValidationId} input: ({string.Join(",", parameters)}): Result: {output}\n");
                            listResponse.Add(resultCompilation);
                        }
                    }
                    else
                    {
                        throw new Exception($"The method '{methodName}' was not found in the provided code.");
                    }
                }
                return (listResponse, 0);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
