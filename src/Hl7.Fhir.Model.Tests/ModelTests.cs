﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Tests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void ValidateElementAssertions()
        {
            XElement xr = new XElement("root",
                        new XElement("child", "value"),
                        new XElement("child", "value2"));

            Assert.IsNull(xr.Element("childx"));
            Assert.AreEqual(0, xr.Elements("childx").Count());
            Assert.AreEqual("value", xr.Element("child").Value);
        }


        [TestMethod]
        public void DateTimeHandling()
        {
            FhirDateTime dt = new FhirDateTime("2010-01-01");
            Assert.AreEqual("2010-01-01", dt.Value);

            FhirDateTime dt2 = new FhirDateTime(1972, 11, 30, 15, 10);
            Assert.IsTrue(dt2.Value.StartsWith("1972-11-30T15:10"));
        }

       

        [TestMethod]
        public void SimpleValueSupport()
        {
            Conformance c = new Conformance();

            Assert.IsNull(c.AcceptUnknown);
            c.AcceptUnknown = true;
            Assert.IsTrue(c.AcceptUnknown.GetValueOrDefault());
            Assert.IsNotNull(c.AcceptUnknownElement);
            Assert.IsTrue(c.AcceptUnknownElement.Value.GetValueOrDefault());

            c.PublisherElement = new FhirString("Furore");
            Assert.AreEqual("Furore", c.Publisher);
            c.Publisher = null;
            Assert.IsNull(c.PublisherElement);
            c.Publisher = "Furore";
            Assert.IsNotNull(c.PublisherElement);

            c.Format = new string[] { "json", "xml" };
            Assert.IsNotNull(c.FormatElement);
            Assert.AreEqual(2, c.FormatElement.Count);
            Assert.AreEqual("json", c.FormatElement.First().Value);

            c.FormatElement = new List<Code>();
            c.FormatElement.Add(new Code("csv"));
            Assert.IsNotNull(c.Format);
            Assert.AreEqual(1, c.Format.Count());
        }


        [TestMethod]
        public void ExtensionManagement()
        {
            Patient p = new Patient();
            Uri u1 = new Uri("http://fhir.org/ext/ext-test");
            Assert.IsNull(p.GetExtension(u1));

            Extension newEx = p.SetExtension(u1, new FhirBoolean(true));
            Assert.AreSame(newEx, p.GetExtension(u1));

            p.AddExtension(new Uri("http://fhir.org/ext/ext-test2"), new FhirString("Ewout"));
            Assert.AreSame(newEx, p.GetExtension(u1));

            p.RemoveExtension(u1);
            Assert.IsNull(p.GetExtension(u1));

            p.SetExtension(new Uri("http://fhir.org/ext/ext-test2"), new FhirString("Ewout Kramer"));
            var ew = p.GetExtensions(new Uri("http://fhir.org/ext/ext-test2"));
            Assert.AreEqual(1, ew.Count());

            p.AddExtension(new Uri("http://fhir.org/ext/ext-test2"), new FhirString("Wouter Kramer"));

            ew = p.GetExtensions(new Uri("http://fhir.org/ext/ext-test2"));
            Assert.AreEqual(2, ew.Count());

        }


        [TestMethod]
        public void RecognizeContainedReference()
        {
            var rref = new ResourceReference() { Reference = "#patient2223432" };

            Assert.IsTrue(rref.IsContainedReference);

            rref.Reference = "http://somehwere.nl/Patient/1";
            Assert.IsFalse(rref.IsContainedReference);

            rref.Reference = "Patient/1";
            Assert.IsFalse(rref.IsContainedReference);
        }


        [TestMethod]
        public void FindContainedResource()
        {
            var cPat1 = new Patient() { Id = "pat1" };
            var cPat2 = new Patient() { Id = "pat2" };
            var pat = new Patient();

            pat.Contained = new List<Resource> { cPat1, cPat2 };

            var rref = new ResourceReference() { Reference = "#pat2" };

            Assert.IsNotNull(pat.FindContainedResource(rref));
            Assert.IsNotNull(pat.FindContainedResource(rref.Url));
            
            rref.Reference = "#pat3";
            Assert.IsNull(pat.FindContainedResource(rref));
        }

        [TestMethod]
        public void TypedResourceEntry()
        {
            var pe = new ResourceEntry<Patient>();

            pe.Resource = new Patient();

            ResourceEntry e = pe;

            Assert.AreEqual(pe.Resource, e.Resource);

            e.Resource = new CarePlan();

            try
            {
                var c = pe.Resource;
                Assert.Fail("Should have bombed");
            }
            catch (InvalidCastException)
            {
                // pass
            }
        }

        [TestMethod]
        public void SelectInstancesAndSeries()
        {
            var studies = new List<ImagingStudy>();

            var studyA = new ImagingStudy();
            studyA.Series = new List<ImagingStudy.ImagingStudySeriesComponent>();

            var serieA1 = new ImagingStudy.ImagingStudySeriesComponent();
            serieA1.Instance = new List<ImagingStudy.ImagingStudySeriesInstanceComponent>();

            var instanceA11 = new ImagingStudy.ImagingStudySeriesInstanceComponent() { Uid = "a11" };
            var instanceA12 = new ImagingStudy.ImagingStudySeriesInstanceComponent() { Uid = "a12" };
            var instanceA13 = new ImagingStudy.ImagingStudySeriesInstanceComponent() { Uid = "a13" };

            serieA1.Instance.Add(instanceA11);
            serieA1.Instance.Add(instanceA12);
            serieA1.Instance.Add(instanceA13);

            var serieA2 = new ImagingStudy.ImagingStudySeriesComponent();
            serieA2.Instance = null;

            studyA.Series.Add(serieA1);
            studyA.Series.Add(serieA2);

            var studyB = new ImagingStudy();
            studyB.Series = null;

            studies.Add(studyA);
            studies.Add(studyB);

            Assert.AreEqual(3, studies.ListInstances().Count());
            Assert.AreEqual(2, studies.ListSeries().Count());
        }
    }
}
