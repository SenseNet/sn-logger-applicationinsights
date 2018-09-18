using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SenseNet.Logging.ApplicationInsights.Tests
{
    internal class TestPropertyHolder : ISupportProperties
    {
        public IDictionary<string, string> Properties { get; } = new Dictionary<string, string>();
    }

    [TestClass]
    public class PropertyTests
    {
        [TestMethod]
        public void Properties_Common()
        {
            var props = new TestPropertyHolder();
            var categories = new List<string>{ "c1", "c2", "c3" };
            var originalProps = new Dictionary<string, object>
            {
                { "p1", 42 },
                { "p2", "value2" }
            };

            ApplicationInsightsLogger.AddProperties(props, "mymessage", categories, 123, "mytitle", originalProps);

            // well-known properties
            Assert.AreEqual("mymessage", props.Properties["Message"]);
            Assert.AreEqual("mytitle", props.Properties["Title"]);
            Assert.AreEqual("123", props.Properties["EventId"]);
            Assert.AreEqual("c1, c2, c3", props.Properties["Categories"]);

            // dynamic properties
            Assert.AreEqual("42", props.Properties["p1"]);
            Assert.AreEqual("value2", props.Properties["p2"]);

            // overall count: null property not added
            Assert.AreEqual(6, props.Properties.Count);
        }
        [TestMethod]
        public void Properties_Edge()
        {
            var props = new TestPropertyHolder();
            var originalProps = new Dictionary<string, object>
            {
                { string.Empty, "value1" },
                { LongPropertyName, LongText }
            };

            ApplicationInsightsLogger.AddProperties(props, null, null, 0, null, originalProps);

            Assert.AreEqual(3, props.Properties.Count);
            Assert.AreEqual(null, props.Properties["Message"]);
            Assert.AreEqual("0", props.Properties["EventId"]);

            Assert.IsFalse(props.Properties.ContainsKey("Title"));

            var longPropName = props.Properties.Keys.FirstOrDefault(k => k.StartsWith("TooLong"));
            Assert.IsTrue(!string.IsNullOrEmpty(longPropName));
            Assert.IsTrue(longPropName.Length > 500);
            Assert.IsFalse(longPropName.Contains("MISSINGTEXT"));

            var longValue = props.Properties[longPropName];
            Assert.IsTrue(!string.IsNullOrEmpty(longValue));
            Assert.IsTrue(longValue.Length > 8190);
            Assert.IsFalse(longValue.Contains("MISSINGTEXT"));
        }

        private const string LongPropertyName = "TooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongTooLongMISSINGTEXT";
        private const string LongText = @"too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too long too long too long 
too long too long too long too long too long too long too MISSINGTEXT";
    }
}
