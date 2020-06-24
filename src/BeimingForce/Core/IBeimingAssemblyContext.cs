using System.Reflection;

namespace BeimingForce.Core
{
    internal interface IBeimingAssemblyContext
    {
        protected Assembly Load(AssemblyName assemblyName);
    }
}