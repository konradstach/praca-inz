using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using RadiusNetworks.IBeaconAndroid;

namespace FindTheMonkey.Droid
{
    [Activity(Label = "NavigateActivity")]
    public class NavigateToActivity : Activity, IBeaconConsumer
    {

        bool _paused;
        View _view;
        IBeaconManager _iBeaconManager;
        MonitorNotifier _monitorNotifier;
        RangeNotifier _rangeNotifier;
        List<Region> _monitoringRegionsList;
        List<Region> _rangingRegionsList;

        int _previousProximity;
        private string _actualBeaconID = "";
        private string _actualSoundString = "";
        private string _destination = "";

        Dictionary<string, string> Entrance_Room1 = new Dictionary<string, string>
        {
            {"f7826da6-4fa2-4e98-8024-bc5b71e08931","file:///sdcard/praca/1.mp3"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08932","file:///sdcard/praca/2.mp3"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08933","file:///sdcard/praca/3.mp3"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08934","file:///sdcard/praca/4.mp3"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08935","file:///sdcard/praca/5.mp3"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08936","file:///sdcard/praca/6.mp3"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08937","file:///sdcard/praca/7.mp3"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08938","file:///sdcard/praca/8.mp3"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08939","file:///sdcard/praca/9.mp3"},
        };

        Dictionary<string, string> Entrance_Room2 = new Dictionary<string, string>
        {
            {"f7826da6-4fa2-4e98-8024-bc5b71e08931","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08932","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08933","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08934","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08935","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08936","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08937","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08938","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08939","sciezka"},
        };

        Dictionary<string, string> Room1_Entrance = new Dictionary<string, string>
        {
            {"f7826da6-4fa2-4e98-8024-bc5b71e08931","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08932","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08933","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08934","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08935","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08936","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08937","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08938","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08939","sciezka"},
        };

        Dictionary<string, string> Room1_Room2 = new Dictionary<string, string>
        {
            {"f7826da6-4fa2-4e98-8024-bc5b71e08931","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08932","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08933","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08934","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08935","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08936","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08937","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08938","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08939","sciezka"},
        };

        Dictionary<string, string> Room2_Entrance = new Dictionary<string, string>
        {
            {"f7826da6-4fa2-4e98-8024-bc5b71e08931","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08932","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08933","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08934","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08935","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08936","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08937","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08938","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08939","sciezka"},
        };

        Dictionary<string, string> Room2_Room1 = new Dictionary<string, string>
        {
            {"f7826da6-4fa2-4e98-8024-bc5b71e08931","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08932","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08933","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08934","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08935","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08936","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08937","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08938","sciezka"},
            {"f7826da6-4fa2-4e98-8024-bc5b71e08939","sciezka"},
        };

        private string GetBeaconInRangeSoundString(string uuid, Dictionary<string, string> dictionaryToResearch)
        {
            foreach (var dictionaryElement in dictionaryToResearch)
            {
                if (dictionaryElement.Key == uuid)
                    return dictionaryElement.Value;
            }

            return null;
        }


        Dictionary<string, string> GiveMeDictionary(string _destination)
        {
            switch (_destination)
            {
                case "Entrance-Room1":
                    return Entrance_Room1;

                case "Entrance-Room2":
                    return Entrance_Room2;

                case "Room1-Entrance":
                    return Room1_Entrance;

                case "Room1-Room2":
                    return Room1_Room2;

                case "Room2-Entrance":
                    return Room2_Entrance;

                case "Room2-Room1":
                    return Room2_Room1;

                default:
                    return null;
            }
        }

        public NavigateToActivity()
        {
            _iBeaconManager = IBeaconManager.GetInstanceForApplication(this);

            _monitorNotifier = new MonitorNotifier();
            _rangeNotifier = new RangeNotifier();

            _monitoringRegionsList = new List<Region>()
            {
                new Region("1", "f7826da6-4fa2-4e98-8024-bc5b71e08931", (Java.Lang.Integer)13138, (Java.Lang.Integer)34361),
                new Region("2", "f7826da6-4fa2-4e98-8024-bc5b71e08932", (Java.Lang.Integer)3271, (Java.Lang.Integer)18626),
                new Region("3", "f7826da6-4fa2-4e98-8024-bc5b71e08933", (Java.Lang.Integer)4067, (Java.Lang.Integer)4146),
                new Region("4", "f7826da6-4fa2-4e98-8024-bc5b71e08934", (Java.Lang.Integer)62074, (Java.Lang.Integer)10187),
                new Region("5", "f7826da6-4fa2-4e98-8024-bc5b71e08935", (Java.Lang.Integer)18869, (Java.Lang.Integer)15746),
                new Region("6", "f7826da6-4fa2-4e98-8024-bc5b71e08936", (Java.Lang.Integer)56526, (Java.Lang.Integer)64981),
                new Region("7", "f7826da6-4fa2-4e98-8024-bc5b71e08937", (Java.Lang.Integer)26265, (Java.Lang.Integer)32892),
                new Region("8", "f7826da6-4fa2-4e98-8024-bc5b71e08938", (Java.Lang.Integer)10484, (Java.Lang.Integer)33855),
                new Region("9", "f7826da6-4fa2-4e98-8024-bc5b71e08939", (Java.Lang.Integer)42751, (Java.Lang.Integer)26202),

            };

            _rangingRegionsList = new List<Region>()
            {
                new Region("1", "f7826da6-4fa2-4e98-8024-bc5b71e08931", (Java.Lang.Integer)13138, (Java.Lang.Integer)34361),
                new Region("2", "f7826da6-4fa2-4e98-8024-bc5b71e08932", (Java.Lang.Integer)3271, (Java.Lang.Integer)18626),
                new Region("3", "f7826da6-4fa2-4e98-8024-bc5b71e08933", (Java.Lang.Integer)4067, (Java.Lang.Integer)4146),
                new Region("4", "f7826da6-4fa2-4e98-8024-bc5b71e08934", (Java.Lang.Integer)62074, (Java.Lang.Integer)10187),
                new Region("5", "f7826da6-4fa2-4e98-8024-bc5b71e08935", (Java.Lang.Integer)18869, (Java.Lang.Integer)15746),
                new Region("6", "f7826da6-4fa2-4e98-8024-bc5b71e08936", (Java.Lang.Integer)56526, (Java.Lang.Integer)64981),
                new Region("7", "f7826da6-4fa2-4e98-8024-bc5b71e08937", (Java.Lang.Integer)26265, (Java.Lang.Integer)32892),
                new Region("8", "f7826da6-4fa2-4e98-8024-bc5b71e08938", (Java.Lang.Integer)10484, (Java.Lang.Integer)33855),
                new Region("9", "f7826da6-4fa2-4e98-8024-bc5b71e08939", (Java.Lang.Integer)42751, (Java.Lang.Integer)26202),
            };
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _iBeaconManager.Bind(this);

            _monitorNotifier.EnterRegionComplete += EnteredRegion;
            _monitorNotifier.ExitRegionComplete += ExitedRegion;

            _rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;

            _destination = Intent.GetStringExtra("destination");
        }

        protected override void OnResume()
        {
            base.OnResume();
            _paused = false;
        }

        protected override void OnPause()
        {
            base.OnPause();
            _paused = true;
        }

        void EnteredRegion(object sender, MonitorEventArgs e)
        {
            if (_paused)
            {
                ShowNotification();
            }
        }

        void ExitedRegion(object sender, MonitorEventArgs e)
        {
        }

        void RangingBeaconsInRegion(object sender, RangeEventArgs e)
        {
            if (e.Beacons.Count > 0)
            {

                var beacon = e.Beacons.FirstOrDefault();
                Console.WriteLine(beacon.ProximityUuid);
                var message = string.Empty;
                _actualSoundString = GetBeaconInRangeSoundString(beacon.ProximityUuid, GiveMeDictionary(_destination));

                switch ((ProximityType)beacon.Proximity)
                {
                    case ProximityType.Immediate:
                        UpdateDisplay(beacon.ProximityUuid);
                        break;

                }

                _previousProximity = beacon.Proximity;
            }

            if (IsDestinationReached(GiveMeDestinationBeaconUuid(), beacon.ProximityUuid))

                GetProperActivity();
        }

        #region IBeaconConsumer impl
        public void OnIBeaconServiceConnect()
        {
            _iBeaconManager.SetMonitorNotifier(_monitorNotifier);
            _iBeaconManager.SetRangeNotifier(_rangeNotifier);

            foreach (var monitoringRegion in _monitoringRegionsList)
                _iBeaconManager.StartMonitoringBeaconsInRegion(monitoringRegion);

            foreach (var rangingRegion in _rangingRegionsList)
                _iBeaconManager.StartRangingBeaconsInRegion(rangingRegion);
        }
        #endregion

        private void UpdateDisplay(string beaconID)
        {
            RunOnUiThread(() =>
            {

                if (beaconID != _actualBeaconID)
                {
                    ShowNotification();
                    _actualBeaconID = beaconID;
                }
            });
        }

       
        private void ShowNotification()
        {
            var audiobuilder = new Android.Net.Uri.Builder();
            var audio = Android.Net.Uri.Parse(_actualSoundString);
            var resultIntent = new Intent(this, typeof(MainActivity));
            resultIntent.AddFlags(ActivityFlags.ReorderToFront);
            var pendingIntent = PendingIntent.GetActivity(this, 0, resultIntent, PendingIntentFlags.UpdateCurrent);
            var notificationId = Resource.String.monkey_notification;

            var builder = new NotificationCompat.Builder(this)
                .SetSmallIcon(Resource.Drawable.Xamarin_Icon)
                .SetContentTitle(this.GetText(Resource.String.app_label))
                .SetContentText(this.GetText(Resource.String.monkey_notification))
                .SetContentIntent(pendingIntent)
                .SetAutoCancel(true)
                .SetSound(audio);


            var notification = builder.Build();



            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(notificationId, notification);
        }

        private bool IsDestinationReached(string beaconUuid, string actualBeaconInRangeUuid)
        {
            if (beaconUuid == actualBeaconInRangeUuid)
                return true;
            return false;
        }

        private void GetProperActivity()
        {
            if (_destination == "Entrance-Room1")
            {
                Intent = new Intent(this, typeof(NavigateActivityFromRoom1));
                Intent.PutExtra("destination", "Entrance-Room1");
                StartActivity(Intent);
            }

            if (_destination == "Entrance-Room2")
            {
                Intent = new Intent(this, typeof(NavigateActivityFromRoom2));
                Intent.PutExtra("destination", "Entrance-Room2");
                StartActivity(Intent);
            }

            if (_destination == "Room1-Entrance")
            {
                Intent = new Intent(this, typeof(NavigateActivity));
                Intent.PutExtra("destination", "Room1-Entrance");
                StartActivity(Intent);
            }

            if (_destination == "Room1-Room2")
            {
                Intent = new Intent(this, typeof(NavigateActivityFromRoom2));
                Intent.PutExtra("destination", "Room1-Room2");
                StartActivity(Intent);
            }

            if (_destination == "Room2-Entrance")
            {
                Intent = new Intent(this, typeof(NavigateActivity));
                Intent.PutExtra("destination", "Room2-Entrance");
                StartActivity(Intent);
            }

            if (_destination == "Room2-Room1")
            {
                Intent = new Intent(this, typeof(NavigateActivityFromRoom1));
                Intent.PutExtra("destination", "Room2-Room1");
                StartActivity(Intent);
            }

        }

        private string GiveMeDestinationBeaconUuid()
        {
            {
                if (_destination == "Entrance-Room1")
                    return "f7826da6-4fa2-4e98-8024-bc5b71e08933";
            }

            {
                if (_destination == "Entrance-Room2")
                    return "f7826da6-4fa2-4e98-8024-bc5b71e08939";
            }

            {
                if (_destination == "Room1-Room2")
                    return "f7826da6-4fa2-4e98-8024-bc5b71e08939";
            }

            {
                if (_destination == "Room1-Entrance")
                    return "f7826da6-4fa2-4e98-8024-bc5b71e08931";
            }

            {
                if (_destination == "Room2-Room1")
                    return "f7826da6-4fa2-4e98-8024-bc5b71e08933";
            }

            {
                if (_destination == "Room2-Entrance")
                    return "f7826da6-4fa2-4e98-8024-bc5b71e08931";
            }
            return "f7826da6-4fa2-4e98-8024-bc5b71e08931";
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();

            _monitorNotifier.EnterRegionComplete -= EnteredRegion;
            _monitorNotifier.ExitRegionComplete -= ExitedRegion;

            _rangeNotifier.DidRangeBeaconsInRegionComplete -= RangingBeaconsInRegion;

            foreach (var monitoringRegion in _monitoringRegionsList)
                _iBeaconManager.StopMonitoringBeaconsInRegion(monitoringRegion);

            foreach (var rangingRegion in _rangingRegionsList)
                _iBeaconManager.StopRangingBeaconsInRegion(rangingRegion);
            _iBeaconManager.UnBind(this);
        }
    }
}