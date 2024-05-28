

namespace RUMMY_SCOREKEEPER.Model;

public partial class CurrentGame : ObservableObject
{
    private Guid currentGameGuid;
    private bool isActive;
    private Player leadPlayer;
    private string leadPlayerName;
    private string name;
    private int score;
    private int totalScoreEntered;
    private int currentRound;
    private Player currentPlayer;
    public List<Player> CurrentPlayers;
    private readonly Hashtable scoreLimitHashTable = 
        new Hashtable { 
                       {2, 385 },
                       {3, 350 },
                       {4, 315 },
                       {5, 280 },
                       {6, 245 }
                     };

    private int scoreLimit;

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
    public Player CurrentPlayer
    {
        get => currentPlayer;
        set
        {
            if (currentPlayer == value) return;
            currentPlayer = CurrentPlayers.FirstOrDefault(cp => cp == value);
            OnPropertyChanged();
        }
    }

    public Player LeadPlayer
    {
        get => leadPlayer;
        set
        {
            if (leadPlayer == value) return; 
            leadPlayer = (Player)CurrentPlayers.OrderByDescending(c => c.Score);
            OnPropertyChanged();
        }
    }
    public string Name
    {
        get => name;
        set
        {
            if (name == value) return;
            name = CurrentPlayer.Name;
        }
    }

    public int Score
    {
        get => score;
        set
        {
            if (score == value) return;
            score = CurrentPlayer.Score;
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

    public int ScoreLimit
    {
        get => scoreLimit = (int)scoreLimitHashTable[CurrentPlayers.Count];
    }


    public string LeadPlayerName
    {
        get => leadPlayerName;
        set
        {                                  
            if(leadPlayerName == value) return;
            leadPlayerName = LeadPlayer.Name;
            OnPropertyChanged();
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

[JsonSerializable(typeof(List<Player>))]
internal sealed partial class CurrentGameContext : JsonSerializerContext
{

}