﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EdiWeave.Core.Model.Edi;
using EdiWeave.Framework.Readers;
using EdiWeave.Rules.EDIFACT_D00A.Rep;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EdiWeave.UnitTests
{
    /// <summary>
    /// Summary description for UnitTestsXml
    /// </summary>
    [TestClass]
    public class UnitTestsXml
    {
        [TestMethod]
        public void TestSerialization()
        {
            // ARRANGE
            const string sample = "EdiFabric.UnitTests.Edi.Edifact_INVOIC_D00A.txt";
            const string expectedSample = "EdiFabric.UnitTests.Xml.Edifact_INVOIC_D00A.xml";
            var ediStream = CommonHelper.LoadStream(sample, false);
            var expected = XDocument.Parse(CommonHelper.LoadString(expectedSample, Encoding.UTF8, false));
            List<EdiItem> ediItems;

            // ACT
            using (var ediReader = new EdifactReader(ediStream, "EdiFabric.Rules.EdifactD00A.Rep", Encoding.UTF8))
            {
                ediItems = ediReader.ReadToEnd().ToList();
            }
            var msg = ediItems.OfType<TSINVOIC>().Single();
            var actual = CommonHelper.Serialize(msg, Encoding.UTF8);

            // ASSERT
            //File.WriteAllText(@"C:\Test\Actual.txt", actual.Root.ToString());
            //File.WriteAllText(@"C:\Test\Expected.txt", expected.Root.ToString());
            Assert.IsTrue(expected.Root.ToString() == actual.Root.ToString());
        }
    }
}
