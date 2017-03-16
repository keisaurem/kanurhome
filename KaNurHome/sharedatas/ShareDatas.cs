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
using KaNurHome.models.hospitals;

namespace KaNurHome.sharedatas
{
    public static class ShareDatas
    {
        // éÌï 
        private static List<NursingTypes> _SelectedNursingTypes = new List<NursingTypes>();
        public static List<NursingTypes> SelectedNursingTypes
        {
            get
            {
                return _SelectedNursingTypes;
            }
        }

        // ãﬂó◊ïaâ@
        private static List<HospitalModels> _SelectedHospitalModels = new List<HospitalModels>();
        public static List<HospitalModels> SelectedHospitalModels
        {
            get
            {
                return _SelectedHospitalModels;
            }
        }

        // èZèä
        private static List<string> _Addresses = new List<string>();
        public static List<string> Addresses
        {
            get
            {
                return _Addresses;
            }
        }

        // é{ê›
        public static NursingHomeModels SelectedNursingHome { get; set; } = null;
    }
}