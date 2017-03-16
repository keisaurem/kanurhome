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
using KaNurHome.models.nursinghomes;
using KaNurHome.attributes;
using KaNurHome.models.hospitals;

namespace KaNurHome.models.questions
{
    public class QuestionSheets
    {
        public QuestionModels[] Models { get; set; }

        public QuestionSheets(NursingTypes[] targetTypes)
        {
            this.Models = Enumerable.Range(0, targetTypes.Length)
                .Select(i => {
                    var attr = (NursingTypeAttribute)Attribute.GetCustomAttribute(
                        typeof(NursingTypes).GetField(targetTypes[i].ToString()), typeof(NursingTypeAttribute));
                    return new NursingTypeQuestionModels() { ID = (i + 1).ToString(), TargetType = targetTypes[i], Text = attr.Text };
                }).ToArray();
        }

        public QuestionSheets(HospitalModels[] models)
        {
            var q = Enumerable.Range(0, models.Length)
                .Select(i =>
                {
                    return new HospitalQuestionModels() { ID = (i + 1).ToString(), TargetModel = models[i], Text = models[i].Name };
                }).ToList();

            //q.Insert(0, new HospitalQuestionModels { ID = "-1", Text = "“Á‚É‚È‚µ" });

            this.Models = q.ToArray();
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
                        ID = (i + 1).ToString(),
                        Text = addresses[i]
                    };
                }).ToArray();
        }
    }
}