using System.Reflection;
using BeimingForce.Model;

namespace BeimingForce.Engine
{
    public interface IAssemblyProcess<T>
    {
        T LoadNameSpaces(string[] nameSpaces);
        T LoadAssembly(Assembly[] assemblies);
        T LoadAssembly(string[] assemblyNames);
        T BuildDynamicScript();
    }
}
