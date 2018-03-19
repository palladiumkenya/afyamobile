using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Smart Card", LaunchMode = LaunchMode.SingleTop, ParentActivity = typeof(AppDashboardActivity), WindowSoftInputMode = SoftInput.StateHidden)]
    public class SmartCardActivity : MvxAppCompatActivity<SmartCardViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SmartCardView);
            ViewModel.ReadCardAction = () => { ReadSmartCard(); };
        }

        private void ReadSmartCard()
        {
            Intent intent = new Intent();
            intent.SetAction("org.kenyahmis.psmart.ACTION_READ_DATA");
            intent.SetType("text/plain");
            intent.PutExtra("AUTH_TOKEN", "123");
            //intent.PutExtra("WRITE_DATA", ViewModel.Text);

            if (intent.ResolveActivity(PackageManager) != null)
            {
                StartActivityForResult(intent, 99);
                //Toast.MakeText(ApplicationContext, "Started Read smartcard intent",ToastLength.Long).Show();
            }
            else
            {
                //Toast.MakeText(ApplicationContext, "Cannot resolve intent", ToastLength.Long).Show();
            }
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            ViewModel.ShrMessage = string.Empty;
            ViewModel.ShrException = null;

            if (null == data)
            {
                ViewModel.ShrException = new Exception("Reader could not read card");
                ViewModel.ReadCardDone();
                return;
            }

            switch (resultCode)
            {
                case Result.Ok:
                    var shr = data.GetStringExtra("message");
                    if (!string.IsNullOrWhiteSpace(shr))
                    {
                        ViewModel.ShrMessage = shr;
                        ViewModel.ReadCardDone();
                        return;
                    }

                    ViewModel.ShrException = new Exception("No data found in card");
                    
                    break;
                case Result.Canceled:
                    var shrErrors = data.GetStringArrayListExtra("errors");
                    if (null != shrErrors && shrErrors.Count > 0)
                    {
                        ViewModel.ShrException = new Exception("Error reading card");
                        ViewModel.ShrErrors = shrErrors as List<string>;
                        ViewModel.ReadCardDone();
                        return;
                    }
                    
                    ViewModel.ShrException = new Exception("unkown Error reading card");
                    
                    break;
                default:
                    ViewModel.ShrException = new Exception("unkown results from the Reader");
                    break;
            }

            ViewModel.ReadCardDone();
        }
    }
}