using System.Collections.Generic;

namespace Romert.Common.KeyBindStyles;

public abstract class KeyBindStyle : ILoadable {
    public Vector2 NamePos = Vector2.Zero;

    public static List<KeyBindStyle> Styles { get; private set; } = [];

    public virtual void Load(Mod mod) => Styles.Add(this);
    
    public abstract bool Active(string keyName);

    public virtual void PreBgEditName(SpriteBatch spriteBatch, Vector2 pos, bool hover, Color color) { Utils.DrawSettingsPanel(spriteBatch, pos, 306.66f, color); }
    public virtual void PostBgEditName(SpriteBatch spriteBatch, Vector2 pos, bool hover, Color color) { }

    public virtual void PreBgEditReset(SpriteBatch spriteBatch, Vector2 pos, bool hover, Color color) { Utils.DrawSettings2Panel(spriteBatch, pos, 211.8f, color); }
    public virtual void PostBgEditReset(SpriteBatch spriteBatch, Vector2 pos, bool hover, Color color) { }

    public void Unload() { Styles = []; NamePos = Vector2.Zero; }
}