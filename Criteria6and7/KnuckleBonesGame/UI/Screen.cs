using System;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util;
using KnuckleBonesGame.Util.Math;

namespace KnuckleBonesGame.UI;

public abstract class Screen : Widget
{
  protected static Tweener TWEENER = new(); // Tweener instance for animations

  public Screen? LastScreen; // The last screen before this one
  public abstract void Initialize(); // Initialize the screen

  public Screen(Screen? lastScreen = null)
  {
    Anchor = Anchor.Center; // Set the anchor to center
  }
  public override void WidgetRender(MatrixStack matrixStack, float delta, ref int scale)
  {
    TWEENER.Update(delta); // Update the tweener for animations
    base.WidgetRender(matrixStack, delta, ref scale);
  }
}
