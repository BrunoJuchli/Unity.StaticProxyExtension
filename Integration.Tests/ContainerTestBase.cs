namespace Integration
{
    using System;

    using Microsoft.Practices.Unity;

    using Unity.StaticProxyExtension;

    public class ContainerTestBase : IDisposable
    {
        protected readonly UnityContainer Container;

        public ContainerTestBase()
        {
            this.Container = new UnityContainer();
            this.Container.AddNewExtension<StaticProxyExtension>();
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}