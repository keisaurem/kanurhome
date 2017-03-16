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
using KaNurHome.attributes;

namespace KaNurHome.enums
{
    // ‰îŒìí•Ê
    public enum NursingTypes
    {
        [NursingType(
            "–K–âŠÅŒì",
            "csv/nursinghomes/VisitingNurse.csv",
            "file:///android_asset/html/img/Helper.png",
            NursingCategories.Helper,
            NursingCategories.Nurse)]
        VisitingNurse,

        [NursingType(
            "–K–â‰îŒì",
            "csv/nursinghomes/VisitingCare.csv",
            "file:///android_asset/html/img/Helper.png",
            NursingCategories.Helper,
            NursingCategories.Care)]
        VisitingCare,

        [NursingType(
            "–K–â“ü—‰îŒì",
            "csv/nursinghomes/VisitingBathCare.csv",
            "file:///android_asset/html/img/Helper.png",
            NursingCategories.Helper,
            NursingCategories.Care,
            NursingCategories.Bath)]
        VisitingBathCare,

        [NursingType(
            "–K–âƒŠƒnƒrƒŠƒe[ƒVƒ‡ƒ“",
            "csv/nursinghomes/VisitingRehabilitation.csv",
            "file:///android_asset/html/img/Rehabilitation.png",
            NursingCategories.Helper,
            NursingCategories.Rehabilitation)]
        VisitingRehabilitation,

        [NursingType(
            "’ÊŠƒŠƒnƒrƒŠƒe[ƒVƒ‡ƒ“",
            "csv/nursinghomes/DayServiceRehabilitation.csv",
            "file:///android_asset/html/img/Rehabilitation.png",
            NursingCategories.DayService,
            NursingCategories.Rehabilitation)]
        DayServiceRehabilitation,

        [NursingType(
            "’ÊŠ‰îŒì",
            "csv/nursinghomes/DayService.csv",
            "file:///android_asset/html/img/Dayservice.png",
            NursingCategories.DayService,
            NursingCategories.Care)]
        DayService,

        [NursingType(
            "’nˆæ–§’…Œ^’ÊŠ‰îŒì",
            "csv/nursinghomes/DayServiceSmall.csv",
            "file:///android_asset/html/img/Dayservice.png",
            NursingCategories.DayService,
            NursingCategories.Care,
            NursingCategories.Small)]
        DayServiceSmall,

        [NursingType(
            "‰îŒì˜Vl•Ÿƒ{İ(“Á•Ê—{Œì˜Vlƒz[ƒ€)",
            "csv/nursinghomes/NursingHomeSpecial.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.SpecialNurseCare,
            NursingCategories.Care)]
        NursingHomeSpecial,

        [NursingType(
            "‰îŒì˜Vl•ÛŒ’{İ",
            "csv/nursinghomes/NursingHome.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.Care)]
        NursingHome,

        [NursingType(
            "”F’mÇ‘Î‰Œ^’ÊŠ‰îŒì",
            "csv/nursinghomes/DayServiceDementia.csv",
            "file:///android_asset/html/img/Dayservice.png",
            NursingCategories.DayService,
            NursingCategories.Dementia,
            NursingCategories.Care)]
        DayServiceDementia,

        [NursingType(
            "”F’mÇ‘Î‰Œ^‹¤“¯¶Šˆ‰îŒì",
            "csv/nursinghomes/NursingHomeDementia.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.Dementia,
            NursingCategories.Care)]
        NursingHomeDementia,

        [NursingType(
            "’ZŠú“üŠŒ^—Ã—{‰îŒì(•a‰@)",
            "csv/nursinghomes/NursingHomeShortTimeHospital.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.Care,
            NursingCategories.ShortTime,
            NursingCategories.Hospital)]
        NursingHomeShortTimeHospital,

        [NursingType(
            "’ZŠú“üŠ—Ã—{‰îŒì(˜Vl•ÛŒ’{İ)",
            "csv/nursinghomes/NursingHomeShortTime.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.Care,
            NursingCategories.ShortTime)]
        NursingHomeShortTime
    }
}