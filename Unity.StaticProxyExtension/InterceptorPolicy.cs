namespace Unity.StaticProxyExtension
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Microsoft.Practices.ObjectBuilder2;

    using Unity.StaticProxyExtension.InterceptorContainer;

    internal class InterceptorPolicy : IBuilderPolicy
    {
        private readonly ICollection<IInterceptorContainer> interceptors;

        public InterceptorPolicy()
        {
            this.interceptors = new Collection<IInterceptorContainer>();
        }

        public void AddInterceptor(Type interceptorType)
        {
            this.interceptors.Add(new LazyCreatedInterceptorContainer(interceptorType));
        }

        public void AddInterceptor(IDynamicInterceptor interceptorInstance)
        {
            this.interceptors.Add(new ConstantInterceptorContainer(interceptorInstance));
        }

        public void AddInterceptor(IInterceptorContainer interceptorContainer)
        {
            this.interceptors.Add(interceptorContainer);
        }

        public ICollection<IDynamicInterceptor> RetrieveInterceptors(IBuilderContext builderContext)
        {
            return this.interceptors
                .Select(x => x.Retrieve(builderContext))
                .ToList();
        }
    }
}