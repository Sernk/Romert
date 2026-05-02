using ReLogic.Content;
using Romert.Content.Projectiles;
using Romert.Content.Tiles;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Romert.Common.GlobalTiles; 
public class Ore : GlobalTile {
    public Texture2D corrosion;

    public Dictionary<Point16, bool> StonePos { get; private set; } = [];
    public Dictionary<Point16, int> TimeToDestroyer { get; private set; } = [];

    public override void Load() { if (!Main.dedServ) { corrosion = Request<Texture2D>("Romert/Asset/Textures/Extra/ArgiteСorrosion", AssetRequestMode.ImmediateLoad).Value; } }

    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem) {
        if (type == TileType<ArgiteOreTile>() && !fail) {
            Projectile.NewProjectile(WorldGen.GetItemSource_FromTileBreak(i * 16, j * 16), new(i * 16 + 10, j * 16 + 10), Vector2.Zero, ProjectileType<ArgiteBlast>(), 30, 1f, Main.myPlayer);

            AddInDictionary(-1, +1); AddInDictionary(+0, +1); AddInDictionary(+1, +1);
            AddInDictionary(-1, +0);                          AddInDictionary(+1, +0);
            AddInDictionary(-1, -1); AddInDictionary(+0, -1); AddInDictionary(+1, -1);

            void AddInDictionary(int x, int y) { if (Main.tile[i + x, j + y].type == TileID.Stone) { StonePos.TryAdd(new(i + x, j + y), true); } }
        }
        base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);
    }
    public override void ReplaceTile(int i, int j, int type, int targetType, int targetStyle) {
        if (type == TileType<ArgiteOreTile>()) {
            SoundEngine.PlaySound(new SoundStyle("Romert/Asset/Song/Ore/Argite/DestructionOre"), new(i * 16, j * 16));

            Projectile.NewProjectile(WorldGen.GetItemSource_FromTileBreak(i * 16, j * 16), new(i * 16 + 10, j * 16 + 10), Vector2.Zero, ProjectileType<ArgiteBlast>(), 30, 1f, Main.myPlayer);

            AddInDictionary(-1, +1); AddInDictionary(+0, +1); AddInDictionary(+1, +1);
            AddInDictionary(-1, +0); AddInDictionary(+0, +0); AddInDictionary(+1, +0);
            AddInDictionary(-1, -1); AddInDictionary(+0, -1); AddInDictionary(+1, -1);

            void AddInDictionary(int x, int y) { if (Main.tile[i + x, j + y].type == TileID.Stone) { StonePos.TryAdd(new(i + x, j + y), true); } }
        }
        base.ReplaceTile(i, j, type, targetType, targetStyle);
    }
    public override void NearbyEffects(int i, int j, int type, bool closer) {
        Point16 pos = new(i, j);
        if (StonePos.TryGetValue(pos, out _)) { TimeToDestroyer.TryAdd(pos, 140); StonePos.Remove(pos); }
        List<Point16> removeList = [];
        foreach (Point16 pos2 in TimeToDestroyer.Keys) if (!Main.tile[pos2.X, pos2.Y].HasTile) { removeList.Add(pos2); }
        foreach (Point16 pos2 in removeList) { TimeToDestroyer.Remove(pos2); }
        if (TimeToDestroyer.TryGetValue(pos, out int value)) {
            value--;
            if (value > 0) { TimeToDestroyer[pos] = value; }
            else { 
                WorldGen.KillTile(i, j); TimeToDestroyer.Remove(pos);
                if (Main.netMode == NetmodeID.MultiplayerClient && !Main.tile[i, j].active()) { NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 4, i, j); }
            }
        }
    }
    public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch) {
        if (TimeToDestroyer.TryGetValue(new(i, j), out int value)) {
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
            Vector2 drawPos = zero + new Vector2(i * 16, j * 16) - Main.screenPosition;
            float progress = 1f - value / 150f;
            float pulse = 0.4f + progress * 0.4f + (float)Math.Sin(Main.GameUpdateCount * 0.1f) * progress * 0.2f;
            spriteBatch.Draw(corrosion, drawPos, new Color(95, 201, 64) * pulse);
        }
    }

    public override void Unload() => corrosion = null;
}
