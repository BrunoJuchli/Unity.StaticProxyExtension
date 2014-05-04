namespace Unity.StaticProxyExtension.InterceptorContainer
{
    using Microsoft.Practices.ObjectBuilder2;

    internal class ConstantInterceptorContainer : IInterceptorContainer
    {
        private readonly IDynamicInterceptor instance;

        public ConstantInterceptorContainer(IDynamicInterceptor instance)
        {
            this.instance = instance;
        }

        public IDynamicInterceptor Retrieve(IBuilderContext builderContext)
        {
            return this.instance;
        }
    }
}