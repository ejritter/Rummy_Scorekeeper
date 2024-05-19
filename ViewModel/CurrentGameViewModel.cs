using CommunityToolkit.Maui.Views;
using System.Runtime.InteropServices;

namespace RUMMY_SCOREKEEPER.ViewModel;

[QueryProperty(nameof(Players), nameof(Players))]
[QueryProperty(nameof(TotalScoreEntered), nameof(TotalScoreEntered))]
[QueryProperty(nameof(CurrentRound), nameof(CurrentRound))]
[QueryProperty(nameof(CurrentGame), nameof(CurrentGame))]
public partial class CurrentGameViewModel : ObservableObject
{
    private List<Player> modifiedPlayer = new List<Player>();
    private List<Player> winningPlayers = new List<Player>();
    private List<Entry> Entries = new List<Entry>();

    public CurrentGameViewModel()
    {
        IsBusy = false;

    }

    [ObservableProperty]
    CurrentGame currentGame;

    [ObservableProperty]
    int totalScoreEntered;
    
    [ObservableProperty]
    int currentRound;

    [ObservableProperty]
    ObservableCollection<Player> players;

    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    int enteredScore;

    [ObservableProperty]
    Entry currentEntry;

    [ObservableProperty]
    Player currentPlayer;


    [ObservableProperty]
    string leadPlayerName;

    [ObservableProperty]
    bool response;



    //[RelayCommand]
    //public void ScoreEntered(object sender)
    //{
    //    if (sender is Label label)
    //    {
    //        var player = Players.FirstOrDefault(p => p.Name == label.Text);
    //        if (player != null)
    //        {

    //            var mp = modifiedPlayer.FirstOrDefault( mp => mp.Name == player.Name); 
    //            //add player here with the modified/entered score from user input. 
    //            if (mp != null )
    //            {
    //                mp.Score = EnteredScore;
    //            }
    //            else
    //            {
    //                mp = new Player
    //                {
    //                    Name = player.Name,
    //                    Score = EnteredScore
    //                };
    //                modifiedPlayer.Add(mp);
    //            }
    //        }

    //        if (modifiedPlayer.Count == Players.Count)
    //        {
    //            if (IsBusy == true)
    //            {
    //                IsBusy = false;
    //            }
    //            else
    //            {
    //                IsBusy = true;
    //            }
    //        }

    //        CurrentEntry.HideKeyboardAsync();
    //    }
    //}

    [RelayCommand]
    public void ScoreEntered(object sender)
    {

        if (Int32.TryParse(CurrentEntry.Text, out int EnteredScore))
        {

            //TODO
            //add check against submitted score and limit it.
            //Maybe make sure it cannot exceed the scorelimit of the match?
            //Not sure how to check that...
            var mp = modifiedPlayer.FirstOrDefault(mp => mp.Name == CurrentPlayer.Name);
            //add player here with the modified/entered score from user input. 
            if (mp != null)
            {
                mp.Score = EnteredScore;
            }
            else
            {
                mp = new Player
                {
                    Name = CurrentPlayer.Name,
                    Score = EnteredScore
                };
                modifiedPlayer.Add(mp);
            }
            

            if (modifiedPlayer.Count == Players.Count)
            {
                if (IsBusy == true)
                {
                    IsBusy = false;
                }
                else
                {
                    IsBusy = true;
                }
            }

            CurrentEntry.HideKeyboardAsync();

        }
        else
        {
            Shell.Current.CurrentPage.ShowPopup(new WarningPopupPage(new WarningPopupViewModel("Please enter a valid score")));
        }
    }

    [RelayCommand]
    public void SubmitRound()
    {

        CurrentEntry.Unfocus();
        foreach(var mp in modifiedPlayer)
        {
            var player = Players.First(p => p.Name == mp.Name);
            player.Score += mp.Score;
        }

        CurrentGame.CurrentPlayers.Clear();
        winningPlayers = Players.Where(players => players.Score >= TotalScoreEntered)
                                 .ToList();

        if (winningPlayers != null && winningPlayers.Count > 0)
        {
            Shell.Current.CurrentPage.ShowPopup(new WinningPopupPage(new WinningPopupViewModel(winningPlayers, CurrentRound)));
            EndGame(null);
            return;
        }
        //if no winner, proceed to next round
        CurrentGame.CurrentPlayers = Players.ToList();
        CurrentRound++;
        modifiedPlayer.Clear();

        foreach (Entry entry in Entries)
        {
            entry.Text = "0";
        }
       
       //TODO
       //assign the leading player here.
       //

        if (CurrentRound > 1)
        {
            var leadPlayer = Players.Where(p => p.Score >= 0)
                                    .OrderByDescending(p => p.Score)
                                    .FirstOrDefault(p => p.Name == p.Name);
            LeadPlayerName = leadPlayer.Name;                 
        }

        //TODO
        //save current game file for reloading later.


        Entries.Clear();

        IsBusy = false;
    }

    [RelayCommand]
    async void EndGame(object? sender)
    {
        //TODO add confirmation
        if (sender is Button button)
        {
            var Response = await Shell.Current.CurrentPage.ShowPopupAsync(new ConfirmPopupPage(new ConfirmPopupViewModel()));
            if (Response is bool boolResult)
            {
                if (boolResult == false)
                {
                    return;
                }
            }
        }
        //This is also called from submitround.
        //Need to identify how it's called.
        //if End Game button is pressed by user, display a prompt.
        //Location = {//GamesPage/NewGamePage/CurrentGamePage}
        Players.Clear();
        Shell.Current.GoToAsync("//" + nameof(GamesPage),true);

    }


    public void UpdateEnteredScore(int score)
    {
        EnteredScore = score;
    }

    public void SetEntry(Entry entry)
    {
        CurrentPlayer = Players.First(p => p.Name == entry.ClassId);
        CurrentEntry = entry;
        
        if (Entries.FirstOrDefault(e => e.ClassId == CurrentEntry.ClassId) == null)
        {
            Entries.Add(CurrentEntry);
        }

        CurrentEntry.ShowKeyboardAsync();
        var modified = modifiedPlayer.FirstOrDefault(mp => mp.Name == CurrentPlayer.Name);
        if (modified != null)
        {
            CurrentEntry.Text = modified.Score.ToString();
        }
        else
        {
            CurrentEntry.Text = string.Empty;
        }

    }

    public void ClearEntry()
    {
        var modified = modifiedPlayer.FirstOrDefault(mp => mp.Name == CurrentPlayer.Name);
        if (modified != null)
        {
            CurrentEntry.Text = modified.Score.ToString();
        }
        else
        {
            CurrentEntry.Text = string.Empty;
        }
        CurrentEntry.HideKeyboardAsync();
    }

    public void SetResponse(bool response)
    {
        Response = response;
    }
}
