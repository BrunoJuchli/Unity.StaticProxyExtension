namespace Unity.StaticProxyExtension
{
    using System.Collections;
    using System.Collections.Generic;

    internal class DynamicInterceptorCollection : IDynamicInterceptorCollection
    {
        private readonly ICollection<IDynamicInterceptor> interceptors;

        public DynamicInterceptorCollection(ICollection<IDynamicInterceptor> interceptors)
        {
            this.interceptors = interceptors;
        }

        public IEnumerator<IDynamicInterceptor> GetEnumerator()
        {
            return this.interceptors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}