using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Media;
using static Android.Media.MediaPlayer;

namespace NavigationDrawerAndroid
{
    public class VideoFragment : Fragment, IOnPreparedListener
    {
        private VideoView videoView;
        private MediaPlayer mediaPlayer;
        private MediaController mediaController;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.videolayout, container, false);
            videoView = view.FindViewById<VideoView>(Resource.Id.videoview);
            videoView.Prepared += VideoView_Prepared;
            mediaController = new MediaController(this.Activity, true);
            return view;
        }

        private void StartVideo()
        {
            mediaController.SetAnchorView(videoView);
            mediaController.SetMediaPlayer(videoView);
            String fileName = "android.resource://" + this.Activity.BaseContext.PackageName + "/raw/one";
            videoView.SetVideoURI(Android.Net.Uri.Parse(fileName));
            videoView.Start();
        }

        private void VideoView_Prepared(object sender, EventArgs e)
        {
            mediaController.Show(2000);
        }

        public override void OnStart()
        {
            base.OnStart();
            StartVideo();
            videoView.SetOnPreparedListener(this);
            videoView.Touch += (sender, e) =>
            {
                if (!(e.Event.Action == MotionEventActions.Down))
                    return;
                if (!mediaController.IsShown)
                    mediaController.Show();
                else
                    mediaController.Hide();
            };

            videoView.Completion += (sender, e) =>
            {
                //base.OnBackPressed();
            };
        }

        public override void OnStop()
        {
            base.OnStop();
            videoView.Prepared -= VideoView_Prepared;
        }

        public void OnPrepared(MediaPlayer mp)
        {
        }
    }
}