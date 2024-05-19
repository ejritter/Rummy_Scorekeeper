namespace RUMMY_SCOREKEEPER.Views;

public partial class WinningPopupPage : Popup
{
	public WinningPopupPage(WinningPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}