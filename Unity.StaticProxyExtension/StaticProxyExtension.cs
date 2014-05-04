namespace Unity.StaticProxyExtension
{
    using System.Collections.ObjectModel;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.ObjectBuilder;

    public class StaticProxyExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            this.Context.Strategies.AddNew<StaticProxyInterceptorStrategy>(UnityBuildStage.PreCreation);

            this.Container.RegisterType<IDynamicInterceptorManager, DynamicInterceptorManager>();

            this.Container.RegisterInstance(
                typeof(IDynamicInterceptorCollection),
                new DynamicInterceptorCollection(new Collection<IDynamicInterceptor>()));
        }
    }
}