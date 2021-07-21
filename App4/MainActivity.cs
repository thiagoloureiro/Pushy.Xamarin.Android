using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using ME.Pushy.Sdk;
using System.Threading.Tasks;
using Java.Net;

namespace App4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Pushy.Listen(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            RegisterForPushNotifications();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        private async Task RegisterForPushNotifications()
        {
            // Execute Pushy.Register() in a background thread
            await Task.Run(() =>
            {
                try
                {
                    // Assign a unique token to this device
                    string deviceToken = Pushy.Register(this);

                    // Log it for debugging purposes
                    //Log.Debug("MyApp", "Pushy device token: " + deviceToken);

                    // Send the token to your backend server via an HTTP GET request
                    //new URL("https://{YOUR_API_HOSTNAME}/register/device?token=" + deviceToken).OpenConnection();
                }
                catch (Exception exc)
                {
                    // Log error to console
                    // Log.Error("MyApp", exc.Message, exc);
                    return;
                }

                // Succeeded, optionally do something to alert the user
            });
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}