using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.Util.Math;

namespace KnuckleBonesGame.UI.Widgets;

public class SpriteWidget : Widget {
  public Texture Image;
  
  public SpriteWidget(Texture image, Vector2 position) {
    Image = image;
    Position = position;
    Size = new Vector2(image.Width, image.Height);
  }
  public override void Render(MatrixStack matrixStack, float delta) {
    
    SystemRegistry.Batcher.Image(Image,Color);
  }

  public override void Update(Vector2 mousePos) {
  }

  public override void HandleInput() {
  }
}