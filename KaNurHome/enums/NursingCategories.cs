//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;

using KaNurHome.attributes;

namespace KaNurHome.enums
{
    // 介護タイプの属性
    public enum NursingCategories
    {
        [Question("ヘルパーサービス")]
        Helper,             // ヘルパー
        [Question("デイサービス")]
        DayService,         // デイサービス
        [Question("入居サービス")]
        NursingHome,        // 老人ホーム
        [Question("リハビリサービス")]
        Rehabilitation,     // リハビリ施設
        [Question("認知症対応")]
        Dementia,           // 認知症対応
        [Question("短期サービス")]
        ShortTime,          // 短期間
        [Question("介護サービス")]
        Care,               // 介護
        [Question("看護サービス")]
        Nurse,              // 看護
        [Question("特別養護サービス")]
        SpecialNurseCare,   // 特別養護
        [Question("入浴補助サービス")]
        Bath,               // 入浴
        [Question("地域密着型")]
        Small,              // 小さいサイズ
        [Question("病院付属")]
        Hospital            // 病院付属
    }

}