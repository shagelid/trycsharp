using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should;

namespace LibraryTests
{
    public class DictonaryTests
    {
        [Fact]
        public void Init_dict()
        {
            var dict = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };
            dict.Count().ShouldEqual(2);

            dict = new Dictionary<string, string>();
            dict.Add("key1", "value1");
            dict.Add("key2", "value2");

            dict.Count().ShouldEqual(2);

        }

        [Fact]
        public void Add_elements_to_dictonary()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("key1", "value1");
            dict.Add("key2", "value2");
            dict.Count().ShouldEqual(2);

            // add or replace if exists
            dict["key3"]="value3";
            dict["key3"].Equals("value3");
            dict.Count().ShouldEqual(3);
            dict["key3"].ShouldEqual("value3");

            dict["key3"] = "value4";
            dict["key3"].ShouldEqual("value4");

            dict.Count().ShouldEqual(3);

        }
        [Fact]
        public void Init_dict_from_object_list()
        {
            var list = new List<Person> { new Person { Name = "John", Age = 34 }, new Person { Name = "Matt", Age = 45 } };
            list.Count().ShouldEqual(2);
            Dictionary<string, Person> dict = list.ToDictionary(p => p.Name);
            dict.Count().ShouldEqual(2);
            dict["Matt"].Age.ShouldEqual(45);
        }

        [Fact]
        public void Dictonary_trows_exception_on_duplicate_key()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("key1", "value1");

            var ex = Assert.ThrowsAny<Exception>(() => dict.Add("key1", "value1"));
            ex.ShouldNotBeNull();
            ex.Message.ShouldStartWith("An item with the same key has already been added.");

            // DotNet core has tryAdd, returns true if added
            if (!dict.ContainsKey("key1"))
                dict.Add("key1", "value1");
        }

    }

    internal class Person
    {
        public string Name { get; internal set; }
        public int Age { get; internal set; }
    }
}
