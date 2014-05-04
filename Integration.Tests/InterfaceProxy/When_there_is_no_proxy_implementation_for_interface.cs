namespace Integration.InterfaceProxy
{
    using System;
    using FluentAssertions;
    using Moq;
    using Unity.StaticProxyExtension;
    using Xunit;

    public class When_there_is_no_proxy_implementation_for_interface : ContainerTestBase
    {
        [Fact]
        public void Registration_MustThrow()
        {
            this.Container
                .Invoking(x => x.RegisterInterfaceProxy<INoProxy>(new Intercept(Mock.Of<IDynamicInterceptor>())))
                .ShouldThrow<InvalidOperationException>()
                .Where(ex => ex.Message.Contains("[StaticProxy]")); // or maybe we could introduce a custom exception so we don't have to check the message's to make sure it's actually the "right" error case.
        } 
    }
}