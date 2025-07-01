using System.Numerics;

namespace KnuckleBonesGame.Util.Math;

public class MatrixStack {
  private readonly Stack<Matrix4x4> _stack = new();

  public MatrixStack() {
    _stack.Push(Matrix4x4.Identity);
  }

  public void Push() {
    _stack.Push(_stack.Peek());
  }

  public void Push(Matrix4x4 matrix) {
    _stack.Push(_stack.Peek() * matrix);
  }

  public Matrix4x4 Pop() {
    return _stack.Pop();
  }

  public Matrix4x4 Peek() {
    return _stack.Peek();
  }

  public void Multiply(Matrix4x4 matrix) {
    _stack.Push(_stack.Pop() * matrix);
  }

  public static implicit operator Matrix4x4(MatrixStack stack) {
    return stack.Peek();
  }

  public void Translate(Vector2 translation) {
    Multiply(Matrix4x4.CreateTranslation(new Vector3(translation, 0)));
  }
  public void Scale(Vector2 scale) {
    Multiply(Matrix4x4.CreateScale(new Vector3(scale, 1)));
  }
  public void Rotate(float angle) {
    Multiply(Matrix4x4.CreateRotationZ(angle));
  }

  public void Clear() {
    _stack.Clear();
    _stack.Push(Matrix4x4.Identity);
  }
}