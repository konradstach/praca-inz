using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace FindTheMonkey.Droid
{
    [Activity(Label = "NavigateActivityFromRoom2")]
    public class NavigateActivityFromRoom2 : Activity
    {
        Button navigateFromRoom2ToRoom1Button, navigateFromRoom2ToEntranceButton, backFromRoom2FromNavigateButton;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NavigateLayout);

            navigateFromRoom2ToRoom1Button = FindViewById<Button>(Resource.Id.navigateFromRoom2ToRoom1Button);
            navigateFromRoom2ToRoom1Button.Click += navigateFromRoom2ToRoom1ButtonClick;

            navigateFromRoom2ToEntranceButton = FindViewById<Button>(Resource.Id.navigateFromRoom2ToEntranceButton);
            navigateFromRoom2ToEntranceButton.Click += navigateFromRoom2ToEntranceButtonClick;

            backFromRoom2FromNavigateButton = FindViewById<Button>(Resource.Id.backFromRoom2FromNavigateButton);
            backFromRoom2FromNavigateButton.Click += backFromNavigateButtonClick;
        }

        private void navigateFromRoom2ToRoom1ButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(NavigateToActivity));
            Intent.PutExtra("destination", "Room2-Room1");
            StartActivity(Intent);
            Finish();
        }

        private void navigateFromRoom2ToEntranceButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(NavigateToActivity));
            Intent.PutExtra("destination", "Room2-Entrance");
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