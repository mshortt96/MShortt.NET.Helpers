using NUnit.Framework;
using System;

namespace MShortt.NET.Helpers.Tests
{
    public abstract class ConstructorTests<T>
    {
        private Type objectType;

        public ConstructorTests()
        {
            objectType = typeof(T);
        }

        protected void AssertInstancePropertyHasCorrectValue(string propertyName, object expectedValue, T objectInstance)
        {
            object value = objectType.GetProperty(propertyName).GetValue(objectInstance);
            Assert.That(value.Equals(expectedValue));
        }
    }
}
