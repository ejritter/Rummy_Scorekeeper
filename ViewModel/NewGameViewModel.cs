using CommunityToolkit.Maui.Converters;
using System.Threading;

namespace RUMMY_SCOREKEEPER.ViewModel;

[QueryProperty(nameof(Players), nameof(Players))]
[QueryProperty(nameof(TotalScoreEntered), nameof(TotalScoreEntered))]
public partial class NewGameViewModel : ObservableObject
{

    public NewGameViewModel()
    {

    }

    [ObservableProperty]
    int totalScoreEntered;

    [ObservableProperty]
    ObservableCollection<Player> players;

    [ObservableProperty]
    string text;


    [RelayCommand]
    async Task CancelBtn()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task StartGame(object sender)
    {
        
        var enteredScore = Text;

        if (string.IsNullOrWhiteSpace(enteredScore))
        {
            return;
        }

        enteredScore = enteredScore.Trim();

        if(Int32.TryParse(enteredScore, out var score) == false)
        {
            Shell.Current.CurrentPage.ShowPopup(new PopupPage());
        }

        if (score < 100 || score > 500)
        {
            Shell.Current.CurrentPage.ShowPopup(new PopupPage());
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

            await Shell.Current.GoToAsync(nameof(CurrentGamePage),
                    new Dictionary<string, object>
                    {
                        ["Players"] = Players,
                        ["TotalScoreEntered"] = enteredScore
                    });
        }


    }
}
