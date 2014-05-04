namespace Integration.ClassProxy
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Microsoft.Practices.Unity;

    using Unity.StaticProxyExtension;

    using Xunit;

    public class When_there_are_multiple_interceptors_for_proxy : ContainerTestBase
    {
        [Fact]
        public void MustUseInterceptorsInCorrectOrder()
        {
            var interceptionCallLog = new List<IDynamicInterceptor>();
            var interceptor1 = new TraceInterceptor(interceptionCallLog, "1");
            var interceptor2 = new TraceInterceptor(interceptionCallLog, "2");
            var interceptor3 = new TraceInterceptor(interceptionCallLog, "3");

            this.Container.RegisterType<InterceptedTarget, InterceptedTarget>(
                new Intercept(interceptor1),
                new Intercept(interceptor2),
                new Intercept(interceptor3));

            var proxy = this.Container.Resolve<InterceptedTarget>();

            proxy.Bar();

            interceptionCallLog.Should()
                .ContainInOrder(interceptor1, interceptor2, interceptor3)
                .And.HaveCount(3);
        }
    }
}