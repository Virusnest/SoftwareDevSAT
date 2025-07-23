using Foster.Framework;
using MiniAudioExNET;

namespace KnuckleBonesGame;

public class SoundSystem {
  public AudioSource SFXSource;
  public AudioSource MusicSource;
  public const uint SAMPLE_RATE = 44100;
  public const uint CHANNELS = 2;
  
  public SoundSystem() {
    AudioContext.Initialize(SAMPLE_RATE, CHANNELS);
    AudioContext.MasterVolume = 0.1f;
    SFXSource = new AudioSource();
    MusicSource = new AudioSource();
  }
  public void PlaySFX(Sound sound) {
    SFXSource.Play(sound.Clip);
   
  }
  public void PlayMusic(Sound sound) {}
}