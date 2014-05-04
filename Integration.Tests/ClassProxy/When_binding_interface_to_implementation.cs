namespace Integration.ClassProxy
{
    using Microsoft.Practices.Unity;
    using Moq;
    using Unity.StaticProxyExtension;
    using Xunit;

    public class When_binding_interface_to_implementation : ContainerTestBase
    {
        [Fact]
        public void Must_use_interceptor()
        {
            this.Container.RegisterType<IInterceptedTarget, InterceptedTarget>(new Intercept<ForwardToMockInterceptor>());

            var interceptor = new ForwardToMockInterceptor();
            this.Container.RegisterInstance(typeof(ForwardToMockInterceptor), interceptor);

            interceptor.Mock
                .Setup(x => x.Intercept(It.IsAny<IInvocation>()))
                .Callback<IInvocation>(invocation => invocation.Proceed());

            this.Container.Resolve<IInterceptedTarget>().Foo();

            interceptor.Mock.Verify(x => x.Intercept(It.Is<IInvocation>(invocation => invocation.Method.Name == "Foo")));
        }

    }
}