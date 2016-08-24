using GlobalLogic.BLL.Factory.WriterFactory;
using GlobalLogic.BLL.Model;
using GlobalLogic.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace GlobalLogic_Assignment.tests
{
    [TestClass]
    public class ApplicatonTests
    {
        [TestMethod]
        public void ComparerTest()
        {
            
            //Unit testing for numeric format
            var inputCollection = new ObservableCollection<string> { "1", "4", "10", "2", "5" };
            var expectedCollection = new ObservableCollection<string> { "1", "2", "4", "5", "10" };
            ObservableCollection<string> actualCollection;

            using (var comparer = new CustomComparer())
            {
                actualCollection = new ObservableCollection<string>(inputCollection.OrderBy(k => k, comparer));
            }

            Assert.IsNotNull(actualCollection);
            Assert.AreEqual(expectedCollection.Count, actualCollection.Count);
            Assert.IsTrue(expectedCollection.SequenceEqual(actualCollection));

            //Unit testing for string format
            inputCollection = new ObservableCollection<string> { "za", "wb", "abc", "aa", "zb" };
            expectedCollection = new ObservableCollection<string> { "aa", "abc", "wb", "za", "zb" };
            using (var comparer = new CustomComparer())
            {
                actualCollection = new ObservableCollection<string>(inputCollection.OrderBy(k => k, comparer));
            }

            Assert.IsNotNull(actualCollection);
            Assert.AreEqual(expectedCollection.Count, actualCollection.Count);
            Assert.IsTrue(expectedCollection.SequenceEqual(actualCollection));

            //Unit testing for aplhanumeric format
            inputCollection = new ObservableCollection<string> { "1za", "5wb", "2abc", "3aa", "2zb", "ab11", "ab9" };
            expectedCollection = new ObservableCollection<string> { "1za", "2abc", "2zb", "3aa", "5wb", "ab9", "ab11" };
            using (var comparer = new CustomComparer())
            {
                actualCollection = new ObservableCollection<string>(inputCollection.OrderBy(k => k, comparer));
            }

            Assert.IsNotNull(actualCollection);
            Assert.AreEqual(expectedCollection.Count, actualCollection.Count);
            Assert.IsTrue(expectedCollection.SequenceEqual(actualCollection));
        }

        [TestMethod]
        public void InpectObjectTest()
        {
            Address address = new Address();
            address.City = "Delhi";
            address.Street = "Street 1";
            address.Suite = "Suite 1";
            address.Zipcode = "1234";

            Company company = new Company();
            company.BS = "BS";
            company.CatchPhrase = "Catch Phrase";
            company.Name = "Company Name";

            User usr = new User();
            usr.Address = address;
            usr.Company = company;
            usr.Email = "test1@gmail.com";
            usr.ID = 1;
            usr.Phone = "9199989898";
            usr.UserName = "userTest";
            usr.WebSite = "www.website.com";
            usr.Name = "Name";

            List<PropertyDetail> expectedCollection = new List<PropertyDetail>();
            expectedCollection.Add(new PropertyDetail { Name = "City", Value = "Delhi" });
            expectedCollection.Add(new PropertyDetail { Name = "Street", Value = "Street 1" });
            expectedCollection.Add(new PropertyDetail { Name = "Suite", Value = "Suite 1" });
            expectedCollection.Add(new PropertyDetail { Name = "Zipcode", Value = "1234" });
            expectedCollection.Add(new PropertyDetail { Name = "BS", Value = "BS" });
            expectedCollection.Add(new PropertyDetail { Name = "CatchPhrase", Value = "Catch Phrase" });
            expectedCollection.Add(new PropertyDetail { Name = "Name", Value = "Name" });
            expectedCollection.Add(new PropertyDetail { Name = "Name", Value = "Company Name" });
            expectedCollection.Add(new PropertyDetail { Name = "Email", Value = "test1@gmail.com" });
            expectedCollection.Add(new PropertyDetail { Name = "ID", Value = "1" });
            expectedCollection.Add(new PropertyDetail { Name = "Phone", Value = "9199989898" });
            expectedCollection.Add(new PropertyDetail { Name = "UserName", Value = "userTest" });
            expectedCollection.Add(new PropertyDetail { Name = "WebSite", Value = "www.website.com" });

            var actualCollection = InspectObject.GetPropertyValues(usr);
            expectedCollection = expectedCollection.OrderBy(x => x.Name).ToList();
            actualCollection = actualCollection.OrderBy(x => x.Name).ToList();

            PropertyDetailComparer comparer = new PropertyDetailComparer();
            CollectionAssert.AreEqual(expectedCollection, actualCollection, comparer);
        }

        [TestMethod]
        public void JSONToObjectCollectionTest()
        {
            List<PropertyDetail> expectedCollection = new List<PropertyDetail>();
            expectedCollection.Add(new PropertyDetail() { Name = "Property1", Value = "Value1" });
            expectedCollection.Add(new PropertyDetail() { Name = "Property2", Value = "Value2" });

            string jsonString = "[{\"Name\":\"Property1\",\"Value\":\"Value1\"},{\"Name\":\"Property2\",\"Value\":\"Value2\"}]";
            var actualCollection = JsonToObjectCollection.getData<PropertyDetail>(Encoding.ASCII.GetBytes(jsonString));

            PropertyDetailComparer comparer = new PropertyDetailComparer();
            CollectionAssert.AreEqual(expectedCollection, actualCollection, comparer);
        }

        [TestMethod]
        public void GenerateJSONTest()
        {
            string expected = "{\"Name\":\"Property1\",\"Value\":\"Value1\"}";

            PropertyDetail propertyObj = new PropertyDetail();
            propertyObj.Name = "Property1";
            propertyObj.Value = "Value1";

            JSONWriter writer = new JSONWriter();
            var actual = writer.Generate<PropertyDetail>(propertyObj);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GeneratePlainTextTest()
        {
            var expectedContent = new StringBuilder();
            expectedContent.AppendFormat("{0}:{1}{2}", "Name", "Property1", Environment.NewLine);
            expectedContent.AppendFormat("{0}:{1}{2}", "Value", "Value1", Environment.NewLine);

            PropertyDetail propertyObj = new PropertyDetail();
            propertyObj.Name = "Property1";
            propertyObj.Value = "Value1";

            PlainWriter writer = new PlainWriter();
            var actualContent = writer.Generate<PropertyDetail>(propertyObj);
            Assert.AreEqual(expectedContent.ToString(), actualContent);
        }

        [TestMethod]
        public void GenerateHTMLTest()
        {
           //TODO: This will test whether the generated html is valid or not. 
            //Also, verify the output generated from Generate method of HTML Writer.
        }
    }

    public class PropertyDetailComparer : Comparer<PropertyDetail>
    {
        public override int Compare(PropertyDetail source, PropertyDetail target)
        {
            int comparison = source.Name.CompareTo(target.Name);
            if (comparison != 0) return comparison;

            comparison = source.Value.CompareTo(target.Value);
            if (comparison != 0) return comparison;

            return 0;
        }
    }
}
