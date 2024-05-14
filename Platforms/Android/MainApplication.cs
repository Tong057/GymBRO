using Android.App;
using Android.Content.Res;
using Android.Runtime;

namespace GymBro
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
                if (view is Entry)
                {
                    // Change underline color for Entry
                    handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(new Android.Graphics.Color(73, 219, 117));
                }
            });

            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(Editor), (handler, view) =>
            {
                if (view is Editor)
                {
                    // Change underline color for Editor
                    handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(new Android.Graphics.Color(73, 219, 117));
                }
            });
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
