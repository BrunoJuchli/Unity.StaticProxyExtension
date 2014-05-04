namespace Integration.ClassProxy
{
    using FluentAssertions;

    using Microsoft.Practices.Unity;

    using Xunit;

    public class When_there_is_no_interceptor_for_proxy : ContainerTestBase
    {
        [Fact]
        public void Must_execute_original_method()
        {
            const int ExpectedValue = 583;

            this.Container.RegisterType<IInterceptedTarget, InterceptedTarget>();

            var proxy = this.Container.Resolve<IInterceptedTarget>();

            proxy.FooReturnValue = ExpectedValue;

            int actualValue = proxy.Foo();

            actualValue.Should().Be(ExpectedValue);
        }
    }
}
