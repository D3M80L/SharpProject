using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patterns.IoC.Unity.Infrastructure;

namespace Patterns.IoC.Unity
{
    public sealed class UnityContainerRegistration : IContainerRegistration
    {
        private readonly IContainer _mainContainer = new UnityContainerWrapper();

        public void Register()
        {
            ContainerFactory.Register(BuildUnityContainer);
        }

        private IContainer BuildUnityContainer()
        {
            return _mainContainer;
        }
    }
}
