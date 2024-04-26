namespace RUMMY_SCOREKEEPER.ViewModel;

public partial class GamesViewModel : ObservableObject
{
    public GamesViewModel()
    {
       Players = new ObservableCollection<Player>();
    }

    [ObservableProperty]
    ObservableCollection<Player> players;

    [ObservableProperty]
    string text;

    [RelayCommand]
    void AddPlayerBtn()
    {
        var enteredName = Text;

        if (string.IsNullOrWhiteSpace(enteredName))
        {
            return;
        }

        enteredName = enteredName.Trim();
        
        //if(Players.Contains(enteredName) == false)
        //{
        //    Players.Add(enteredName);

        //    Text = string.Empty;
        //}

        var player = Players.FirstOrDefault(p =>  p.Name == enteredName);   
        if (player == null)
        {
            player = new Player();
            player.Name = enteredName;
            Players.Add(player);
            
            Text = string.Empty;
        }
    }

    [RelayCommand]
    void DeletePlayerBtn(string name)
    {
        //if (Players.Contains(name) == false)
        //{
        //    Players.Add(name);
        //}

        var player = Players.FirstOrDefault(p => p.Name == name);
        if (player != null)
        {
            Players.Remove(player);
        }
    }

    [RelayCommand]
    async Task NextBtn(object object1)
    {
        if (Players.Count < 2)
        {
            Shell.Current.CurrentPage.ShowPopup(new PopupPage());

        }
        else
        {
            if (object1 is Entry entry)
            {
                if (entry.IsSoftKeyboardShowing() == true)
                {
                    entry.HideKeyboardAsync(CancellationToken.None);
                }
            }
            await Shell.Current.GoToAsync(nameof(NewGamePage), 
                    new Dictionary<string, object>
                    {
                        ["Players"] = Players
                    });
        }
    }


}
