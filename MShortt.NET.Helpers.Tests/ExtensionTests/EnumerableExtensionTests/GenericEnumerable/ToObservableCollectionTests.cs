using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.EnumerableExtensionTests.GenericEnumerable;

public class ToObservableCollectionTests : EnumerableTests
{
    [Test]
    public void PreservesElementsTest()
    {
        IEnumerable<int> rawCollection = new int[] { 1, 2, 3 };
        ObservableCollection<int> observable = rawCollection.ToObservableCollection();

        Assert.That(observable.SequenceEqual(rawCollection));
    }
}
