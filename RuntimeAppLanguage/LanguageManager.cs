using Android.Content;
using Android.OS;

using Java.Util;

namespace RuntimeAppLanguage
{
    internal class LanguageManager
    {
        private const string MYLANGUAGE = "myLanguage";
        private const string MYPREF = "myPreference";

        public static Context LoadLanguage(Context context)
        {
            var loadedLanguage = GetLanguage(context, Locale.Default.Language);
            return ChangeLanguage(context, loadedLanguage);
        }

        public static Context ChangeLanguage(Context context, string language)
        {
            SaveLanguage(context, language);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                return ChangeForAPI24(context, language);
            }
            return ChangeForLegacy(context, language);
        }

        private static string GetLanguage(Context context, string Language)
        {
            var privatePreference = context.GetSharedPreferences(MYPREF, FileCreationMode.Private);
            return privatePreference.GetString(MYLANGUAGE, Language);
        }

        private static void SaveLanguage(Context context, string language)
        {
            var privatePreference = context.GetSharedPreferences(MYPREF, FileCreationMode.Private);
            var editor = privatePreference.Edit();
            editor.PutString(MYLANGUAGE, language);
            editor.Apply();
        }

        private static Context ChangeForAPI24(Context context, string language)
        {
            // for api >= 24
            var locale = new Locale(language);
            Locale.Default = locale;
            var configuration = context.Resources.Configuration;
            configuration.SetLocale(locale);
            configuration.SetLayoutDirection(locale);

            return context.CreateConfigurationContext(configuration);
        }

        private static Context ChangeForLegacy(Context context, string language)
        {
            var locale = new Locale(language);
            Locale.Default = locale;
            var resources = context.Resources;
            var configuration = resources.Configuration;
            configuration.Locale = locale;
            resources.UpdateConfiguration(configuration, resources.DisplayMetrics);

            return context;
        }
    }
}