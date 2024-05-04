using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace GymBro;

internal class CustomShellHandler : ShellRenderer
{
	protected override IShellNavBarAppearanceTracker CreateNavBarAppearanceTracker()
	{
		return new CustomShellNavBarAppearanceTracker(this, base.CreateNavBarAppearanceTracker());
	}
}