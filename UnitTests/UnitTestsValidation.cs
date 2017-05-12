﻿using System.Collections.Generic;
using System.Linq;
using EdiFabric.Core.Model.Edi.ErrorCodes;
using EdiFabric.Core.Model.Edi.Exceptions;
using EdiFabric.Framework.Readers;
using EdiFabric.Rules.EDIFACT_D00A.Rep;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EdiFabric.UnitTests
{
    /// <summary>
    /// Summary description for UnitTestsValidation
    /// </summary>
    [TestClass]
    public class UnitTestsValidation
    {

        [TestMethod]
        public void TestValidationRequiredMissing()
        {
            // ARRANGE
            const string sample = "EdiFabric.UnitTests.Edi.Edifact_INVOIC_D00A_Val_RequiredMissing.txt";
            var ediStream = CommonHelper.LoadStream(sample, false);
            List<object> ediItems;

            // ACT
            using (var ediReader = new EdifactReader(ediStream, "EdiFabric.Rules.EdifactD00A.Rep"))
            {
                ediItems = ediReader.ReadToEnd().ToList();
            }
            var msg = ediItems.OfType<TSINVOICAll>().Single();

            List<ErrorContextSegment> results;
            var validationResult = msg.IsValid(out results);

            // ASSERT
            Assert.IsFalse(validationResult);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count == 6);

            var sErr1 = results.SingleOrDefault(r => r.Name == "UNH" && r.Position == 1);
            Assert.IsNotNull(sErr1);
            Assert.IsTrue(sErr1.Codes.Count == 0);
            Assert.IsTrue(sErr1.Errors.Count == 1);
            var dErr1 = sErr1.Errors.First();
            Assert.IsTrue(dErr1.Name == "0051");
            Assert.IsTrue(dErr1.Position == 2);
            Assert.IsTrue(dErr1.ComponentPosition == 4);
            Assert.IsTrue(dErr1.RepetitionPosition == 0);
            Assert.IsTrue(dErr1.Value == null);
            Assert.IsTrue(dErr1.Code == DataElementErrorCode.RequiredDataElementMissing);

            var sErr2 = results.SingleOrDefault(r => r.Name == "PCD" && r.Position == 14);
            Assert.IsNotNull(sErr2);
            Assert.IsTrue(sErr2.Codes.Count == 0);
            Assert.IsTrue(sErr2.Errors.Count == 1);
            var dErr2 = sErr2.Errors.First();
            Assert.IsTrue(dErr2.Name == "C501");
            Assert.IsTrue(dErr2.Position == 1);
            Assert.IsTrue(dErr2.ComponentPosition == 0);
            Assert.IsTrue(dErr2.RepetitionPosition == 0);
            Assert.IsTrue(dErr2.Value == null);
            Assert.IsTrue(dErr2.Code == DataElementErrorCode.RequiredDataElementMissing);

            var sErr3 = results.SingleOrDefault(r => r.Name == "UNS" && r.Position == 32);
            Assert.IsNotNull(sErr3);
            Assert.IsTrue(sErr3.Codes.Count == 1);
            Assert.IsTrue(sErr3.Errors.Count == 0);
            Assert.IsTrue(sErr3.Codes.Contains(SegmentErrorCode.RequiredSegmentMissing));

            var sErr4 = results.SingleOrDefault(r => r.Name == "MOA" && r.Position == 32);
            Assert.IsNotNull(sErr4);
            Assert.IsTrue(sErr4.Codes.Count == 1);
            Assert.IsTrue(sErr4.Errors.Count == 0);
            Assert.IsTrue(sErr4.Codes.Contains(SegmentErrorCode.RequiredSegmentMissing));

            var sErr5 = results.SingleOrDefault(r => r.Name == "PAI" && r.Position == 4);
            Assert.IsNotNull(sErr5);
            Assert.IsTrue(sErr5.Codes.Count == 1);
            Assert.IsTrue(sErr5.Errors.Count == 0);
            Assert.IsTrue(sErr5.Codes.Contains(SegmentErrorCode.RequiredSegmentMissing));

            var sErr6 = results.SingleOrDefault(r => r.Name == "UNT" && r.Position == 4);
            Assert.IsNotNull(sErr6);
            Assert.IsTrue(sErr6.Codes.Count == 1);
            Assert.IsTrue(sErr6.Errors.Count == 0);
            Assert.IsTrue(sErr6.Codes.Contains(SegmentErrorCode.RequiredSegmentMissing));
        }

        [TestMethod]
        public void TestValidationListCountMax()
        {
            // ARRANGE
            const string sample = "EdiFabric.UnitTests.Edi.Edifact_INVOIC_D00A_Val_ListCountMax.txt";
            var ediStream = CommonHelper.LoadStream(sample, false);
            List<object> ediItems;

            // ACT
            using (var ediReader = new EdifactReader(ediStream, "EdiFabric.Rules.EdifactD00A.Rep"))
            {
                ediItems = ediReader.ReadToEnd().ToList();
            }
            var msg = ediItems.OfType<TSINVOIC>().Single();

            List<ErrorContextSegment> results;
            var validationResult = msg.IsValid(out results);

            // ASSERT
            Assert.IsFalse(validationResult);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count == 4);

            var sErr1 = results.SingleOrDefault(r => r.Name == "BGM" && r.Position == 2);
            Assert.IsNotNull(sErr1);
            Assert.IsTrue(sErr1.Codes.Count == 0);
            Assert.IsTrue(sErr1.Errors.Count == 1);
            var dErr1 = sErr1.Errors.First();
            Assert.IsTrue(dErr1.Name == "1225");
            Assert.IsTrue(dErr1.Position == 3);
            Assert.IsTrue(dErr1.ComponentPosition == 0);
            Assert.IsTrue(dErr1.RepetitionPosition == 4);
            Assert.IsTrue(dErr1.Value == null);
            Assert.IsTrue(dErr1.Code == DataElementErrorCode.TooManyRepetitions);

            var sErr2 = results.SingleOrDefault(r => r.Name == "COM" && r.Position == 15);
            Assert.IsNotNull(sErr2);
            Assert.IsTrue(sErr2.Codes.Count == 0);
            Assert.IsTrue(sErr2.Errors.Count == 1);
            var dErr2 = sErr2.Errors.First();
            Assert.IsTrue(dErr2.Name == "C076");
            Assert.IsTrue(dErr2.Position == 1);
            Assert.IsTrue(dErr2.ComponentPosition == 0);
            Assert.IsTrue(dErr2.RepetitionPosition == 4);
            Assert.IsTrue(dErr2.Value == null);
            Assert.IsTrue(dErr2.Code == DataElementErrorCode.TooManyRepetitions);

            var sErr3 = results.SingleOrDefault(r => r.Name == "ALI" && r.Position == 4);
            Assert.IsNotNull(sErr3);
            Assert.IsTrue(sErr3.Codes.Count == 1);
            Assert.IsTrue(sErr3.Errors.Count == 0);
            Assert.IsTrue(sErr3.Codes.Contains(SegmentErrorCode.SegmentExceedsMaximumUse));

            var sErr4 = results.SingleOrDefault(r => r.Name == "TAX" && r.Position == 19);
            Assert.IsNotNull(sErr4);
            Assert.IsTrue(sErr4.Codes.Count == 1);
            Assert.IsTrue(sErr4.Errors.Count == 0);
            Assert.IsTrue(sErr4.Codes.Contains(SegmentErrorCode.LoopExceedsMaximumUse));
        }

        [TestMethod]
        public void TestValidationListCountMin()
        {
            // ARRANGE
            const string sample = "EdiFabric.UnitTests.Edi.Edifact_INVOIC_D00A_Val_ListCountMin.txt";
            var ediStream = CommonHelper.LoadStream(sample, false);
            List<object> ediItems;

            // ACT
            using (var ediReader = new EdifactReader(ediStream, "EdiFabric.Rules.EdifactD00A.Rep"))
            {
                ediItems = ediReader.ReadToEnd().ToList();
            }
            var msg = ediItems.OfType<TSINVOIC>().Single();

            List<ErrorContextSegment> results;
            var validationResult = msg.IsValid(out results);

            // ASSERT
            Assert.IsFalse(validationResult);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count == 4);

            var sErr1 = results.SingleOrDefault(r => r.Name == "BGM" && r.Position == 2);
            Assert.IsNotNull(sErr1);
            Assert.IsTrue(sErr1.Codes.Count == 0);
            Assert.IsTrue(sErr1.Errors.Count == 1);
            var dErr1 = sErr1.Errors.First();
            Assert.IsTrue(dErr1.Name == "1225");
            Assert.IsTrue(dErr1.Position == 3);
            Assert.IsTrue(dErr1.ComponentPosition == 0);
            Assert.IsTrue(dErr1.RepetitionPosition == 2);
            Assert.IsTrue(dErr1.Value == null);
            Assert.IsTrue(dErr1.Code == DataElementErrorCode.TooFewRepetitions);

            var sErr2 = results.SingleOrDefault(r => r.Name == "COM" && r.Position == 10);
            Assert.IsNotNull(sErr2);
            Assert.IsTrue(sErr2.Codes.Count == 0);
            Assert.IsTrue(sErr2.Errors.Count == 1);
            var dErr2 = sErr2.Errors.First();
            Assert.IsTrue(dErr2.Name == "C076");
            Assert.IsTrue(dErr2.Position == 1);
            Assert.IsTrue(dErr2.ComponentPosition == 0);
            Assert.IsTrue(dErr2.RepetitionPosition == 2);
            Assert.IsTrue(dErr2.Value == null);
            Assert.IsTrue(dErr2.Code == DataElementErrorCode.TooFewRepetitions);

            var sErr3 = results.SingleOrDefault(r => r.Name == "ALI" && r.Position == 4);
            Assert.IsNotNull(sErr3);
            Assert.IsTrue(sErr3.Codes.Count == 1);
            Assert.IsTrue(sErr3.Errors.Count == 0);
            Assert.IsTrue(sErr3.Codes.Contains(SegmentErrorCode.SegmentBelowMinimumUse));

            var sErr4 = results.SingleOrDefault(r => r.Name == "TAX" && r.Position == 14);
            Assert.IsNotNull(sErr4);
            Assert.IsTrue(sErr4.Codes.Count == 1);
            Assert.IsTrue(sErr4.Errors.Count == 0);
            Assert.IsTrue(sErr4.Codes.Contains(SegmentErrorCode.LoopBelowMinimumUse));
        }

        [TestMethod]
        public void TestValidationInvalidAttributes()
        {
            // ARRANGE
            const string sample = "EdiFabric.UnitTests.Edi.Edifact_INVOIC_D00A_Val_InvalidAttributes.txt";
            var ediStream = CommonHelper.LoadStream(sample, false);
            List<object> ediItems;

            // ACT
            using (var ediReader = new EdifactReader(ediStream, "EdiFabric.Rules.EdifactD00A.Rep"))
            {
                ediItems = ediReader.ReadToEnd().ToList();
            }
            var msg = ediItems.OfType<TSINVOICInvalidAttributes>().Single();

            List<ErrorContextSegment> results;
            var validationResult = msg.IsValid(out results);
            
            // ASSERT
            Assert.IsTrue(validationResult);
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        public void TestValidationStringLen()
        {
            // ARRANGE
            const string sample = "EdiFabric.UnitTests.Edi.Edifact_INVOIC_D00A_Val_StringLen.txt";
            var ediStream = CommonHelper.LoadStream(sample, false);
            List<object> ediItems;

            // ACT
            using (var ediReader = new EdifactReader(ediStream, "EdiFabric.Rules.EdifactD00A.Rep"))
            {
                ediItems = ediReader.ReadToEnd().ToList();
            }
            var msg = ediItems.OfType<TSINVOIC>().Single();

            List<ErrorContextSegment> results;
            var validationResult = msg.IsValid(out results);

            // ASSERT
            Assert.IsFalse(validationResult);
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count == 4);

            var sErr1 = results.SingleOrDefault(r => r.Name == "UNH" && r.Position == 1);
            Assert.IsNotNull(sErr1);
            Assert.IsTrue(sErr1.Codes.Count == 0);
            Assert.IsTrue(sErr1.Errors.Count == 1);
            var dErr1 = sErr1.Errors.First();
            Assert.IsTrue(dErr1.Name == "0062");
            Assert.IsTrue(dErr1.Position == 1);
            Assert.IsTrue(dErr1.ComponentPosition == 0);
            Assert.IsTrue(dErr1.RepetitionPosition == 0);
            Assert.IsTrue(dErr1.Value == "5");
            Assert.IsTrue(dErr1.Code == DataElementErrorCode.DataElementTooShort);

            var sErr2 = results.FirstOrDefault(r => r.Name == "BGM" && r.Position == 2);
            Assert.IsNotNull(sErr2);
            Assert.IsTrue(sErr2.Codes.Count == 0);
            Assert.IsTrue(sErr2.Errors.Count == 1);
            var dErr2 = sErr2.Errors.First();
            Assert.IsTrue(dErr2.Name == "1225");
            Assert.IsTrue(dErr2.Position == 3);
            Assert.IsTrue(dErr2.ComponentPosition == 0);
            Assert.IsTrue(dErr2.RepetitionPosition == 1);
            Assert.IsTrue(dErr2.Value == "B");
            Assert.IsTrue(dErr2.Code == DataElementErrorCode.DataElementTooShort);

            var sErr3 = results.LastOrDefault(r => r.Name == "BGM" && r.Position == 2);
            Assert.IsNotNull(sErr3);
            Assert.IsTrue(sErr3.Codes.Count == 0);
            Assert.IsTrue(sErr3.Errors.Count == 1);
            var dErr3 = sErr3.Errors.First();
            Assert.IsTrue(dErr3.Name == "1225");
            Assert.IsTrue(dErr3.Position == 3);
            Assert.IsTrue(dErr3.ComponentPosition == 0);
            Assert.IsTrue(dErr3.RepetitionPosition == 2);
            Assert.IsTrue(dErr3.Value == "CDEF");
            Assert.IsTrue(dErr3.Code == DataElementErrorCode.DataElementTooLong);

            var sErr4 = results.SingleOrDefault(r => r.Name == "MOA" && r.Position == 16);
            Assert.IsNotNull(sErr4);
            Assert.IsTrue(sErr4.Codes.Count == 0);
            Assert.IsTrue(sErr4.Errors.Count == 1);
            var dErr4 = sErr4.Errors.First();
            Assert.IsTrue(dErr4.Name == "6345");
            Assert.IsTrue(dErr4.Position == 1);
            Assert.IsTrue(dErr4.ComponentPosition == 3);
            Assert.IsTrue(dErr4.RepetitionPosition == 0);
            Assert.IsTrue(dErr4.Value == "BCDE");
            Assert.IsTrue(dErr4.Code == DataElementErrorCode.DataElementTooLong);
        }

        [TestMethod]
        public void TestValidationNoAttributes()
        {
            // ARRANGE
            const string sample = "EdiFabric.UnitTests.Edi.Edifact_INVOIC_D00A_Val_NoAttributes.txt";
            var ediStream = CommonHelper.LoadStream(sample, false);
            List<object> ediItems;

            // ACT
            using (var ediReader = new EdifactReader(ediStream, "EdiFabric.Rules.EdifactD00A.Rep"))
            {
                ediItems = ediReader.ReadToEnd().ToList();
            }
            var msg = ediItems.OfType<TSINVOICNoAttributes>().Single();

            List<ErrorContextSegment> results;
            var validationResult = msg.IsValid(out results);

            // ASSERT
            Assert.IsTrue(validationResult);
            Assert.IsFalse(results.Any());
        }
    }
}