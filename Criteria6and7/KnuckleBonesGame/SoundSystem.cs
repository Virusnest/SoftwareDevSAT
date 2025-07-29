using Foster.Framework;
using MiniAudioExNET;

namespace KnuckleBonesGame;

public class SoundSystem {
  public AudioSource[] SFXSource;
  public const uint SAMPLE_RATE = 44100;
  public const uint CHANNELS = 2;
  
  public SoundSystem() {
    AudioContext.Initialize(SAMPLE_RATE, CHANNELS);
    AudioContext.MasterVolume = 0.1f;
    SFXSource = new AudioSource[100];
    for (int i = 0; i < SFXSource.Length; i++) {
      var audioSource = new AudioSource();
      audioSource.Volume = 0.9f;
      audioSource.Loop = false;
      SFXSource[i] = audioSource;
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