using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Core.v1.Contracts
{
    public interface ICompiler
    {
        Task<string> CompileAsync();
    }
}
