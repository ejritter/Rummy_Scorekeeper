namespace RUMMY_SCOREKEEPER
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GamesPage), typeof(GamesPage));
            Routing.RegisterRoute(nameof(NewGamePage), typeof(NewGamePage));
            Routing.RegisterRoute(nameof(CurrentGamePage), typeof(CurrentGamePage));
            //Routing.RegisterRoute(nameof(ConfirmPopupPage), typeof(ConfirmPopupPage));


        }
    }
}
