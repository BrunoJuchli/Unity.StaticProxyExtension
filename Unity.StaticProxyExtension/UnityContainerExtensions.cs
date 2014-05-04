namespace Unity.StaticProxyExtension
{
    using System;
    using System.Linq;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;

    using StaticProxy.Interceptor.InterfaceProxy;

    public static class UnityContainerExtensions
    {
        public static IUnityContainer RegisterInterfaceProxy<TInterface>(this IUnityContainer container, params InjectionMember[] injectionMembers)
        {
            Guard.ArgumentNotNull(container, "container");

            return container.RegisterInterfaceProxy<TInterface>(null, injectionMembers);
        }

        public static IUnityContainer RegisterInterfaceProxy<TInterface>(this IUnityContainer container, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            Guard.ArgumentNotNull(container, "container");

            return container.RegisterInterfaceProxy<TInterface>(null, lifetimeManager, injectionMembers);
        }

        public static IUnityContainer RegisterInterfaceProxy<TInterface>(this IUnityContainer container, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            Guard.ArgumentNotNull(container, "container");
            Guard.ArgumentNotNull(injectionMembers, "injectionMembers");

            return container.RegisterInterfaceProxy(typeof(TInterface), name, lifetimeManager, injectionMembers);
        }

        public static IUnityContainer RegisterInterfaceProxy(this IUnityContainer container, Type interfaceType, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            Guard.ArgumentNotNull(interfaceType, "interfaceType");
            Guard.ArgumentNotNull(container, "container");
            Guard.ArgumentNotNull(injectionMembers, "injectionMembers");

            if (!injectionMembers.OfType<Intercept>().Any())
            {
                throw new ArgumentOutOfRangeException(
                    "injectionMembers",
@"An interface proxy requires at least one interceptor to provide it's implementation.
Add an Intercept or Intercept<T> injection member to the registration.");
            }

            Type implementationType = InterfaceProxyHelpers.GetImplementationTypeOfInterface(interfaceType);

            return container.RegisterType(interfaceType, implementationType, name, lifetimeManager, injectionMembers);
        }
    }
}