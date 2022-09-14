using MShortt.NET.Helpers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MShortt.NET.Helpers.Internal
{
    internal class ObjectPopulationChecker
    {
        private readonly ObjectPopulationCheckOptions options;

        internal ObjectPopulationChecker(ObjectPopulationCheckOptions options = null)
        {
            this.options = options ?? new ObjectPopulationCheckOptions();
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal bool Check<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            if (HasPopulatedFields(obj, bindingFlags))
            {
                return true;
            }

            else
            {
                return HasPopulatedAutoGetterProperties(obj, bindingFlags);
            }
        }

        private bool HasPopulatedFields<T>(T obj, BindingFlags bindingFlags)
        {
            return typeof(T)
                .GetFields(bindingFlags)
                .Where(x => HasSpecifiedAccessibility(x, options.FieldAccessibility) && HasValue(x.GetValue(obj)))
                .Any();
        }

        private bool HasPopulatedAutoGetterProperties<T>(T obj, BindingFlags bindingFlags)
        {
            return typeof(T)
                .GetProperties(bindingFlags)
                .Where(x =>
                {
                    bool hasAutoGetter = x.GetMethod.CustomAttributes.Any(y => y.AttributeType == typeof(CompilerGeneratedAttribute));
                    return hasAutoGetter
                        ? HasSpecifiedAccessibility(x.GetMethod, options.AutoPropertyGetterAccessibility) && HasValue(x.GetValue(obj)) 
                        : false;
                })
                .Any();
        }

        //Using reflection to invoke the access evaluation properties as FieldInfo, MethodBase and PropertyInfo don't inherit them from a common ancestor.
        //While this has a performance impact and could be considered a bit dodgy, from a maintainability standpoint it allows us to have this single method
        //for each MemberInfo derivative the class works with, instead of multiple, near-identical overloads.
        private bool HasSpecifiedAccessibility(MemberInfo memberInfo, PopulationCheckMemberAccessibilityOption accessibility)
        {
            if (accessibility is PopulationCheckMemberAccessibilityOption.All)
            {
                return true;
            }

            else
            {
                Type memberInfoType = memberInfo.GetType();

                if (accessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Public) && (bool)memberInfoType.GetProperty("IsPublic").GetValue(memberInfo))
                {
                    return true;
                }

                if (accessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Private) && (bool)memberInfoType.GetProperty("IsPrivate").GetValue(memberInfo))
                {
                    return true;
                }

                if (accessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Protected) && (bool)memberInfoType.GetProperty("IsFamily").GetValue(memberInfo))
                {
                    return true;
                }

                if (accessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Internal) && (bool)memberInfoType.GetProperty("IsAssembly").GetValue(memberInfo))
                {
                    return true;
                }

                if (accessibility.HasFlag(PopulationCheckMemberAccessibilityOption.ProtectedInternal) && (bool)memberInfoType.GetProperty("IsFamilyOrAssembly").GetValue(memberInfo))
                {
                    return true;
                }

                if (accessibility.HasFlag(PopulationCheckMemberAccessibilityOption.PrivateProtected) && (bool)memberInfoType.GetProperty("IsFamilyAndAssembly").GetValue(memberInfo))
                {
                    return true;
                }

                return false;
            }
        }

        private bool HasValue(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if(obj is string)
            {
                return StringHasValue(obj as string);
            }

            if(obj is IEnumerable<object>)
            {
                return CollectionHasValue(obj as IEnumerable<object>);
            }

            return true;
        }

        private bool StringHasValue(string stringValue)
        {
            switch (options.Strings)
            {
                case PopulationCheckStringOption.NullOnlyCountsTowardsUnpopulated:
                    return stringValue != null;

                case PopulationCheckStringOption.EmptyCountsTowardsUnpopulated:
                    return !string.IsNullOrEmpty(stringValue);

                case PopulationCheckStringOption.EmptyWhiteSpaceCountsTowardsUnpopulated:
                    return !string.IsNullOrWhiteSpace(stringValue);

                default:
                    throw new InvalidOperationException($@"The {nameof(PopulationCheckStringOption)} value ""{options.Strings}"" is currently not supported.");
            }
        }

        private bool CollectionHasValue(IEnumerable<object> collection)
        {
            return collection != null && options.EmptyCollectionsCountTowardsUnpopulated 
                ? collection.Any() 
                : false;
        }
    }
}
