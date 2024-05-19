
namespace RUMMY_SCOREKEEPER.Views;

public partial class CurrentGamePage : ContentPage
{
	private CurrentGameViewModel currentViewModel;
	public CurrentGamePage(CurrentGameViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        currentViewModel = vm;
	}



	public void EntryFocus(object sender, FocusEventArgs e)
	{
		if (sender is Entry entry)
		{
            currentViewModel.SetEntry(entry);
        }
		
	}


    public void EntryUnfocus(object sender, FocusEventArgs e)
    {
		if (sender is Entry entry)
		{
            currentViewModel.ClearEntry();
        }
    }

}