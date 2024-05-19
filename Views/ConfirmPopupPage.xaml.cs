namespace RUMMY_SCOREKEEPER.Views;

public partial class ConfirmPopupPage : Popup
{
	public  ConfirmPopupPage(ConfirmPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    async void Selection(object sender, EventArgs e)
    {
        if(sender is Button button)
        {
            if (button.ClassId == "Hold")
            {
                await CloseAsync(false);
            }

            if (button.ClassId == "Fold")
            {
                await CloseAsync(true);
            }
        }

    }

}