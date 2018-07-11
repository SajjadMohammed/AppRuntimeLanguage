using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace RuntimeAppLanguage
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        V7Toolbar mainToolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            mainToolbar = FindViewById<V7Toolbar>(Resource.Id.mainToolbar);

            //Toolbar will now take on default actionbar characteristics
            SetSupportActionBar(mainToolbar);

            SupportActionBar.Title = GetString(Resource.String.app_name);
        }

        protected override void AttachBaseContext(Context @base)
        {
            base.AttachBaseContext(LanguageManager.LoadLanguage(@base));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.language_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.arItem:
                    LanguageManager.ChangeLanguage(this, "ar");
                    Recreate();
                    return true;
                case Resource.Id.enItem:
                    LanguageManager.ChangeLanguage(this, "en");
                    Recreate();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }


}

