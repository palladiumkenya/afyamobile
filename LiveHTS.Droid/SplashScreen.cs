using System;
using System.IO;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid
{
    [Activity(
        
         MainLauncher = true
        , Icon = "@mipmap/icon"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
            
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            UserDialogs.Init(this);
            CopyDatabaseAsync(this).ContinueWith(t =>
            {
                if (t.Status != TaskStatus.RanToCompletion)
                    return;

                //your code here
            });
        }

        private async Task CopyDatabaseAsync(Activity activity)
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "livehtsmeta.db");
            if (!File.Exists(dbPath))
            {
                try
                {
                    using (var dbAssetStream = activity.Assets.Open("livehtsmeta.db"))
                    using (var dbFileStream = new FileStream(dbPath, FileMode.OpenOrCreate))
                    {
                        var buffer = new byte[1024];

                        int b = buffer.Length;
                        int length;

                        while ((length = await dbAssetStream.ReadAsync(buffer, 0, b)) > 0)
                        {
                            await dbFileStream.WriteAsync(buffer, 0, length);
                        }

                        dbFileStream.Flush();
                        dbFileStream.Close();
                        dbAssetStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    //Handle exceptions
                }
            }
        }
    }
}
