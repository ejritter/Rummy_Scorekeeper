
namespace RUMMY_SCOREKEEPER.Services;

public class AudioService:IAudioService
{
    private readonly IAudioManager audioManager;
    private  IAudioPlayer audioPlayer;
    public AudioService(AudioManager audioManager)
    {
        this.audioManager = audioManager;
        LoadAudioFile();
    }
  


    async void Stop()
    {
        audioPlayer.Stop();
    }

    async void LoadAudioFile()
    {
        
        audioPlayer = (IAudioPlayer)this.audioManager.CreateAsyncPlayer(await FileSystem.OpenAppPackageFileAsync("Resources/Audio/theGambler.mp3"));
        


        //var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Resources/Audio/theGambler.mp3"));

        //audioPlayer.Play();
        //audioPlayer.Loop = true;
    }

    public Task StartAsync()
    {
        await audioPlayer.Play();
        audioPlayer.Loop = true;
    }

    public Task StopAsync()
    {
        throw new NotImplementedException();
    }
}
