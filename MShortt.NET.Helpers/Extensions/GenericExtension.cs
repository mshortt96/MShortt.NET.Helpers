using MShortt.NET.Helpers.Data;
using MShortt.NET.Helpers.Internal;
using System;

namespace MShortt.NET.Helpers.Extensions
{
    public static class GenericExtension
    {
        /// <summary>
        ///     By default, indicates whether the object has at least one instance field or auto-implemented property getter 
        ///     of any accessibility with a value. The evaluation behaviour of the check can be tweaked by providing
        ///     <see cref="ObjectPopulationCheckOptions"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsPopulated<T>(this T obj, ObjectPopulationCheckOptions options = null)
        {
            ObjectPopulationChecker checker = new ObjectPopulationChecker(options);
            return checker.Check(obj);
        }
    }
}
