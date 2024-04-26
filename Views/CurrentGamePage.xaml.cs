namespace RUMMY_SCOREKEEPER.Views;

public partial class CurrentGamePage : ContentPage
{
	public CurrentGamePage(CurrentGameViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}