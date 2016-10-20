using System.Linq;
using NUnit.Framework;

namespace StationSearch.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test_With_Results()
        {
            var ts = new StationSearchClass(new string[] { "DARTFORD", "DARTMOUTH", "TOWER HILL", "DERBY" });
            var result = ts.Search("DART");
            Assert.NotNull(result);
            Assert.NotNull(result.NextChars);
            Assert.NotNull(result.Stations);
            Assert.AreEqual(2, result.NextChars.Length);
            Assert.AreEqual(2, result.Stations.Length);
            Assert.True((new char[] { 'F', 'M' }).All(result.NextChars.Contains));
            Assert.True((new string[] { "DARTFORD", "DARTMOUTH" }).All(result.Stations.Contains));
        }

        [Test]
        public void Test_With_Results_Space()
        {
            var ts = new StationSearchClass(new string[] { "LIVERPOOL", "LIVERPOOL LIME STREET", "PADDINGTON" });
            var result = ts.Search("LIVERPOOL");
            Assert.NotNull(result);
            Assert.NotNull(result.NextChars);
            Assert.NotNull(result.Stations);
            Assert.AreEqual(1, result.NextChars.Length);
            Assert.AreEqual(2, result.Stations.Length);
            Assert.True((new char[] { ' ' }).All(result.NextChars.Contains));
            Assert.True((new string[] { "LIVERPOOL", "LIVERPOOL LIME STREET" }).All(result.Stations.Contains));
        }

        [Test]
        public void Test_With_No_Results()
        {
            var ts = new StationSearchClass(new string[] { "EUSTON", "LONDON BRIDGE", "VICTORIA" });
            var result = ts.Search("KINGS CROSS");
            Assert.NotNull(result);
            Assert.NotNull(result.NextChars);
            Assert.NotNull(result.Stations);
            Assert.AreEqual(0, result.NextChars.Length);
            Assert.AreEqual(0, result.Stations.Length);
        }

        [Test]
        public void Test_With_Results_Long()
        {
            var ts = new StationSearchClass(new AllStations().Stations);
            var result = ts.Search("N");
            Assert.NotNull(result);
            Assert.NotNull(result.NextChars);
            Assert.NotNull(result.Stations);
            Assert.True(result.Stations.Length > 0);
            Assert.True(result.NextChars.Length > 0);
        }
    }
}
