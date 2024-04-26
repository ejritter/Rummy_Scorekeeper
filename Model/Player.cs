using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUMMY_SCOREKEEPER.Model;

public partial class Player : ObservableObject
{
    private int score = 0;
    private string name;
    public string Name
    {
        get => name;
        set
        {
            if (name == value) return;
            name = value;
            OnPropertyChanged();
        }
    }
    public int Score
    {
        get => score;
        set
        {
            if (score == value) return;
            score = value;
            OnPropertyChanged();
        }
    }

    public Player()
    {

    }
}
