namespace RUMMY_SCOREKEEPER.Views;

public partial class NewGamePage : ContentPage
{
	public NewGamePage(NewGameViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}