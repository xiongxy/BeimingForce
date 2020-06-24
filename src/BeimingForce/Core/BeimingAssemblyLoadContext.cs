using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;

namespace BeimingForce.Core
{
    public class BeimingAssemblyLoadContext : AssemblyLoadContext, IBeimingAssemblyContext
    {
        public BeimingAssemblyLoadContext()
        {
            
        }

        Assembly IBeimingAssemblyContext.Load(AssemblyName assemblyName)
        {
            throw new System.NotImplementedException();
        }
    }
}