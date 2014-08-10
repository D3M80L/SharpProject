using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.IoC
{
    public sealed class ContainerFactory
    {

        private static Func<IContainer> _containerBuilder;

        public static IContainer Get()
        {
            if (_containerBuilder == null)
            {
                throw new InvalidOperationException("Container builder must be registered.");
            }

            return _containerBuilder();
        }

        public static void Register(Func<IContainer> containerBuilder)
        {
            if (containerBuilder == null)
            {
                throw new ArgumentNullException("containerBuilder");
            }

            _containerBuilder = containerBuilder;
        }
    }
}
