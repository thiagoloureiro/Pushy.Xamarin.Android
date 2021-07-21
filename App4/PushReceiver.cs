using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using ME.Pushy.Sdk;

namespace App4
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    [IntentFilter(new[] { "pushy.me" })]
    public class PushReceiver : BroadcastReceiver
    {
        [System.Obsolete]
        public override void OnReceive(Context context, Intent intent)
        {
            string notificationTitle = "MyApp";
            string notificationText = "Test notification";

            // Attempt to extract the "message" property from the payload: {"message":"Hello World!"}
            if (intent.GetStringExtra("message") != null)
            {
                notificationText = intent.GetStringExtra("message");
            }

            // Prepare a notification with vibration, sound and lights
            var builder = new Notification.Builder(context)
                  .SetAutoCancel(true)
                  .SetSmallIcon(Android.Resource.Drawable.IcDialogInfo)
                  .SetContentTitle(notificationTitle)
                  .SetContentText(notificationText)
                  .SetLights(Color.Red, 1000, 1000)
                  .SetVibrate(new long[] { 0, 400, 250, 400 })
                  .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                  .SetContentIntent(PendingIntent.GetActivity(context, 0, new Intent(context, typeof(MainActivity)), PendingIntentFlags.UpdateCurrent));

            // Automatically configure a Notification Channel for devices running Android O+
            Pushy.SetNotificationChannel(builder, context);

            // Get an instance of the NotificationManager service
            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            // Build the notification and display it
            notificationManager.Notify(1, builder.Build());
        }
    }
}