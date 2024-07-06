

namespace RUMMY_SCOREKEEPER.Model;

public partial class CurrentGame : ObservableObject
{
    private Guid currentGameGuid;
    private bool isActive;
    private int totalScoreEntered;
    private int currentRound;
    public List<Player> CurrentPlayers = new();
    private string winningPlayer;


    public string WinningPlayer
    {
        get => winningPlayer;
        set
        {
            if (value == winningPlayer)
                return;
            winningPlayer = value;
        }
    }

    public bool IsActive
    {
        get => isActive = true;
        set
        {
            if (isActive = value) return;
            isActive = false;
        }
    }
    public Guid CurrentGameGuid
    {
        get => currentGameGuid = Guid.NewGuid();
        set
        {
            if (value == currentGameGuid) return;
            currentGameGuid = value;
        }
    }

    public int TotalScoreEntered
    {
        get => totalScoreEntered;
        set
        {
            if (totalScoreEntered == value) return;
            totalScoreEntered = value;
        }
    }

    public int CurrentRound
    {
        get => currentRound;
        set
        {
            if (currentRound == value) return;
            currentRound = value;
            OnPropertyChanged();
        }
    }


    public CurrentGame()
    {

    }

}

[JsonSerializable(typeof(CurrentGame))]
internal sealed partial class CurrentGameContext : JsonSerializerContext
{

}