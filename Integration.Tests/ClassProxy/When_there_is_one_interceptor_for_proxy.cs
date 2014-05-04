namespace Integration.ClassProxy
{
    using Microsoft.Practices.Unity;

    using Moq;

    using Unity.StaticProxyExtension;

    using Xunit;

    public class When_there_is_one_interceptor_for_proxy : ContainerTestBase
    {
        [Fact]
        public void Must_use_interceptor()
        {
            var interceptor = new ForwardToMockInterceptor();
            interceptor.Mock
                .Setup(x => x.Intercept(It.IsAny<IInvocation>()))
                .Callback<IInvocation>(invocation => invocation.Proceed());

            this.Container.RegisterType<InterceptedTarget, InterceptedTarget>(new Intercept(interceptor));

            var proxy = this.Container.Resolve<InterceptedTarget>();

            proxy.Foo();

            interceptor.Mock.Verify(x => x.Intercept(It.Is<IInvocation>(invocation => invocation.Method.Name == "Foo")));
        } 
    }
}