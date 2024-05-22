namespace RUMMY_SCOREKEEPER.Views;

public partial class NewGamePage : ContentPage
{

    private NewGameViewModel currentViewModel;
    public NewGamePage(NewGameViewModel vm)
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


    public void SetNameTextFocus(object? sender, EventArgs? e)
    {
        NameText.Focus();

        if (NameText.IsSoftKeyboardShowing() == false)
        {
            NameText.ShowKeyboardAsync();
        }
    }


    public void OnPageLoaded(object sender, EventArgs e)
    {
        SetNameTextFocus(sender, e);
    }
}