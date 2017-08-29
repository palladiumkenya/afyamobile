

using System;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;
using DialogFragment = Android.Support.V4.App.DialogFragment;

namespace LiveHTS.Droid.Custom
{
    public class DatePickerFragmentV4 : DialogFragment,
        DatePickerDialog.IOnDateSetListener
    {
        private static DateTime CurrentDate;
        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(DatePickerFragmentV4).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static DatePickerFragmentV4 NewInstance(Action<DateTime> onDateSelected,DateTime currentDate)
        {
            CurrentDate = currentDate;
            DatePickerFragmentV4 frag = new DatePickerFragmentV4();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = CurrentDate;

            DatePickerDialog dialog = new DatePickerDialog(Activity,
                this,
                currently.Year,
                currently.Month,
                currently.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }
}