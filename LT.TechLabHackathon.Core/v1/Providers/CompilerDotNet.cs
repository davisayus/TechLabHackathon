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
    public class CompilerDotNet
    {

        public async Task<string> Compile(ChallengeDto challenge, string code)
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

                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error);

                    return string.Join(Environment.NewLine, failures.Select(f => f.GetMessage()));
                }
                else
                {
                    var assembly = Assembly.Load(ms.ToArray());
                    var methodName = challenge.MethodName;
                    var entryPoint = assembly.GetType("Program")?.GetMethod(methodName);
                    string resultCompilation = string.Empty;

                    if (entryPoint != null)
                    {
                        foreach (var validation in challenge.Validations)
                        {
                            var parameters = validation.InputParameters.OrderBy(ip => ip.Sequence).Select(ip => ip.InputValue as object).ToArray();
                            var output = entryPoint.Invoke(null, parameters);
                            resultCompilation += string.Join(Environment.NewLine, $"Execution {validation.ValidationId} input: ({string.Join(",", parameters)}): Result: {output}\n");
                        }

                        return resultCompilation;
                    }
                    else
                    {
                        return $"The method '{methodName}' was not found in the provided code.";
                    }

                    //// El código se compiló correctamente
                    //return "El código C# proporcionado es válido.";
                }
            }
            catch (Exception ex)
            {
                // Capturar excepciones inesperadas y devolver un error interno del servidor
                return $"Error interno del servidor: {ex.Message}";
                
            }
        }
    }
}
