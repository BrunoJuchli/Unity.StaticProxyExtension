namespace Integration.InterfaceProxy
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Microsoft.Practices.Unity;
    using Moq;
    using Unity.StaticProxyExtension;
    using Xunit;

    public class When_there_are_multiple_interceptors : ContainerTestBase
    {
        [Fact]
        public void Instanciating_ShouldNotThrow()
        {
            this.Container.RegisterInterfaceProxy<IProxy>(
                new Intercept(Mock.Of<IDynamicInterceptor>()),
                new Intercept(Mock.Of<IDynamicInterceptor>()));

            this.Container.Invoking(x => x.Resolve<IProxy>())
                .ShouldNotThrow();
        }

        [Fact]
        public void MethodCall_MustUseInterceptorsInCorrectSequence()
        {
            var interceptionCallLog = new List<IDynamicInterceptor>();
            var interceptor1 = new TraceInterceptor(interceptionCallLog, "1");
            var interceptor2 = new TraceInterceptor(interceptionCallLog, "2");
            var interceptor3 = new TraceInterceptor(interceptionCallLog, "3");

            this.Container.RegisterInterfaceProxy<IProxy>(
                new Intercept(interceptor1),
                new Intercept(interceptor2),
                new Intercept(interceptor3));

            this.Container.Resolve<IProxy>().Foo();

            interceptionCallLog.Should()
                           .ContainInOrder(interceptor1, interceptor2, interceptor3)
                           .And.HaveCount(3);
        }
    }
}