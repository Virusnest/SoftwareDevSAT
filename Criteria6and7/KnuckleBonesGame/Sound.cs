using KnuckleBonesGame.Registry;
using MiniAudioExNET;

namespace KnuckleBonesGame;

public class Sound : IAsset {
  public byte[] Data { get; private set; }
  public AudioClip Clip;
  public bool Streamed { get; private set; }
  public string FilePath { get; private set; }

  public ResourceLocation Id { get; }
  public void LoadFromFile(string filePath, ResourceLocation location) {
    FilePath = filePath;
    Streamed = false;
    Data = File.ReadAllBytes(filePath);
    Clip = new AudioClip(Data);
  }
  public static Sound LoadStreamed(string filePath) {
    var sound = new Sound();
    sound.FilePath = filePath;
    sound.Streamed = true;
    sound.Data = System.IO.File.ReadAllBytes(filePath);
    sound.Clip = new AudioClip(sound.Data);
    return sound;
  }
}