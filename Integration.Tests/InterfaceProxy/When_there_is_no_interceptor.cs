namespace Integration.InterfaceProxy
{
    using System;

    using FluentAssertions;

    using Microsoft.Practices.Unity;

    using StaticProxy.Interceptor.InterfaceProxy;

    using Unity.StaticProxyExtension;

    using Xunit;

    public class When_there_is_no_interceptor : ContainerTestBase
    {
        [Fact]
        public void Registration_MustThrow()
        {
            this.Container.Invoking(x => x.RegisterInterfaceProxy<IProxy>())
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Instanciating_MustThrow()
        {
            Type interfaceType = typeof(IProxy);
            Type implementationType = InterfaceProxyHelpers.GetImplementationTypeOfInterface(interfaceType);

            this.Container.RegisterType(interfaceType, implementationType);

            this.Container.Invoking(x => x.Resolve<IProxy>())
                .ShouldThrow<ResolutionFailedException>()
                .WithInnerException<InvalidOperationException>();
        }
    }
}