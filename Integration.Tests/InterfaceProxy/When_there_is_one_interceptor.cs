namespace Integration.InterfaceProxy
{
    using System.Reflection;

    using FluentAssertions;

    using Microsoft.Practices.Unity;

    using Moq;

    using Unity.StaticProxyExtension;

    using Xunit;

    public class When_there_is_one_interceptor : ContainerTestBase
    {
        [Fact]
        public void Instanciating_ShouldNotThrow()
        {
            this.Container.RegisterInterfaceProxy<IProxy>(new Intercept(Mock.Of<IDynamicInterceptor>()));

            this.Container.Invoking(x => x.Resolve<IProxy>())
                .ShouldNotThrow();
        }

        [Fact]
        public void MethodCall_MustUseInterceptor()
        {
            var interceptor = new Mock<IDynamicInterceptor>();
            MethodInfo expectedMethod = Reflector<IProxy>.GetMethod(x => x.Foo());

            this.Container.RegisterInterfaceProxy<IProxy>(new Intercept(interceptor.Object));

            this.Container.Resolve<IProxy>().Foo();

            interceptor.Verify(x => x.Intercept(It.Is<IInvocation>(invocation => invocation.Method == expectedMethod)));
        }
    }
}