using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.HelperClasses
{
    public class Enums
    {
        public enum Success
        {
            [Description("تم الاضافة بنجاح"), AmbientValue(1)]
            Insert = 1
           ,
            [Description("تم النعديل بنجاح"), AmbientValue(2)]
            Update = 2
            ,
            [Description("تم الحفظ بنجاح"), AmbientValue(3)]
            Save = 3
                 ,
            [Description("تم الحفظ بنجاح"), AmbientValue(4)]
            GetData = 4
                   ,
            [Description("تم الحفظ بنجاح"), AmbientValue(5)]
            DeletedScussfuly = 5
        }
        public enum Error
        {
            [Description("Exception!")]
            Exception = -1,
            [Description("There is an error, try again!")]
            Common = 0,

            [Description("Sorry, this record is not exist!"), AmbientValue(10)]
            RecordNotExist = 10,

            [Description("حدث خطأ...حاول مرة اخرى!"), AmbientValue(11)]
            ErrorHappened = 11,

            [Description("خطأ في ادخال البيانات !"), AmbientValue(12)]
            DataValue = 12,

            [Description("هذا السجل موجود من قبل"), AmbientValue(13)]
            RepeatedData = 13,

            [Description("لايمكن الحذف للإرتباط ببيانات اخرى"), AmbientValue(14)]
            RelatedData = 14,

            [Description("أسم المستخدم مسجل من قبل"), AmbientValue(15)]
            RelatedUserName = 15,

            [Description("تأكد من البيانات المطلوبة"), AmbientValue(16)]
            CehckData = 16


        }
        public enum Custom_Messages
        {
            [Description("يوجد حاله وظيفيه للموظف فعاله يجب تغييرها!")]
            Working_State_Error = 0,
        }

        #region Android Location Data
        public enum Android_Operation
        {
            //Same Id's in DB Andriod_Operation
            [Description("لجنة فحص"), AmbientValue(1)]
            ExaminationCommitte = 1,
            [Description("سحب عينة"), AmbientValue(2)]
            SampleData = 2,
            [Description("معالجة"), AmbientValue(3)]
            Treatment = 3,
            [Description("  تأكيد لجنة فحص"), AmbientValue(4)]
            ConfirmExaminationCommitte = 4,
            [Description("تأكيد سحب عينة"), AmbientValue(5)]
            ConfirmSampleData = 5,
            [Description("تأكيد معالجة"), AmbientValue(6)]
            ConfirmTreatment = 6,
            [Description("لجنة صرف"), AmbientValue(7)]
            DismissCommittee = 7,
            [Description("لجنة استلام"), AmbientValue(8)]
            ReceiveCommittee = 8,
            [Description("لجنة إعدام"), AmbientValue(9)]
            ExecutionCommitte = 9,
            [Description("اعتماد محطة"), AmbientValue(10)]
            StationCommitte = 10,
            [Description(" تأكيد اعتماد محطة "), AmbientValue(11)]
            StationCommitte_Confirm = 11,
            [Description("اعتماد مزرعة"), AmbientValue(12)]
            FormCommitte = 12,
            [Description(" تأكيد اعتماد مزرعة "), AmbientValue(13)]
            FormCommitte_Confirm = 13,
        }
        #endregion
        public enum User_Login
        {
            [Description("  تم الدخول وتغير الباسورد"), AmbientValue(1)]
            Valid = 1
            , [Description("  تم الدخول ولم تغير الباسورد"), AmbientValue(1)]
            ValidNotPasswordChanged = 6
            ,
            [Description("خطأ فى أسم المستخدم أو كلمة المرو"), AmbientValue(2)]
            InvalidCredential = 2,

            [Description("حدث خطأ...حاول مرة اخرى!"), AmbientValue(0)]
            ErrorHappened = 0,

            [Description("تواصل مع مدير النظام التشغيل للاضافة."), AmbientValue(3)]
            ConnectManager = 3,

            [Description("تواصل مع المدير الدومين للاضافة."), AmbientValue(4)]
            ConnectDomain = 4,

            [Description("هذا المستخدم مسجل على جهاز أخر"), AmbientValue(5)]
            LoginFromAnotherDevice = 5,
        }
        public static string GetEnumDescription<T>(object value)
        {
            Type type = typeof(T);
            string name = Enum.GetName(typeof(T), value);
            var enumName = Enum.GetNames(type).Where(f => f.Equals(name, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (enumName == null)
            {
                return string.Empty;
            }
            var field = type.GetField(enumName);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : enumName;
        }

        public enum CommitteeType
        {
            [Description("لجنة فحص"), AmbientValue(1)]
            Examination_Committee = 1,
            [Description("لجنة الجشني"), AmbientValue(2)]
            Genshi_Committee = 2,
            [Description("لجنة سحب عينات(مزرعة)"), AmbientValue(3)]
            FarmSampling_Committee = 3,
            [Description("لجنة إعتماد محطة "), AmbientValue(4)]
            StationAccrediation_Committee = 4,
            [Description("لجنة إعتماد مزرعة"), AmbientValue(5)]
            FarmAccrediation_Committee = 5,
            [Description(" لجنة  معالجة "), AmbientValue(6)]
            Treatment_Committee = 6,
            [Description("لجنة الصرف "), AmbientValue(7)]
            Custody_DismissCommittee = 7,
            [Description("لجنة الاستلام"), AmbientValue(8)]
            Custody_ReceiveCommittee = 8,
            [Description("لجنة الاعدام "), AmbientValue(9)]
            Execution_Committee = 9,
            [Description("لجنة اعتماد شركه"), AmbientValue(10)]
            CompanyAccrediation_Committee = 10,
        }
    }
}
