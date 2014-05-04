namespace Unity.StaticProxyExtension
{
    using System.Collections.Generic;

    using Microsoft.Practices.ObjectBuilder2;
    using Microsoft.Practices.Unity;

    internal class StaticProxyInterceptorStrategy : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            var policy = context.Policies.Get<InterceptorPolicy>(context.BuildKey);
            if (policy != null)
            {
                ICollection<IDynamicInterceptor> interceptors = policy.RetrieveInterceptors(context);
                var dynamicInterceptorManager = new DynamicInterceptorManager(new DynamicInterceptorCollection(interceptors));
                context.AddResolverOverrides(new DependencyOverride<IDynamicInterceptorManager>(dynamicInterceptorManager));
            }
        }
    }
}