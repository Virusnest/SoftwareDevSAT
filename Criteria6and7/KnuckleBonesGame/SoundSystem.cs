using Foster.Framework;
using MiniAudioExNET;

namespace KnuckleBonesGame;
/// <summary>
/// Simple Sound System for KnuckleBonesGame
/// Plays Music and SFX
/// </summary>
public class SoundSystem {

  public AudioSource[] SFXSource;
  public AudioSource MusicSource;
  public const uint SAMPLE_RATE = 44100;
  public const uint CHANNELS = 2;
  
  public SoundSystem() {
    AudioContext.Initialize(SAMPLE_RATE, CHANNELS);
    AudioContext.MasterVolume = 0.7f;
    SFXSource = new AudioSource[100];
    MusicSource = new AudioSource();
    MusicSource.Loop = true;
    for (int i = 0; i < SFXSource.Length; i++) {
      var audioSource = new AudioSource();
      audioSource.Volume = 0.9f;
      audioSource.Loop = false;
      SFXSource[i] = audioSource;
    }
    
  }

  public void PlayMusic(Sound sound) {
    if (!MusicSource.IsPlaying) {
      MusicSource.Play(sound.Clip);
      
    }
  }
  public void PlaySFX(Sound sound) {
    foreach (var audioSource in SFXSource) {

      if (!audioSource.IsPlaying) {
        audioSource.Play(sound.Clip);
        return;
      }
    }
   
  }
}