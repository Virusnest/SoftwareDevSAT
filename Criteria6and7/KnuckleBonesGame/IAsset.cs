using KnuckleBonesGame.Registry;

namespace KnuckleBonesGame;

public interface IAsset {
  ResourceLocation Id { get; }
  public void LoadFromFile(string filePath, ResourceLocation location);
}