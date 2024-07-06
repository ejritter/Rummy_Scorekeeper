
namespace RUMMY_SCOREKEEPER.Services;

public class AudioService:IAudioService
{
    private readonly IAudioManager audioManager;
    private  IAudioPlayer audioPlayer;
    private Stream audioFile;
    public AudioService(IAudioManager audioManager)
    {
        this.audioManager = audioManager;
        LoadAudioFile();
        CreateAudioPlayer();
    }
    void CreateAudioPlayer()
    {
        audioPlayer = this.audioManager.CreatePlayer(audioFile);
        audioPlayer.Loop = true;
    }
    async void LoadAudioFile()
    {
        audioFile = await FileSystem.OpenAppPackageFileAsync("Resources/Audio/theGambler.mp3");
    }

    public void Play()
    {
        audioPlayer.Play();
    }

    public void Stop()
    {
        audioPlayer.Stop();
    }

}
