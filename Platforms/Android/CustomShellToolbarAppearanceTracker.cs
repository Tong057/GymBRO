using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace GymBro
{
	internal class CustomShellToolbarAppearanceTracker(IShellContext shellContext) : ShellToolbarAppearanceTracker(shellContext)
	{
		private readonly IShellContext shellContext = shellContext;

        public override void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            base.SetAppearance(toolbar, toolbarTracker, appearance);
            if (Shell.GetNavBarIsVisible(shellContext.Shell.CurrentPage))
            {
                var backgroundDrawable = new GradientDrawable();
                backgroundDrawable.SetShape(ShapeType.Rectangle);
                backgroundDrawable.SetColor(123);
                backgroundDrawable.SetCornerRadii(new float[] { 0, 0, 0, 0, 50, 50, 50, 50 });
                backgroundDrawable.SetColor(appearance.BackgroundColor.ToPlatform());
                toolbar.SetBackground(backgroundDrawable);
            }
        }
    }
}