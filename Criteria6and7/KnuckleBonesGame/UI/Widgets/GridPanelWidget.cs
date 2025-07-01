using System;
using System.Numerics;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util.Math;

namespace TileGame.UI.Widgets;

public class GridPanelWidget : Widget {

  public int Rows, Columns = 0;
  public Vector2 Spacing = new(0, 0);
  public GridPanelWidget(int rows, int columns, Vector2 spacing) {
    Rows = rows;
    Columns = columns;
    Spacing = spacing;
  }
  public override void HandleInput() {
  }

  public override void Render(MatrixStack matrixStack, float delta) {
  }

  public override void Update(Vector2 mousePos) {
    // Update the position of each child widget based on the grid layout
    float cellWidth = (Size.X - (Columns - 1) * Spacing.X) / Columns;
    float cellHeight = (Size.Y - (Rows - 1) * Spacing.Y) / Rows;
    for (int i = 0; i < Children.Count; i++) {
      int row = i / Columns;
      int column = i % Columns;
      float x = column * (cellWidth + Spacing.X);
      float y = row * (cellHeight + Spacing.Y);
      Children[i].Position = new Vector2(x, y);
      Children[i].Size = new Vector2(cellWidth, cellHeight);
    }
  }
}
