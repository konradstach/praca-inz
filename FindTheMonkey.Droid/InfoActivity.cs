using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FindTheMonkey.Droid
{
    public class InfoActivity : Activity
    {
        Button backFromInfoButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InfoLayout);
            backFromInfoButton = FindViewById<Button>(Resource.Id.backFromInfoButton);
            backFromInfoButton.Click += backFromInfoButtonClick;
        }

        private void backFromInfoButtonClick(object sender, EventArgs e)
        {
            Intent = new Intent(this, typeof(MainActivity));
            StartActivity(Intent);
            Finish();
        }
    }
}