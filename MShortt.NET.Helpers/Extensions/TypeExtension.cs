using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Extensions
{
    public static class TypeExtension
    {
        /// <summary>Indicates if the Type is a concrete class; that is, a class that is not marked as abstract.</summary>
        public static bool IsConcreteClass(this Type type) => type.IsClass && !type.IsAbstract;

        /// <summary>
        ///     Traverses the Type's inheritance tree and returns all of its parent Types, optionally filtering out interfaces and/or Types
        ///     that do match a given predicate.
        /// </summary>
        /// <returns>A collection of the Type's parents, or an empty collection if none were found.</returns>
        public static IEnumerable<Type> GetInheritanceTree(this Type type, bool excludeInterfaces = false, Func<Type, bool> predicate = null)
        {
            List<Type> parents = new List<Type>();

            if (!excludeInterfaces)
            {
                IEnumerable<Type> interfaces = type.GetInterfaces();

                if(interfaces.Any() && predicate != null)
                    interfaces = interfaces.Where(predicate);

                if(interfaces.Any())
                    parents.AddRange(interfaces);
            }

            for(Type superclass = type.BaseType; superclass != null; superclass = superclass.BaseType)
            {
                if (predicate != null && predicate.Invoke(superclass) == false)
                    continue;

                parents.Add(superclass);
            }

            return parents;
        }
    }
}
