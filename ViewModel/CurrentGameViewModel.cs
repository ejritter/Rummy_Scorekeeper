using CommunityToolkit.Maui.Views;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace RUMMY_SCOREKEEPER.ViewModel;

[QueryProperty(nameof(Players), nameof(Players))]
[QueryProperty(nameof(TotalScoreEntered), nameof(TotalScoreEntered))]
[QueryProperty(nameof(CurrentRound), nameof(CurrentRound))]
[QueryProperty(nameof(CurrentGame), nameof(CurrentGame))]
[QueryProperty(nameof(GamesPath), nameof(GamesPath))]
[QueryProperty(nameof(GameName), nameof(GameName))]
public partial class CurrentGameViewModel : ObservableObject
{
    private List<Player> modifiedPlayer = new List<Player>();
    private List<Player> tiePlayers = new List<Player>();

    private List<Player> winningPlayers = new List<Player>();
    private List<Entry> Entries = new List<Entry>();
    private readonly Dictionary<int, int> dictScoreLimit =
     new Dictionary<int, int>
     {
         [2] = 385,
         [3] = 350,
         [4] = 315,
         [5] = 280,
         [6] = 245

     };

    private int scoreLimit;
    
    public int ScoreLimit
    {
        get => dictScoreLimit[CurrentGame.CurrentPlayers.Count];
    }

    public CurrentGameViewModel()
    {
        IsBusy = false;

    }

    [ObservableProperty]
    CurrentGame currentGame;

    [ObservableProperty]
    int totalScoreEntered;
    
    [ObservableProperty]
    private int currentRound;

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

    [ObservableProperty]
    string gamesPath;

    [ObservableProperty]
    string gameName;



    private void ScoreCheck(int enteredScore)
    {
        if (enteredScore > ScoreLimit)
        {

            string message = $"With {CurrentGame.CurrentPlayers.Count} players,  entered score cannot exceed {ScoreLimit}.";

            Shell.Current.CurrentPage.ShowPopup(new WarningPopupPage(new WarningPopupViewModel(message)));
            return;
        }
    }
    [RelayCommand]
    public void ScoreEntered(object sender)
    {

        if (Int32.TryParse(CurrentEntry.Text, out int EnteredScore))
        {

            //check against submitted score and limit it.
            ScoreCheck(EnteredScore);

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

        //TODO ADD a button asking to pick
        //a winner if multiple winners reach 500
        if (winningPlayers != null && winningPlayers.Count > 0)
        {
            CurrentGame.IsActive = false;
            if (winningPlayers.Count > 1)
            {
                foreach (Player comparePlayer in winningPlayers)
                {
                    var currentScore = comparePlayer.Score;
                    var winningPlayer = comparePlayer.Name;

                    foreach (Player player in winningPlayers)
                    {
                        if (player.Name != winningPlayer && player.Score == currentScore)
                        {
                            //Player has the same score as our current player check.
                            //add it to tiebreaker list.
                            tiePlayers.Add(player);
                        }
                    }
                }
                if (tiePlayers.Count > 0)
                {
                    winningPlayers = null;
                    winningPlayers = tiePlayers;
                }
            }
            Shell.Current.CurrentPage.ShowPopup(new WinningPopupPage(new WinningPopupViewModel(winningPlayers, CurrentRound)));
            EndGame(null);
            return;
        }


        //if no winner, proceed to next round
        CurrentGame.CurrentPlayers = Players.ToList();
        CurrentRound++;
        CurrentGame.CurrentRound = CurrentRound;
        modifiedPlayer.Clear();

        foreach (Entry entry in Entries)
        {
            entry.Text = "0";
        }


        Entries.Clear();
        //assign lead player
        UpdateLeadPlayer();
        IsBusy = false;

        //TODO
        //save current game file for reloading later.
        SaveGame();

    }

    public void UpdateLeadPlayer()
    {
        if (CurrentGame != null) 
        {
            var leadPlayer = CurrentGame.CurrentPlayers.OrderByDescending(x => x.Score).FirstOrDefault();
            if (leadPlayer != null && leadPlayer.Score != 0)
            {
                LeadPlayerName = leadPlayer.Name;
            }
            else
            {
                LeadPlayerName = string.Empty;
            }
        }
    }

    async Task SaveGame()
    {
        var gamePath = Path.Combine(GamesPath, GameName);

        await using FileStream createStream = File.Create(gamePath);
        await JsonSerializer.SerializeAsync(createStream, gamePath);
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
                    //holdem
                    return;
                }
                else
                {
                    //foldem
                    CurrentGame.IsActive = false;
                    CurrentGame.WinningPlayer = string.Empty;
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

}
