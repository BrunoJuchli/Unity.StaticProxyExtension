namespace Unity.StaticProxyExtension
{
    using System;

    using Microsoft.Practices.ObjectBuilder2;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;

    using Unity.StaticProxyExtension.InterceptorContainer;

    public class Intercept : InjectionMember
    {
        private readonly IInterceptorContainer interceptorContainer;

        public Intercept(Type interceptorType)
        {
            Guard.ArgumentNotNull(interceptorType, "interceptorType");

            this.interceptorContainer = new LazyCreatedInterceptorContainer(interceptorType);
        }

        public Intercept(IDynamicInterceptor interceptor)
        {
            Guard.ArgumentNotNull(interceptor, "interceptor");

            this.interceptorContainer = new ConstantInterceptorContainer(interceptor);
        }

        public override void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies)
        {
            var key = new NamedTypeBuildKey(implementationType, name);
            var policy = policies.Get<InterceptorPolicy>(key);
            if (policy == null)
            {
                policy = new InterceptorPolicy();
                policies.Set(policy, key);
            }
            
            policy.AddInterceptor(this.interceptorContainer);
        }
    }

    public class Intercept<TInterceptor> : Intercept
        where TInterceptor : IDynamicInterceptor
    {
        public Intercept()
            : base(typeof(TInterceptor))
        {
        }
    }
}