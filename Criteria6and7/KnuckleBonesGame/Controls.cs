using Foster.Framework;
/// <summary>
/// Control System Most Not Actually Used
/// </summary>
public class Controls(Input input)
{

  public readonly VirtualStick Move = new(input, "Move",
    new StickBindingSet()
      .AddArrowKeys()
      .AddDPad()
      .Add(Axes.LeftX, 0.25f, Axes.LeftY, 0.50f, 0.25f)
  );

  public readonly VirtualAction Jump = new(input, "Jump",
    new ActionBindingSet()
      .Add(Keys.X)
      .Add(Buttons.South)
  );

  public readonly VirtualAction Attack = new(input, "Attack",
    new ActionBindingSet()
      .Add(Keys.C)
      .Add(Buttons.West)
  );
  public readonly VirtualAction Pause = new(input, "Pause",
    new ActionBindingSet()
      .Add(Keys.Escape)
      .Add(Buttons.Start)
  );
}