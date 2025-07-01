
using Foster.Framework;
using KnuckleBonesGame.Registry;

namespace KnuckleBonesGame;

public class AssetManager {
  public const string AssetDirectory = "Assets";
  public static string GetAssetPath(string assetName) {
    return $"Assets/{assetName}";
  }
  public static string GetFontPath(string fontName) {
    return $"Assets/Fonts/{fontName}";
  }
  public static string GetSoundPath(string soundName) {
    return $"Assets/Sounds/{soundName}";
  }
  public static string GetImagePath(string imageName) {
    return $"Assets/Sprites/{imageName}";
  }
  public static string GetShaderPath(string shaderName) {
    return $"Assets/Shaders/{shaderName}";
  }
  public static string GetModelPath(string modelName) {
    return $"Assets/Models/{modelName}";
  }
  
  public T LoadAsset<T>(ResourceLocation location) where T : IAsset, new() {
    if (Assets.TryGetValue(location, out var asset1)) return (T)asset1;
    var asset = new T();
    asset.LoadFromFile(Path.Combine(AssetDirectory, location.ToString()), location);
    Assets[location] = asset;
    return asset;
  }

  public bool IsAssetLoaded<T>(ResourceLocation location) where T : IAsset, new() {
    return Assets.ContainsKey(location) && Assets[location] is T;
  }
  public bool IsAssetLoaded(ResourceLocation location) {
    return Assets.ContainsKey(location);
  }
  
  public Font Font = new Font(GetFontPath("default.ttf"));
  public SpriteFont SpriteFont = new (SystemRegistry.GraphicsDevice,GetFontPath("default.ttf"), 8);
  public Dictionary<ResourceLocation, IAsset> Assets { get; } = new Dictionary<ResourceLocation, IAsset>();
}