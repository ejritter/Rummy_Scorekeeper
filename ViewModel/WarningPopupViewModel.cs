
namespace RUMMY_SCOREKEEPER.ViewModel;

public partial class WarningPopupViewModel : ObservableObject
{
    public string Text { get; set; }
    public WarningPopupViewModel(string text)
    {
        Text = text;
    }


}
