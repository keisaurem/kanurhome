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
    // �����
    public enum NursingTypes
    {
        [NursingType(
            "�K��Ō�",
            "csv/nursinghomes/VisitingNurse.csv",
            "file:///android_asset/html/img/Helper.png",
            NursingCategories.Helper,
            NursingCategories.Nurse)]
        VisitingNurse,

        [NursingType(
            "�K����",
            "csv/nursinghomes/VisitingCare.csv",
            "file:///android_asset/html/img/Helper.png",
            NursingCategories.Helper,
            NursingCategories.Care)]
        VisitingCare,

        [NursingType(
            "�K��������",
            "csv/nursinghomes/VisitingBathCare.csv",
            "file:///android_asset/html/img/Helper.png",
            NursingCategories.Helper,
            NursingCategories.Care,
            NursingCategories.Bath)]
        VisitingBathCare,

        [NursingType(
            "�K�⃊�n�r���e�[�V����",
            "csv/nursinghomes/VisitingRehabilitation.csv",
            "file:///android_asset/html/img/Rehabilitation.png",
            NursingCategories.Helper,
            NursingCategories.Rehabilitation)]
        VisitingRehabilitation,

        [NursingType(
            "�ʏ����n�r���e�[�V����",
            "csv/nursinghomes/DayServiceRehabilitation.csv",
            "file:///android_asset/html/img/Rehabilitation.png",
            NursingCategories.DayService,
            NursingCategories.Rehabilitation)]
        DayServiceRehabilitation,

        [NursingType(
            "�ʏ����",
            "csv/nursinghomes/DayService.csv",
            "file:///android_asset/html/img/Dayservice.png",
            NursingCategories.DayService,
            NursingCategories.Care)]
        DayService,

        [NursingType(
            "�n�斧���^�ʏ����",
            "csv/nursinghomes/DayServiceSmall.csv",
            "file:///android_asset/html/img/Dayservice.png",
            NursingCategories.DayService,
            NursingCategories.Care,
            NursingCategories.Small)]
        DayServiceSmall,

        [NursingType(
            "���V�l�����{��(���ʗ{��V�l�z�[��)",
            "csv/nursinghomes/NursingHomeSpecial.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.SpecialNurseCare,
            NursingCategories.Care)]
        NursingHomeSpecial,

        [NursingType(
            "���V�l�ی��{��",
            "csv/nursinghomes/NursingHome.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.Care)]
        NursingHome,

        [NursingType(
            "�F�m�ǑΉ��^�ʏ����",
            "csv/nursinghomes/DayServiceDementia.csv",
            "file:///android_asset/html/img/Dayservice.png",
            NursingCategories.DayService,
            NursingCategories.Dementia,
            NursingCategories.Care)]
        DayServiceDementia,

        [NursingType(
            "�F�m�ǑΉ��^�����������",
            "csv/nursinghomes/NursingHomeDementia.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.Dementia,
            NursingCategories.Care)]
        NursingHomeDementia,

        [NursingType(
            "�Z�������^�×{���(�a�@)",
            "csv/nursinghomes/NursingHomeShortTimeHospital.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.Care,
            NursingCategories.ShortTime,
            NursingCategories.Hospital)]
        NursingHomeShortTimeHospital,

        [NursingType(
            "�Z�������×{���(�V�l�ی��{��)",
            "csv/nursinghomes/NursingHomeShortTime.csv",
            "file:///android_asset/html/img/NursingHome.png",
            NursingCategories.NursingHome,
            NursingCategories.Care,
            NursingCategories.ShortTime)]
        NursingHomeShortTime
    }
}