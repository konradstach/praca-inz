using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace FindTheMonkey.Droid
{
    [Activity(Label = "NavigateActivityFromRoom1")]
    public class NavigateActivityFromRoom1 : Activity
    {
        Button navigateFromRoom1ToRoom2Button, navigateFromRoom1ToEntranceButton, backFromRoom1FromNavigateButton;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NavigateLayout);

            navigateFromRoom1ToRoom2Button = FindViewById<Button>(Resource.Id.navigateFromRoom1ToRoom2Button);
            navigateFromRoom1ToRoom2Button.Click += navigateFromRoom1ToRoom2ButtonClick;

            navigateFromRoom1ToEntranceButton = FindViewById<Button>(Resource.Id.navigateFromRoom1ToEntranceButton);
            navigateFromRoom1ToEntranceButton.Click += navigateFromRoom1ToEntranceButtonClick;

            backFromRoom1FromNavigateButton = FindViewById<Button>(Resource.Id.backFromRoom1FromNavigateButton);
            backFromRoom1FromNavigateButton.Click += backFromNavigateButtonClick;
        }

        private void navigateFromRoom1ToRoom2ButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(NavigateToActivity));
            Intent.PutExtra("destination", "Room1-Room2");
            StartActivity(Intent);
            Finish();
        }

        private void navigateFromRoom1ToEntranceButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(NavigateToActivity));
            Intent.PutExtra("destination", "Room1-Room2");
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