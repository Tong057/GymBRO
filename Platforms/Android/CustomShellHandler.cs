using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace GymBro
{
	internal class CustomShellHandler : ShellRenderer
	{
		protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
		{
			return new CustomShellToolbarAppearanceTracker(this);
		}

		protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
		{
			return new CustomShellSectionRenderer(this);
		}
	}
}