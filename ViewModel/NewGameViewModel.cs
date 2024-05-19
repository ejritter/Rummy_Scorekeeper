using CommunityToolkit.Maui.Converters;
using System.Threading;

namespace RUMMY_SCOREKEEPER.ViewModel;

public partial class NewGameViewModel : ObservableObject
{

    public NewGameViewModel()
    {
       CurrentGame = new CurrentGame();
       Players = new ObservableCollection<Player>();
    }

    [ObservableProperty]
    string totalScoreEntered;

    [ObservableProperty]
    int currentRound;

    [ObservableProperty]
    ObservableCollection<Player> players;

    [ObservableProperty]
    string text;

    [ObservableProperty]
    CurrentGame currentGame;



    [RelayCommand]
    async Task CancelBtn()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    [RelayCommand]
    void AddPlayerBtn()
    {
        if (Players.Count == 6)
        {
            return;
        }
        var enteredName = Text;

        if (string.IsNullOrWhiteSpace(enteredName))
        {
            return;
        }

        enteredName = enteredName.Trim();

        var player = Players.FirstOrDefault(p => p.Name == enteredName);
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
        var player = Players.FirstOrDefault(p => p.Name == name);
        if (player != null)
        {
            Players.Remove(player);
        }
    }

    [RelayCommand]
    async Task StartGame(object sender)
    {

        if (Players.Count < 2)
        {
            Shell.Current.CurrentPage.ShowPopup(new WarningPopupPage(new WarningPopupViewModel("Enter 2 - 6 players")));
            return;
        }

        var enteredScore = TotalScoreEntered;

        if (string.IsNullOrWhiteSpace(enteredScore))
        {
            Shell.Current.CurrentPage.ShowPopup(new WarningPopupPage(new WarningPopupViewModel("Please enter a valid score range.")));
            return;
        }

        enteredScore = enteredScore.Trim();

        if(Int32.TryParse(enteredScore, out var score) == false)
        {
            Shell.Current.CurrentPage.ShowPopup(new WarningPopupPage(new WarningPopupViewModel("Please enter a valid score range.")));
            return;
        }

        if (score < 100 || score > 500)
        {
            Shell.Current.CurrentPage.ShowPopup(new WarningPopupPage(new WarningPopupViewModel("Please enter a valid score range.")));
            return;
        }
        else
        {
            if (sender is Entry entry)
            {
                if (entry.IsSoftKeyboardShowing() == true)
                {
                    entry.HideKeyboardAsync(CancellationToken.None);
                }
            }
            CurrentRound = 1;
            CurrentGame.CurrentRound = CurrentRound;
            CurrentGame.CurrentPlayers = Players.ToList();

            await Shell.Current.GoToAsync(nameof(CurrentGamePage),true,
                    new Dictionary<string, object>
                    {
                        ["Players"] = Players,
                        ["TotalScoreEntered"] = enteredScore,
                        ["CurrentRound"] = CurrentRound,
                        ["CurrentGame"] = CurrentGame
                    });
        }


    }
}
