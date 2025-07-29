using Foster.Framework;
using KnuckleBonesGame.Registry;

namespace KnuckleBonesGame;

public class SpriteAsset : IAsset {
  public Texture Texture { get; private set; }
  public ResourceLocation Id { get; }
  public void LoadFromFile(string filePath, ResourceLocation location) {
    Texture = new Texture(SystemRegistry.GraphicsDevice,new Image(filePath));
  }
}