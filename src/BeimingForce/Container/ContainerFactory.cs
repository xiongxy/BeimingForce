using System.Collections.Concurrent;
using System.Diagnostics;

namespace BeimingForce.Container
{
    public static class ContainerFactory
    {
        private static readonly ConcurrentDictionary<string, IContainer> Containers;
        private const string DefaultContainerName = "Default";

        static ContainerFactory()
        {
            Containers = new ConcurrentDictionary<string, IContainer>();
            IContainer container = new ContainerDefault("Default");
            Containers.TryAdd(DefaultContainerName, container);
        }

        public static IContainer CreateContainer(string containerName = DefaultContainerName)
        {
            IContainer container = new ContainerDefault(containerName);
            Containers.TryAdd(containerName, container);
            return container;
        }

        public static bool DeleteContainer(string key)
        {
            var containsKey = Containers.ContainsKey(key);
            if (!containsKey) return false;
            Containers.TryRemove(key, out var container);
            container?.Dispose();
            return true;
        }
    }
}