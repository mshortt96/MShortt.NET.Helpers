using System;

namespace MShortt.NET.Helpers.Extensions
{
    public static class TypeExtension
    {
        /// <summary>Indicates if the Type is a concrete class; that is, a class that is not marked as abstract.</summary>
        public static bool IsConcreteClass(this Type type) => type.IsClass && !type.IsAbstract;
    }
}
