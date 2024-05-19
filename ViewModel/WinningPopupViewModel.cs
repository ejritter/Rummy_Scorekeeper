
namespace RUMMY_SCOREKEEPER.ViewModel;

public partial class WinningPopupViewModel : ObservableObject
{
    
    public List<Player> WinningPlayers { get; set; }
    public List<string> Names { get; set; }
    public int TotalRounds { get; set; }
    public WinningPopupViewModel(List<Player> winningPlayers, int rounds)
    {
        WinningPlayers = winningPlayers;
        Names = WinningPlayers.Select(x => x.Name).ToList(); 
        TotalRounds = rounds;
        
    }
}
