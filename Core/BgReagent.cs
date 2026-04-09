using System.Collections.Generic;

namespace Romert.Core;

public class BgReagent {
    public string TexturePatch;
    public Vector2 Position;

    const float Lifetime = 120f;
    float Time;
    float Alpha;

    public void Update(ref List<BgReagent> reagents) {
        Time++;
        if (Time < 20f) { Alpha = Time / 20f; }
        else if (Time > Lifetime - 20f) { Alpha = (Lifetime - Time) / 20f; }
        else { Alpha = 1f; }
        if (Time >= Lifetime) { reagents.Remove(this); }
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 pos) => spriteBatch.Draw(TexturePatch.GetAsset().Value, pos, null, Color.White * Alpha, 0f, TexturePatch.GetAsset().Value.Size() / 2, 0.75f, SpriteEffects.None, 1);
}