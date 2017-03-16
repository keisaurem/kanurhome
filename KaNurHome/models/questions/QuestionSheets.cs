using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KaNurHome.enums;
using KaNurHome.attributes;
using KaNurHome.models.nursinghomes;

namespace KaNurHome.models.questions
{
    // QuestionModels‚ÌW‡‘Ì
    public class QuestionSheets
    {
        public QuestionModels[] Models { get; set; }

        public QuestionSheets(params NursingTypes[] attrval)
        {
            if ((attrval == null) || (attrval.Length == 0))
            {
                attrval = new NursingCategories[] {
                    NursingCategories.Helper,
                    NursingCategories.DayService,
                    NursingCategories.NursingHome
                };
            }
            this.Models = GetTargetAttributes(attrval).ToArray();
        }

        public QuestionSheets(params NursingCategories[] attrval)
        {
            if ((attrval == null) || (attrval.Length == 0))
            {
                attrval = new NursingCategories[] {
                    NursingCategories.Helper,
                    NursingCategories.DayService,
                    NursingCategories.NursingHome
                };
            }
            this.Models = GetTargetAttributes(attrval).ToArray();
        }

        public QuestionSheets(NursingHomeModels[] models)
        {
            var addresses = Enumerable.Range(0, models.Length)
                .Select(i =>
                {
                    var mod = models[i];
                    var add = mod.Address
                        .Replace("ÎìŒ§", "")
                        .Replace("‹à‘òŽs", "")
                        .Trim();
                    add = new string(add.Reverse().ToArray());
                    var li = add.LastIndexOfAny(
                        new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                            '‚O','‚P','‚Q','‚R','‚S','‚T','‚U','‚V','‚W','‚X'}
                    );
                    add = new string(add.Substring(li + 1).Reverse().ToArray());
                    return add;
                }).Distinct().ToArray();

            this.Models = Enumerable.Range(0, addresses.Length)
                .Select(i =>
                {
                    return new QuestionModels
                    {
                        ID  = (i + 1).ToString(),
                        Text = addresses[i],
                        TargetType = 0
                    };
                }).ToArray();
        }

        private IEnumerable<QuestionModels> GetTargetAttributes(NursingCategories[] attrval)
        {
            for (var i = 0; i < attrval.Length; ++i)
            {
                var targetField = typeof(NursingCategories).GetField(attrval[i].ToString());
                var targetAttr = (QuestionAttribute)Attribute.GetCustomAttribute(targetField, typeof(QuestionAttribute));
                yield return new QuestionModels
                {
                    ID = "Q" + (i + 1),
                    Text = targetAttr.QuestionString,
                    TargetType = attrval[i]
                };
            }
        }
    }
}