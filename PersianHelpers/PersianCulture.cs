using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersianHelpers
{
    public class PersianCulture : CultureInfo
    {
        public PersianCulture()
            : base("fa-IR")
        {
            PersianCultureHelper.SetPersianOptions(this);
        }
    }

    internal static class PersianCultureHelper
    {
        //Represents a PropertyInfo that refer to ID of Calendar. The ID is private property of Calendar.
        private static PropertyInfo calendarID;

        //Represents a FieldInfo that refer to m_isReadOnly of CultureInfo. The m_isReadOnly is private filed of CultureInfo.
        private static FieldInfo cultureInfoReadOnly;

        //Represents a FieldInfo that refer to calendar of CultureInfo. The calendar is private filed of CultureInfo.
        private static FieldInfo cultureInfoCalendar;

        //Represents a FieldInfo that refer to isReadOnly of NumberFormatInfo. The isReadOnly is private filed of NumberFormatInfo.
        private static FieldInfo numberFormatInfoReadOnly;

        //Represents a FieldInfo that refer to m_isReadOnly of DateTimeFormatInfo. The m_isReadOnly is private filed of DateTimeFormatInfo.
        private static FieldInfo dateTimeFormatInfoReadOnly;

        //Represents a FieldInfo that refer to calendar of DateTimeFormatInfo. The calendar is private filed of DateTimeFormatInfo.
        private static FieldInfo dateTimeFormatInfoCalendar;

        //Represents a FieldInfo that refer to m_cultureTableRecord of DateTimeFormatInfo. The m_cultureTableRecord is private filed of DateTimeFormatInfo.
        private static FieldInfo dateTimeFormatInfoCultureTableRecord;

        //Represents a MethodInfo that refer to UseCurrentCalendar of CultureTableRecord. The UseCurrentCalendar is private method of CultureTableRecord that the class is private too.
        private static MethodInfo cultureTableRecordUseCurrentCalendar;

        /// <summary>
        /// Represents static constructor
        /// </summary>
        static PersianCultureHelper()
        {
            calendarID = typeof(System.Globalization.Calendar).GetProperty("ID", BindingFlags.NonPublic | BindingFlags.Instance);
            cultureInfoReadOnly = typeof(CultureInfo).GetField("m_isReadOnly", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            cultureInfoCalendar = typeof(CultureInfo).GetField("calendar", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            numberFormatInfoReadOnly = typeof(NumberFormatInfo).GetField("isReadOnly", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            dateTimeFormatInfoCalendar = typeof(DateTimeFormatInfo).GetField("calendar", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            dateTimeFormatInfoReadOnly = typeof(DateTimeFormatInfo).GetField("m_isReadOnly", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            //dateTimeFormatInfoCultureTableRecord = typeof(DateTimeFormatInfo).GetField("m_cultureTableRecord", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            Type cultureTableRecord = typeof(DateTimeFormatInfo).Assembly.GetType("System.Globalization.CultureTableRecord");
            //cultureTableRecordUseCurrentCalendar = cultureTableRecord.GetMethod("UseCurrentCalendar", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        /// Represents a method that set PersianCalendar to specified instances of CultureInfo
        /// </summary>
        /// <param name="culture">Represents an instance of CultureInfo that persian number format should be set to it.</param>
        public static void SetPersianOptions(CultureInfo culture)
        {
            SetPersianCalendar(culture, new DateTimeFormatInfo());
        }

        /// <summary>
        /// Represents a method that set PersianCalendar to specified instances of CultureInfo and DateTimeFormatInfo
        /// </summary>
        /// <param name="culture">Represents an instance of CultureInfo that persian number format should be set to it.</param>
        /// <param name="dateTimeFormat">Represents an instance of DateTimeFormatInfo that persian format should be set to it.</param>
        public static void SetPersianCalendar(CultureInfo culture, DateTimeFormatInfo dateTimeFormat)
        {
            if (culture == null || culture.LCID != 1065)
                return;
            PersianCalendar calendar = new PersianCalendar();
            bool readOnly = (bool)cultureInfoReadOnly.GetValue(culture);
            if (readOnly)
            {
                cultureInfoReadOnly.SetValue(culture, false);
            }
            culture.DateTimeFormat = dateTimeFormat;
            InitPersianDateTimeFormat(dateTimeFormat);
            cultureInfoCalendar.SetValue(culture, calendar);
            InitPersianNumberFormat(culture);
            if (readOnly)
            {
                cultureInfoReadOnly.SetValue(culture, true);
            }
        }

        /// <summary>
        /// Represents a method that set persian number format to specified instance CultureInfo.
        /// </summary>
        /// <param name="info">Represents an instance of CultureInfo that persian number format should be set to it.</param>
        public static void InitPersianNumberFormat(CultureInfo info)
        {
            InitPersianNumberFormat(info.NumberFormat);
        }

        /// <summary>
        /// Represents a method that set persian number format to specified instance NumberFormatInfo.
        /// </summary>
        /// <param name="info">Represents an instance of NumberFormatInfo that persian option should be set to it.</param>
        public static void InitPersianNumberFormat(NumberFormatInfo info)
        {
            if (info == null)
                return;
            bool readOnly = (bool)numberFormatInfoReadOnly.GetValue(info);
            if (readOnly)
            {
                numberFormatInfoReadOnly.SetValue(info, false);
            }
            info.NativeDigits = new string[] { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };
            info.CurrencyDecimalSeparator = "/";
            info.CurrencySymbol = "ريال";
            if (readOnly)
            {
                numberFormatInfoReadOnly.SetValue(info, true);
            }
        }

        /// <summary>
        /// Represents a method that set persian option to specified instance CultureInfo
        /// </summary>
        /// <param name="dateTimeFormat">Represents an instance of DateTimeFormatInfo that persian option should be set to it.</param>
        public static void InitPersianDateTimeFormat(DateTimeFormatInfo info)
        {
            if (info == null)
                return;
            PersianCalendar calendar = new PersianCalendar();
            bool readOnly = (bool)dateTimeFormatInfoReadOnly.GetValue(info);
            if (readOnly)
            {
                dateTimeFormatInfoReadOnly.SetValue(info, false);
            }
            dateTimeFormatInfoCalendar.SetValue(info, calendar);
            //object obj2 = dateTimeFormatInfoCultureTableRecord.GetValue(info);
            //cultureTableRecordUseCurrentCalendar.Invoke(obj2, new object[] { calendarID.GetValue(calendar, null) });
            info.AbbreviatedDayNames = new string[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            info.ShortestDayNames = new string[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            info.DayNames = new string[] { "یکشنبه", "دوشنبه", "ﺳﻪشنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            info.AbbreviatedMonthNames = new string[] { "فروردین", "ارديبهشت", "خرداد", "تير", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            info.MonthNames = new string[] { "فروردین", "ارديبهشت", "خرداد", "تير", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            info.AMDesignator = "ق.ظ";
            info.PMDesignator = "ب.ظ";
            info.FirstDayOfWeek = DayOfWeek.Saturday;
            info.FullDateTimePattern = "yyyy MMMM dddd, dd HH:mm:ss";
            info.LongDatePattern = "yyyy MMMM dddd, dd";
            info.ShortDatePattern = "yyyy/MM/dd";
            if (readOnly)
            {
                dateTimeFormatInfoReadOnly.SetValue(info, true);
            }
        }
    }
}
