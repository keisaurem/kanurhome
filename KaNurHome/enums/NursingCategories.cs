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
    // ���^�C�v�̑���
    public enum NursingCategories
    {
        [Question("�w���p�[�T�[�r�X")]
        Helper,             // �w���p�[
        [Question("�f�C�T�[�r�X")]
        DayService,         // �f�C�T�[�r�X
        [Question("�����T�[�r�X")]
        NursingHome,        // �V�l�z�[��
        [Question("���n�r���T�[�r�X")]
        Rehabilitation,     // ���n�r���{��
        [Question("�F�m�ǑΉ�")]
        Dementia,           // �F�m�ǑΉ�
        [Question("�Z���T�[�r�X")]
        ShortTime,          // �Z����
        [Question("���T�[�r�X")]
        Care,               // ���
        [Question("�Ō�T�[�r�X")]
        Nurse,              // �Ō�
        [Question("���ʗ{��T�[�r�X")]
        SpecialNurseCare,   // ���ʗ{��
        [Question("�����⏕�T�[�r�X")]
        Bath,               // ����
        [Question("�n�斧���^")]
        Small,              // �������T�C�Y
        [Question("�a�@�t��")]
        Hospital            // �a�@�t��
    }

}