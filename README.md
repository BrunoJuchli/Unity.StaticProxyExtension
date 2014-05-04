[![Build status](https://ci.appveyor.com/api/projects/status/78rnapve4bny7js1)](https://ci.appveyor.com/project/BrunoJuchli/unity-staticproxyextension)

Unity.StaticProxyExtension
==========================

Proxying / Interception for Unity IoC by means of IL Weaving. Due to il weaving, it also supports Windows Store and MonoTouch platforms.

## Nuget

Nuget package http://nuget.org/packages/Unity.StaticProxy

To Install the static proxy weaver from the Nuget Package Manager Console 
    
    PM> Install-Package Unity.StaticProxy

## Usage

### Setup
 - install the nuget package in the application project
 - install nuget package StaticProxy.Fody in every project where types should be proxied
 - install nuget package StaticProxy.Interceptor in every project where you want to write interceptors.

### Configuring Class Proxy Interception
this is similar to castle dynamic proxy's "class proxy" and "interface proxy with target" proxy types.

 - put a `[StaticProxy]` attribute on the class
 - Write some interceptors (`FooInterceptor : IDynamicInterceptor`) - this is almost idential to Castle Dynamic Proxy Interceptors.
 - configure the unity container:

         var container = new UnityContainer();
         container.AddNewExtension<StaticProxyExtension>();
         container.RegisterType<IFoo, Foo>(new Intercept<LoggingInterceptor>(), new Intercept(new MyInterceptor());
                

### Configuring Interface Proxy Interception
this is similar to castle dynamic proxy's "interface proxy without target" proxy type. Requires at least one interceptor!

 - put a `[StaticProxy]` attribute on the interface
 - Write some interceptors (`FooInterceptor : IDynamicInterceptor`) - this is almost idential to Castle Dynamic Proxy Interceptors.
 - configure the unity container:
 
        var container = new UnityContainer();
        container.AddNewExtension<StaticProxyExtension>();
        container.RegisterInterfaceProxy<IFoo>(new Intercept<LoggingInterceptor>(), new Intercept(new MyInterceptor());

                
