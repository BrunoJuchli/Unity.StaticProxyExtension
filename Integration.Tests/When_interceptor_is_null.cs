namespace Integration
{
    using System;
    using FluentAssertions;
    using Integration.ClassProxy;
    using Microsoft.Practices.Unity;
    using Unity.StaticProxyExtension;
    using Xunit;

    public class When_interceptor_is_null : ContainerTestBase
    {
        [Fact]
        public void WhenTypeIsNull_MustThrow()
        {
            this.Container
                .Invoking(x => x.RegisterType<IInterceptedTarget, IInterceptedTarget>(new Intercept((Type)null)))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void WhenInstanceIsNull_MustThrow()
        {
            this.Container
                .Invoking(x => x.RegisterType<IInterceptedTarget, IInterceptedTarget>(new Intercept((IDynamicInterceptor)null)))
                .ShouldThrow<ArgumentNullException>();
        }
    }
}