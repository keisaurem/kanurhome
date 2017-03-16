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
using System.Xml.Linq;
using KaNurHome.xmls;
using KaNurHome.models.nursinghomes;
using KaNurHome.models.hospitals;

namespace KaNurHome.models.questions
{
    public class QuestionModels
    {

        // ID
        public string ID { get; set; }
        // �e�L�X�g
        public string Text { get; set; }


        // HTML�ϊ�
        public XElement ToHTML()
        {
            var divQuestionItem = new XElement("div");
            divQuestionItem.SetAttributeValue("id", this.ID);
            divQuestionItem.SetAttributeValue("class", "card_button ripple_target margB_20 text_center");

            var divTextWrap = new XElement("div");
            divTextWrap.SetAttributeValue("class", "pad_5");

            var divText = new XElement("div");
            divText.SetAttributeValue("class", "font_x qtext");
            divText.Value = this.Text;

            divTextWrap.Add(divText);
            divQuestionItem.Add(divTextWrap);

            divQuestionItem.Add(XHtmlModels.RippleEffectItem);

            return divQuestionItem;
        }
    }

    // ���^�C�v�����i���݃N�G�X�`�����V�[�g
    public class NursingTypeQuestionModels : QuestionModels
    {
        // �Ώۂ̉��^�C�v����
        public NursingTypes TargetType { get; set; }

    }
    public class HospitalQuestionModels : QuestionModels
    {
        public HospitalModels TargetModel { get; set; }

    }
}