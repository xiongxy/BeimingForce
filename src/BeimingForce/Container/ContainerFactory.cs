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
            IContainer container = new ContainerDefault(DefaultContainerName);
            Containers.TryAdd(DefaultContainerName, container);
        }

        /// <summary>
        /// 创建容器于缓存中
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="isCover"></param>
        /// <returns></returns>
        public static IContainer CreateContainer(string containerName = DefaultContainerName, bool isCover = false)
        {
            IContainer container;
            if (Containers.ContainsKey(containerName))
            {
                if (isCover)
                {
                    container = new ContainerDefault(containerName);
                    DeleteContainer(containerName);
                    Containers.TryAdd(containerName, container);
                }
                else
                {
                    Containers.TryGetValue(containerName, out container);
                }
            }
            else
            {
                container = new ContainerDefault(containerName);
                Containers.TryAdd(containerName, container);
            }
            return container;
        }


        /// <summary>
        /// 创建一个全新的容器(不参与缓存)
        /// </summary>
        /// <returns></returns>
        public static IContainer NewContainer()
        {
            return new ContainerDefault("NoCache");
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