using System.Windows;
using MaterialDesignThemes.Wpf;

namespace TaskManagerApp
{
    public partial class App : Application
    {
        public static bool IsDarkTheme { get; private set; }

        public static void ApplyTheme(bool dark)
        {
            IsDarkTheme = dark;
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(dark ? BaseTheme.Dark : BaseTheme.Light);
            paletteHelper.SetTheme(theme);
        }
    }
}