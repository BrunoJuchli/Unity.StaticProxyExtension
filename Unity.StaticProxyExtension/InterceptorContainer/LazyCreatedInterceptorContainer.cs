namespace Unity.StaticProxyExtension.InterceptorContainer
{
    using System;

    using Microsoft.Practices.ObjectBuilder2;

    internal class LazyCreatedInterceptorContainer : IInterceptorContainer
    {
        private readonly Type interceptorType;

        public LazyCreatedInterceptorContainer(Type interceptorType)
        {
            this.interceptorType = interceptorType;
        }

        public IDynamicInterceptor Retrieve(IBuilderContext builderContext)
        {
            return (IDynamicInterceptor)builderContext.NewBuildUp(new NamedTypeBuildKey(this.interceptorType, builderContext.BuildKey.Name));
        }
    }
}