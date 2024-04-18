using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using LT.TechLabHackathon.Core.v1.Contracts;
using LT.TechLabHackathon.Shared.DTOs;

namespace LT.TechLabHackathon.Core.v1.Providers
{
    public class CompilerCSharp: ICompiler
    {

        public async Task<(IEnumerable<object> ResultList, long MemoryUse)> CompileAsync(ChallengeDto challenge, string code)
        {
            int timeoutMilliseconds = 1000;

            // Configurar el compilador C#
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.GenerateInMemory = true;
            compilerParams.GenerateExecutable = false;

            // add needed references
            compilerParams.ReferencedAssemblies.Add("System.dll");
            compilerParams.ReferencedAssemblies.Add("mscorlib.dll");

            // Compile C# code
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, code);

            if (results.Errors.HasErrors)
                throw new Exception("Compilation error");

            // Obtain the compiled assembly
            Assembly assembly = results.CompiledAssembly;

            // Obtain the class "Program" and its method "method name".
            Type programType = assembly.GetType("Program");
            MethodInfo methodInfo = programType.GetMethod(challenge.MethodName);

            // Create an instance of the method in a secure application domain
            //AppDomainSetup domainSetup = new AppDomainSetup();
            //PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
            AppDomain appDomain = AppDomain.CreateDomain("DynamicCodeExecutionDomain");


            // Execute the method in a separate thread to avoid deadlocks
            List<object> result = [];
            Exception exception = null;
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {
                try
                {
                    foreach (var validation in challenge.Validations)
                    {
                        result.Add(methodInfo.Invoke(null, validation.InputParameters.Select(p => p.InputValue).ToArray()));
                    }
                    
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });

            // Get memory usage before execution
            long memoryBefore = Process.GetCurrentProcess().WorkingSet64;

            thread.Start();

            // Wait until the thread ends or until the maximum time is reached.
            if (!thread.Join(timeoutMilliseconds))
            {
                thread.Abort();
                throw new TimeoutException("Exceeded execution time.");
            }

            // Get memory usage after execution
            long memoryAfter = Process.GetCurrentProcess().WorkingSet64;

            if (exception != null)
                throw exception;

            // Return the result of execution and memory usage
            return (result, (memoryAfter - memoryBefore));
        }

    }
}
