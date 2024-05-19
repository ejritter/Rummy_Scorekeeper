namespace RUMMY_SCOREKEEPER.ViewModel;

public partial class GamesViewModel : ObservableObject
{
    public GamesViewModel()
    {
        Players = new ObservableCollection<Player>();
        CurrentGame = new CurrentGame();
    }

    [ObservableProperty]
    ObservableCollection<Player> players;

    [ObservableProperty]
    CurrentGame currentGame;

    

    [RelayCommand]
    async Task StartNewGame()
    {
        await Shell.Current.GoToAsync(nameof(NewGamePage), true);

    }
}
