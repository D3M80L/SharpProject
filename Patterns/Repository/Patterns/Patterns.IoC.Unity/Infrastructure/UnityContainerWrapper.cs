using Microsoft.Practices.Unity;

namespace Patterns.IoC.Unity.Infrastructure
{
    internal sealed class UnityContainerWrapper : IContainer
    {
        private readonly IUnityContainer _unityContainer = new UnityContainer();

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public void Dispose()
        {
            _unityContainer.Dispose();
        }
    }
}