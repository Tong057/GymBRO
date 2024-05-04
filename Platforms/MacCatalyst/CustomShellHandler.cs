using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace GymBro;

class CustomShellHandler : ShellRenderer
{
	protected override IShellNavBarAppearanceTracker CreateNavBarAppearanceTracker()
	{
		return new CustomShellToolbarAppearanceTracker(this, base.CreateNavBarAppearanceTracker());
	}
}