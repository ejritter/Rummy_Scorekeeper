namespace RUMMY_SCOREKEEPER.Views;

public partial class PopupCalculatorPage : Popup 
{
	public PopupCalculatorPage(string player, [CallerMemberNameAttribute] string method = "")
	{
		InitializeComponent();

		if(method != null)
		{
			if(method.Equals("AddScore"))
			{
				PopupCalculatorLabel.Text = string.Format("Add to {0}", player);
				var input = PopupCalculatorEntry.Text;
            }

		}
	}
}