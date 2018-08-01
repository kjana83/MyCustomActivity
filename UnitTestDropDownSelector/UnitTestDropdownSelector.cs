using System;
using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestDropDownSelector
{
    [TestClass]
    public class UnitTestDropdownSelector
    {
        [TestMethod]
        public void DropdownSpecialCharsMatch()
        {
            var matchDropdown = new ClosestDropdownSelector.MatchDropdownValue();
            var inputForActivity = new Dictionary<string, object>()
            {
                { "ToBeMatched" , "Jana"},
                {"ValuesList",new List<string>{ "janarthana", ".jana@","janak.#" } },
                {"FindClosestMatch",false }
            };

            var output = WorkflowInvoker.Invoke(matchDropdown, inputForActivity);

            Assert.AreEqual(output["MatchedItem"], ".jana@");
        }

        [TestMethod]
        public void DropdownClosestMatch()
        {
            var matchDropdown = new ClosestDropdownSelector.MatchDropdownValue();
            var inputForActivity = new Dictionary<string, object>()
            {
                { "ToBeMatched" , "Jana"},
                {"ValuesList",new List<string>{ "janarthana", "jane","janak" } },
                {"FindClosestMatch",true }
            };

            var output = WorkflowInvoker.Invoke(matchDropdown, inputForActivity);

            Assert.AreEqual(output["MatchedItem"], "jane");
        }
        [TestMethod]
        public void DropdownClosestLobMatch()
        {
            var matchDropdown = new ClosestDropdownSelector.MatchDropdownValue();
            var inputForActivity = new Dictionary<string, object>()
            {
                { "ToBeMatched" , "Liabillity"},
                {"ValuesList",new List<string>{ "Please Select..", "Property.","Liability.","Automobile." } },
                {"FindClosestMatch",true }
            };

            var output = WorkflowInvoker.Invoke(matchDropdown, inputForActivity);

            Assert.AreEqual(output["MatchedItem"], "Liability.");
        }
    }
}
