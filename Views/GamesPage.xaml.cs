namespace RUMMY_SCOREKEEPER.Views;

public partial class GamesPage : ContentPage
{
	
	public GamesPage(GamesViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
	}

    private void ItemTapped(object sender, TappedEventArgs args)
    {

    }
}