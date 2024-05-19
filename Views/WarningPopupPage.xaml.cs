namespace RUMMY_SCOREKEEPER.Views;

public partial class WarningPopupPage : Popup
{
	public WarningPopupPage(WarningPopupViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
	}
}