using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Android.Media;

namespace FindTheMonkey.Droid
{
    [Activity(Label = "Praca inzynierska", MainLauncher = true, LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : Activity
    {
    
        Button infoButton;
        Button appExitButton;
        Button navigateToButton;
        MediaPlayer _player;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            _player = MediaPlayer.Create(this, Resource.MainActivitySound);
            _player.Start();

            infoButton = FindViewById<Button>(Resource.Id.infoButton);
            infoButton.Click += infoButtonClick;

            appExitButton = FindViewById<Button>(Resource.Id.appExitButton);
            appExitButton.Click += appExitButtonClick;

            navigateToButton = FindViewById<Button>(Resource.Id.navigateToButton);
            navigateToButton.Click += navigateToButtonClick;
                      
        }

        private void infoButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(InfoActivity));
            StartActivity(Intent);
        }

        private void navigateToButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(NavigateActivity));
            StartActivity(Intent);
        }

        private void appExitButtonClick(object sender, EventArgs e)
        {
            Process.KillProcess(Process.MyPid());
        }


        
    }
}