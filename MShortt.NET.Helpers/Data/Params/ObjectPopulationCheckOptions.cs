using System;

namespace MShortt.NET.Helpers.Data
{
    public class ObjectPopulationCheckOptions
    {
        /// <summary>Specifies how the population check should evaluate the object's string members.</summary>
        public PopulationCheckStringOption Strings { get; set; }

        /// <summary>Specifies whether the population check should count initialized, empty collections towards the object being unpopulated.</summary>
        public bool EmptyCollectionsCountTowardsUnpopulated { get; set; }

        /// <summary>Specifies, based on access level, which fields the population check should evaluate.</summary>
        public PopulationCheckMemberAccessibilityOption FieldAccessibility { get; set; }

        /// <summary>Specifies, based on access level, which auto-implemented property getters the population check should evaluate.</summary>
        public PopulationCheckMemberAccessibilityOption AutoPropertyGetterAccessibility { get; set; }
    }

    public enum PopulationCheckStringOption
    {
        /// <summary>Specifies that the population check should only count uninitialized strings towards the object being unpopulated.</summary>
        NullOnlyCountsTowardsUnpopulated = 0,

        /// <summary>Specifies that the population check should count empty strings towards the object being unpopulated.</summary>
        EmptyCountsTowardsUnpopulated,

        /// <summary>Specifies that the population check should count empty and white space strings towards the object being unpopulated.</summary>
        EmptyWhiteSpaceCountsTowardsUnpopulated
    }

    [Flags]
    public enum PopulationCheckMemberAccessibilityOption
    {
        /// <summary>Specifies that the population check should check members of all accessibility levels.</summary>
        All = 0,

        /// <summary>Specifies that the population check should only check members flagged as public.</summary>
        Public = 1,

        /// <summary>Specifies that the population check should only check members flagged as private.</summary>
        Private = 2,

        /// <summary>Specifies that the population check should only check members flagged as protected.</summary>
        Protected = 4,

        /// <summary>Specifies that the population check should only check members flagged as internal.</summary>
        Internal = 8,

        /// <summary>Specifies that the population check should only check members flagged as protected internal.</summary>
        ProtectedInternal = 16,

        /// <summary>Specifies that the population check should only check members flagged as private protected.</summary>
        PrivateProtected = 32
    }
}
