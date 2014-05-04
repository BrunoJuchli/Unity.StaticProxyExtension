namespace Unity.StaticProxyExtension.InterceptorContainer
{
    using Microsoft.Practices.ObjectBuilder2;

    internal interface IInterceptorContainer
    {
        IDynamicInterceptor Retrieve(IBuilderContext builderContext);
    }
}