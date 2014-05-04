namespace Unity.StaticProxyExtension
{
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;

    public static class UnityContainerExtensions
    {
        public static IUnityContainer RegisterInterfaceProxy<TInterface>(this IUnityContainer container, params InjectionMember[] injectionMembers)
        {

            Guard.ArgumentNotNull(container, "container");
            return container.RegisterType(typeof(TFrom), typeof(TTo), null, null, injectionMembers);
        }
    }
}