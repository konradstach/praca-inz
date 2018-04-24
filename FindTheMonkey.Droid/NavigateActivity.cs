using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Media;

namespace FindTheMonkey.Droid
{
    [Activity(Label = "NavigateActivity")]
    public class NavigateActivity : Activity
    {
        Button navigateToRoom1Button, navigateToRoom2Button, backFromNavigateButton;
        MediaPlayer _player;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.NavigateLayout);

            _player = MediaPlayer.Create(this, Resource.NavigateActivitySound);
            _player.Start();

            navigateToRoom1Button = FindViewById<Button>(Resource.Id.navigateToRoom1Button);
            navigateToRoom1Button.Click += navigateToRoom1ButtonClick;

            navigateToRoom2Button = FindViewById<Button>(Resource.Id.navigateToRoom2Button);
            navigateToRoom2Button.Click += navigateToRoom2ButtonClick;

            backFromNavigateButton = FindViewById<Button>(Resource.Id.backFromNavigateButton);
            backFromNavigateButton.Click += backFromNavigateButtonClick;
        }

        private void navigateToRoom1ButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(NavigateToActivity));
            Intent.PutExtra("destination", "Entrance-Room1");
            StartActivity(Intent);
            Finish();
        }

        private void navigateToRoom2ButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(NavigateToActivity));
            Intent.PutExtra("destination", "Entrance-Room2");
            StartActivity(Intent);
            Finish();
        }

        private void backFromNavigateButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(MainActivity));
            StartActivity(Intent);
            Finish();
        }
    }
}