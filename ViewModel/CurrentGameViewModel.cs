

using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace RUMMY_SCOREKEEPER.ViewModel;

[QueryProperty(nameof(Players), nameof(Players))]
[QueryProperty(nameof(TotalScoreEntered), nameof(TotalScoreEntered))]
public partial class CurrentGameViewModel : ObservableObject
{    
    public CurrentGameViewModel()
    {

    }

    [ObservableProperty]
    int totalScoreEntered;

    [ObservableProperty]
    ObservableCollection<Player> players;

    [RelayCommand]
    void AddScore(object sender)
    {
        if(sender is Label label)
        {
            var player = Players.FirstOrDefault(p => p.Name == label.Text);
            if(player != null)
            {
                Shell.Current.CurrentPage.ShowPopup(new PopupCalculatorPage(player.Name));
            }
        }
    }

    [RelayCommand]
    async Task EndGame()
    {
        //TODO add confirmation
        Players.Clear();
        await Shell.Current.GoToAsync("../..");
    }


}