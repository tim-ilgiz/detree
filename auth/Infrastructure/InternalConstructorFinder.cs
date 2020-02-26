using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac.Core.Activators.Reflection;

namespace Infrastructure
{
    public class InternalConstructorFinder : IConstructorFinder
    {
        public ConstructorInfo[] FindConstructors(Type t) => t.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsPrivate && !c.IsPublic).ToArray();
    }
}
