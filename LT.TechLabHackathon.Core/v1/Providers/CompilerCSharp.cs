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

namespace LT.TechLabHackathon.Core.v1.Providers
{
    public class CompilerCSharp
    {

        public static object ExecuteCSharpCode(string code, object[] parameters, int timeoutMilliseconds)
        {
            // Configurar el compilador C#
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.GenerateInMemory = true;
            compilerParams.GenerateExecutable = false;

            // Agregar las referencias necesarias
            compilerParams.ReferencedAssemblies.Add("System.dll");
            compilerParams.ReferencedAssemblies.Add("mscorlib.dll");

            // Compilar el código C#
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, code);

            // Verificar si hay errores de compilación
            if (results.Errors.HasErrors)
            {
                throw new Exception("Error de compilación en el código proporcionado.");
            }

            // Obtener el ensamblado compilado
            Assembly assembly = results.CompiledAssembly;

            // Obtener la clase "Program" y su método "Main"
            Type programType = assembly.GetType("Program");
            MethodInfo methodInfo = programType.GetMethod("Main");

            // Crear una instancia del método en un dominio de aplicación seguro
            //AppDomainSetup domainSetup = new AppDomainSetup();
            //PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
            AppDomain appDomain = AppDomain.CreateDomain("DynamicCodeExecutionDomain");

            
            // Ejecutar el método en un hilo separado para evitar bloqueos
            object result = null;
            Exception exception = null;
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {
                try
                {
                    result = methodInfo.Invoke(null, parameters);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });

            // Obtener el uso de memoria antes de la ejecución
            long memoryBefore = Process.GetCurrentProcess().WorkingSet64;

            thread.Start();

            // Esperar hasta que el hilo termine o hasta que se alcance el tiempo máximo
            if (!thread.Join(timeoutMilliseconds))
            {
                thread.Abort();
                throw new TimeoutException("Tiempo de ejecución excedido.");
            }

            // Obtener el uso de memoria después de la ejecución
            long memoryAfter = Process.GetCurrentProcess().WorkingSet64;

            // Si hubo una excepción durante la ejecución, lanzarla
            if (exception != null)
            {
                throw exception;
            }

            // Retornar el resultado de la ejecución y el uso de memoria
            return new Tuple<object, long>(result, memoryAfter - memoryBefore);
        }

    }
}
