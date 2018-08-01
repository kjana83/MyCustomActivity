using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ClosestDropdownSelector
{
    public class MatchDropdownValue : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> ToBeMatched { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<List<string>> ValuesList { get; set; }

        [Category("Output")]
        public OutArgument<string> MatchedItem { get; set; }

        [Category("Input")]
        public InArgument<bool> FindClosestMatch { get; set; }        

        protected override void Execute(CodeActivityContext context)
        {
            var regEx = new Regex("[^a-zA-Z0-9]+");
            var minDistance = 99999;

            var toBeMatched = regEx.Replace(ToBeMatched.Get(context), "").ToLower();
            var valueDictionary = new Dictionary<string, int>();

            var findClosestMatch = FindClosestMatch.Get(context);

            foreach (var value in ValuesList.Get(context))
            {
                var val = regEx.Replace(value, "").ToLower();
                if (!findClosestMatch && val == toBeMatched)
                {
                    this.MatchedItem.Set(context,value);
                    break;
                }

                if (findClosestMatch)
                {
                    var distance = LevenshteinDistance.Compute(toBeMatched, val);
                    valueDictionary.Add(value, distance);
                    if (minDistance > distance)
                        minDistance = distance;
                }
                
            }

            if (findClosestMatch)
            {
                var result = valueDictionary.FirstOrDefault(val => val.Value == minDistance).Key;                    
                this.MatchedItem.Set(context, result);
                
            }
        }
    }
}
