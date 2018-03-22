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
            ViewModel.WriteCardAction = () => { WriteSmartCard(); };
        }

        private void WriteSmartCard()
        {
            Intent intent = new Intent();
            intent.SetAction("org.kenyahmis.psmart.ACTION_WRITE_DATA");
            intent.SetType("text/plain");
            intent.PutExtra("AUTH_TOKEN", "123");
            intent.PutExtra("WRITE_DATA", ViewModel.ShrMessage);

            if (intent.ResolveActivity(PackageManager) != null)
            {
                StartActivityForResult(intent, 99);
            }
            else
            {
                ViewModel.ShrException = new Exception("PSmart App not Installed");
                ViewModel.WriteCardDone();
            }
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
            }
            else
            {
                ViewModel.ShrException = new Exception("PSmart App not Installed");
                ViewModel.ReadCardDone();
            }
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            ViewModel.ShrMessage = ViewModel.ShrWriteResponse= string.Empty;
            ViewModel.ShrException = null;

            //WRITE
            if (ViewModel.ShrMode == "read")
            {
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
            else
            {
                if (null == data)
                {
                    ViewModel.ShrException = new Exception("Reader could not write card");
                    ViewModel.WriteCardDone();
                    return;
                }

                switch (resultCode)
                {
                    case Result.Ok:
                        var shr = data.GetStringExtra("message");
                        if (!string.IsNullOrWhiteSpace(shr))
                        {
                            ViewModel.ShrWriteResponse = shr;
                            ViewModel.WriteCardDone();
                            return;
                        }

                        ViewModel.ShrException = new Exception("No data found in card");

                        break;
                    case Result.Canceled:
                        var shrErrors = data.GetStringArrayListExtra("errors");
                        if (null != shrErrors && shrErrors.Count > 0)
                        {
                            ViewModel.ShrException = new Exception("Error writing card");
                            ViewModel.ShrErrors = shrErrors as List<string>;
                            ViewModel.WriteCardDone();
                            return;
                        }

                        ViewModel.ShrException = new Exception("unkown Error writing card");

                        break;
                    default:
                        ViewModel.ShrException = new Exception("unkown results from the Reader");
                        break;
                }

                ViewModel.WriteCardDone();
            }
            //READ

            

            
        }
    }
}