using Terraria.DataStructures;

namespace Romert.Common.PlayerLayers;

public abstract class UIPlayerLayer : PlayerDrawLayer {
    public abstract bool IsVisible(Player player);
    public abstract void Draw(ref PlayerDrawSet drawInfo, Player player, Vector2 pos);
    public sealed override Position GetDefaultPosition() => PlayerDrawLayers.BeforeFirstVanillaLayer;
    protected sealed override void Draw(ref PlayerDrawSet drawInfo) {
        Player player = drawInfo.drawPlayer;
        if (drawInfo.shadow != 0f || player.dead || player.whoAmI != Main.myPlayer) { return; }
        float mountScale = player.mount.Active ? 20 : 0;
        Vector2 Position = drawInfo.Position;
        Vector2 pos = new((int)(Position.X - Main.screenPosition.X + player.width / 2), (int)(Position.Y - Main.screenPosition.Y + (player.height / 2) - 2f * player.gravDir) + mountScale);
        if (IsVisible(player)) { Draw(ref drawInfo, player, pos); }
    }
}