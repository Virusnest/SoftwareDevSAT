# Code Snippets


## Screen Manager

`UI/ScreenManager.cs`
```csharp  
 public void SetScreen(Screen? screen) {
    CurrentScreen = screen;
    if (!screen?.hasInitialized??false){
      CurrentScreen?.Initialize();
      screen.hasInitialized = true;
    }
    if (CurrentScreen != null) CurrentScreen.Size = new Vector2(virtualWidth, virtualHeight);
  }
 
  public void Resize(float width, float height) {
    _virtualScale= CalcualteUIScale(UIScale, (int)width, (int)height);
    Width = width;
    Height = height;
    virtualHeight = (int)Height / _virtualScale;
    virtualWidth = (int)Width / _virtualScale;
    if (CurrentScreen != null) CurrentScreen.Size = new Vector2(virtualWidth, virtualHeight);
    ViewMatrix = Matrix4x4.CreateScale(_virtualScale + 0.001f);
  }

  private int CalcualteUIScale(int scale, int Width, int Height, int minWidth = 400, int minHeight = 300) {
    int i;
    for (i = 1; i != scale && Width / (i + 1) > minWidth && Width / (i + 1) > minHeight; i++) ;
    return i;
  }
```

## Widgets

`UI/Widgets/Widget.cs`
```csharp
  private Vector2 GetOffset()
  {
    // Calculate the position based on the anchor point and size
    if (Parent == null) return new Vector2(0, 0); // If no parent, return zero offset
    var parentSize = Parent?.Size ?? new Vector2(0, 0); // Get the size of the parent widget

    return Anchor switch
    {
      Anchor.TopLeft => new Vector2(0, 0),
      Anchor.TopCenter => new Vector2(parentSize.X / 2 - Size.X / 2, 0),
      Anchor.TopRight => new Vector2(parentSize.X - Size.X, 0),
      Anchor.LeftCenter => new Vector2(0, parentSize.Y / 2 - Size.Y / 2),
      Anchor.Center => new Vector2(parentSize.X / 2 - Size.X / 2, parentSize.Y / 2 - Size.Y / 2),
      Anchor.RightCenter => new Vector2(parentSize.X - Size.X, parentSize.Y / 2 - Size.Y / 2),
      Anchor.BottomLeft => new Vector2(0, parentSize.Y - Size.Y),
      Anchor.BottomCenter => new Vector2(parentSize.X / 2 - Size.X / 2, parentSize.Y - Size.Y),
      Anchor.BottomRight => new Vector2(parentSize.X - Size.X, parentSize.Y - Size.Y),
      _ => new Vector2(0, 0)
    };
  }

  private Vector2 GetOffsetFlip()
  {
    // Calculate the position based on the anchor point and size
    return Anchor switch
    {
      Anchor.TopLeft => new Vector2(1, 1),
      Anchor.TopCenter => new Vector2(1, 1),
      Anchor.TopRight => new Vector2(-1, 1),
      Anchor.LeftCenter => new Vector2(1, 1),
      Anchor.Center => new Vector2(1, 1),
      Anchor.RightCenter => new Vector2(-1, 1),
      Anchor.BottomLeft => new Vector2(1, -1),
      Anchor.BottomCenter => new Vector2(1, -1),
      Anchor.BottomRight => new Vector2(-1, -1),
      _ => new Vector2(1, 1)
    };
  }
  
  public virtual void WidgetUpdate(MatrixStack matrixStack, ref int scale)
  {
    if (!IsEnabled) return; // If the widget is not enabled, skip the update
    if (!IsVisible) return; // If the widget is not visible, skip the update
    matrixStack.Push(); // Push the current matrix onto the stack
    matrixStack.Translate(GetPosition()); // Translate the matrix to the widget's position
    Matrix4x4.Invert(matrixStack.Peek() * Matrix4x4.CreateScale(scale),
      out var inv); // Invert the matrix for hit testing
    if (!InputBlocked) {
      Update(GetMousePos().Transformed(inv)); // Call the update method
      HandleInput(); // Call the input handling method
    }

    for (int i = 0; i < Children.Count; i++ ){
      Children[i].WidgetUpdate(matrixStack, ref scale); // Update each child widget
    }
    matrixStack.Pop(); // Pop the matrix off the stack
  }

  public virtual void WidgetRender(MatrixStack matrixStack, float delta, ref int scale)
  {
    if (IsVisible)
    {
      matrixStack.Push(); // Push the current matrix onto the stack
      matrixStack.Translate(GetPosition()); // Translate the matrix to the widget's position

      SystemRegistry.Batcher?.PushMatrix(
        matrixStack.Peek().ToMatrix3x2(),false); // Set the model matrix uniform
      SystemRegistry.Batcher?.PushMatrix(
        Matrix3x2.CreateTranslation(-Size / 2) *
        Matrix3x2.CreateScale(Scale) *
        Matrix3x2.CreateRotation(Rotation) *
        Matrix3x2.CreateTranslation(Size / 2)
        ); // Set the model matrix uniform for the batcher
      Render(matrixStack, delta); // Call the render method if the widget is visible
      SystemRegistry.Batcher?.PopMatrix();
      foreach (var child in Children) child.WidgetRender(matrixStack, delta, ref scale);
      SystemRegistry.Batcher?.PopMatrix(); // Pop the model matrix uniform.
      matrixStack.Pop(); // Pop the matrix off the stack
    }
  }
```

## Button Widget

`UI/Widgets/ButtonWidget.cs`
```csharp
  public override void Update(Vector2 mousePos) {
    // Check if the mouse is over the button and if it is clicked
    Hovering = false;

    if (isMouseHover(mousePos)) Hovering = true;
    if (IsMouseDown() == 1) {
      if (Hovering && !startedClickingOut)
        hasPressed = true;
      else
        startedClickingOut = true;
    }

    if (IsMouseDown() != 1) {
      if (hasPressed && Hovering) OnClick?.Invoke();
      if (!Silent && hasPressed && Hovering) SystemRegistry.SoundSystem.PlaySFX(SystemRegistry.AssetManager.LoadAsset<Sound>(new ResourceLocation("Sounds/click.wav"))); // Play click sound if not silent
      startedClickingOut = false;
      hasPressed = false;
    }
  }
```

## Notable Files

 - `KnuckleAI.cs`
 - `UI/Screens/*.cs`
 - `Gameloop/KnuckleGame.cs`


