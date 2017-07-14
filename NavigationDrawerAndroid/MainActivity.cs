using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;

namespace NavigationDrawerAndroid
{
    [Activity(Label = "NavigationDrawerAndroid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
            HomeFragment home = new HomeFragment();
            transaction.Add(Resource.Id.framelayout, home).Commit();

            var drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var naviview = FindViewById<NavigationView>(Resource.Id.nav_view);
            naviview.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);
                FragmentTransaction transaction1 = this.FragmentManager.BeginTransaction();
                switch (e.MenuItem.TitleFormatted.ToString())
                {
                    case "Home":
                        transaction1.Replace(Resource.Id.framelayout, home).AddToBackStack(null).Commit();
                        break;

                    case "Video":
                        VideoFragment video = new VideoFragment();
                        transaction1.Replace(Resource.Id.framelayout, video).AddToBackStack(null).Commit();
                        break;
                }
                drawerLayout.CloseDrawers();
            };
        }
    }
}