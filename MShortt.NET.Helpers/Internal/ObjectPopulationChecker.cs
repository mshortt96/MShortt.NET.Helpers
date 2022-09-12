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
                return HasPopulatedProperties(obj, bindingFlags);
            }
        }

        private bool HasPopulatedFields<T>(T obj, BindingFlags bindingFlags)
        {
            return typeof(T)
                .GetFields(bindingFlags)
                .Where(x => HasSpecifiedAccessibility(x) && HasValue(x.GetValue(obj)))
                .Any();
        }

        private bool HasPopulatedProperties<T>(T obj, BindingFlags bindingFlags)
        {
            return typeof(T)
                .GetProperties(bindingFlags)
                .Where(x =>
                {
                    bool hasAutoGetter = x.GetMethod.CustomAttributes.Any(y => y.AttributeType == typeof(CompilerGeneratedAttribute));
                    return hasAutoGetter
                        ? HasSpecifiedAccessibility(x) && HasValue(x.GetValue(obj)) 
                        : false;
                })
                .Any();
        }


        private bool HasSpecifiedAccessibility(FieldInfo field)
        {
            if(options.FieldAccessibility is PopulationCheckMemberAccessibilityOption.All)
            {
                return true;
            }

            if (options.FieldAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Public) && field.IsPublic)
            {
                return true;
            }

            if (options.FieldAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Private) && field.IsPrivate)
            {
                return true;
            }

            if (options.FieldAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Protected) && field.IsFamily)
            {
                return true;
            }

            if (options.FieldAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Internal) && field.IsAssembly)
            {
                return true;
            }

            if(options.FieldAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.ProtectedInternal) && field.IsFamilyOrAssembly)
            {
                return true;
            }

            if (options.FieldAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.PrivateProtected) && field.IsFamilyAndAssembly)
            {
                return true;
            }

            return false;
        }

        private bool HasSpecifiedAccessibility(PropertyInfo property)
        {
            if (options.AutoPropertyGetterAccessibility is PopulationCheckMemberAccessibilityOption.All)
            {
                return true;
            }

            if (options.AutoPropertyGetterAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Public) && property.GetMethod.IsPublic)
            {
                return true;
            }

            if (options.AutoPropertyGetterAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Private) && property.GetMethod.IsPrivate)
            {
                return true;
            }

            if (options.AutoPropertyGetterAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Protected) && property.GetMethod.IsFamily)
            {
                return true;
            }

            if (options.AutoPropertyGetterAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.Internal) && property.GetMethod.IsAssembly)
            {
                return true;
            }

            if (options.AutoPropertyGetterAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.ProtectedInternal) && property.GetMethod.IsFamilyOrAssembly)
            {
                return true;
            }

            if (options.AutoPropertyGetterAccessibility.HasFlag(PopulationCheckMemberAccessibilityOption.PrivateProtected) && property.GetMethod.IsFamilyAndAssembly)
            {
                return true;
            }

            return false;
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
