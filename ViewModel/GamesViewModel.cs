namespace RUMMY_SCOREKEEPER.ViewModel;

public partial class GamesViewModel : ObservableObject
{
    
    public GamesViewModel()
    {
        Players = new ObservableCollection<Player>();
        CurrentGames = new ObservableCollection<CurrentGame>();
        GamesPath = FileSystem.Current.AppDataDirectory;
        GameName = "guid_gameFile.json";
        LoadGameFiles();
        IsEnabled = false;
    }

    [ObservableProperty]
    string gamesPath;

    [ObservableProperty]
    string gameName;

    [ObservableProperty]
    ObservableCollection<Player> players;

    [ObservableProperty]
    ObservableCollection<CurrentGame> currentGames;

    [ObservableProperty]
    CurrentGame currentGame;

    [ObservableProperty]
    bool isEnabled;
    
    private void LoadGameFiles()
    {

        LoadDevData();
        var gameFiles2 = Directory.GetFiles(GamesPath);

        //var gameFiles = Directory.GetFiles(GamesPath, "*_gameFile.json", );
        if (gameFiles != null &&  gameFiles.Length > 0 )
        {
            LoadGameData(gameFiles);
        }
        

    }


    public static async Task<string> ReadTextFile(string filePath)
    {
        using Stream fileStream = FileSystem.Current.OpenAppPackageFileAsync(filePath).Result;
        using StreamReader reader = new StreamReader(fileStream);
        return await Task.FromResult(reader.ReadToEndAsync().Result);

    }
    private async void LoadGameData(IEnumerable<string> gameFiles)
    {

        
        //How to read a json file
        /*
        var gamePath = Path.Combine(GamesPath, GameName);
        using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        /data/user/0/com.companyname.rummy_scorekeeper/files/guid_gameFile.json
        monkeyList = JsonSerializer.Deserialize(contents, CurrentGameContext.Default.ListPlayer);
        "{guid}_gameFile.json";
        */
        foreach (var gameFile in gameFiles)
        {
            //using var stream = await FileSystem.OpenAppPackageFileAsync("guid_gameFile.json");
            //using var reader = new StreamReader(stream);
            //var contents = await reader.ReadToEndAsync();
            //var players = JsonSerializer.Deserialize(contents, CurrentGameContext.Default.ListPlayer);
           await ReadTextFile(gameFile);
        }
    }
    private void LoadDevData() 
    {
        CurrentGame game1 = new CurrentGame();
        CurrentGame game2 = new CurrentGame();

        Player player1 = new Player { Name = "Saray", Score = 30};
        Player player2 = new Player { Name = "Bill", Score = -5 };
        Player player3 = new Player { Name = "John" , Score = 25};
        Player player4 = new Player { Name = "Joe" , Score = -10};

        Players.Add(player1);
        Players.Add(player2);

        game1.CurrentPlayers = Players.ToList();
        game1.CurrentRound = 5;
        game1.TotalScoreEntered = 250;
        Players.Clear();

        Players.Add(player3);
        Players.Add(player4);

        game2.CurrentPlayers = Players.ToList();
        game2.CurrentRound = 3;
        game2.TotalScoreEntered = 500;

        CurrentGames.Add(game1);
        CurrentGames.Add(game2);
    }

    [RelayCommand]
     void SelectedGame(object sender) 
    {
        IsEnabled = true;
        var currentCollectionView = (CollectionView)sender;
        CurrentGame = (CurrentGame)currentCollectionView.SelectedItem;
    
    }


    [RelayCommand]
    void DeleteGame(Guid guid)
    {
        IsEnabled = false;

        if (CurrentGames.Count == 0)
        {
            return;
        }

        CurrentGames.Remove(CurrentGame);
    }

    [RelayCommand]
    async Task StartNewGame()
    {
        await Shell.Current.GoToAsync(nameof(NewGamePage), true,
            new Dictionary<string, object>
            {
                ["GamesPath"] = GamesPath,
                ["GameName"] = GameName
            });

    }

    [RelayCommand]
    async Task ContinueGame()
    {

        //await Shell.Current.GoToAsync(nameof(CurrentGamePage), true,
        //new Dictionary<string, object>
        //{
        //    ["Players"] = Players,
        //    ["TotalScoreEntered"] = enteredScore,
        //    ["CurrentRound"] = CurrentRound,
        //    ["CurrentGame"] = CurrentGame,
        //    ["GamesPath"] = GamesPath,
        //    ["GameName"] = GameName
        //});
        Players.Clear();
        GameName = GameName.Replace("guid",CurrentGame.CurrentGameGuid.ToString());
        foreach(Player player in CurrentGame.CurrentPlayers)
        {
            Players.Add(player);
        }

        await Shell.Current.GoToAsync(nameof(CurrentGamePage), true,
            new Dictionary<string, object>
            {
                ["Players"] = Players,
                ["TotalScoreEntered"] = CurrentGame.TotalScoreEntered,
                ["CurrentRound"] = CurrentGame.CurrentRound,
                ["CurrentGame"] = CurrentGame,
                ["GamesPath"] = GamesPath,
                ["GameName"] = GameName
            });
    }
}
