using System;
using Terraria.WorldBuilding;

namespace Romert.Common.ModSystems.WorldGens;

public class PygmyArena : BaseWorldGen {
    bool Gen;
    int X = 0;
    int Y = 0;

    public override string NameGen => "Jungle Temple";
    public override bool GensBool { get => Gen; set => Gen = value; }
    public override int Index => 1;
    public override bool Do_MakeGen(GenerationProgress progress) {
        int neededBlockCount = 250;
       
        int blockCount = 0;
        int jungleX0 = 0;
        int jungleX1 = 0;
        int jungleX2 = 0;

        // Right X
        for (int x = GenVars.jungleMinX; x < Main.maxTilesX; x++) {
            for (int y = 0; y < Main.maxTilesY; y++) {
                if (Main.tile[x, y].HasTile && Main.tile[x, y].TileType == TileID.Mud) {
                    if (blockCount >= neededBlockCount) { break; }
                    blockCount = CheckTileCount(x, y, 1000, 1000, TileID.Mud);
                    if (blockCount >= neededBlockCount) { jungleX1 = x; Y = y; }
                }
            }
        }
        RomertVars.JungleRightX = jungleX1;
        blockCount = 0;
        //Left X
        for (int x = GenVars.jungleMaxX; x < Main.maxTilesX; x++) {
            for (int y = 0; y < Main.maxTilesY; y++) {
                if (Main.tile[x, y].HasTile && Main.tile[x, y].TileType == TileID.Mud) {
                    if (blockCount >= neededBlockCount) { break; }
                    blockCount = CheckTileCount(x, y, 1000, 1000, TileID.Mud);
                    if (blockCount >= neededBlockCount) { jungleX2 = x; }
                }
            }
        }
        RomertVars.JungleLeftX = jungleX2;
        jungleX0 = (jungleX1 + jungleX2) / 2;
        RomertVars.JungleCenterX = jungleX0;
        CleaningAll(RomertVars.JungleCenterX, Y, RomertVars.JungleCenterX + 100, Y + 100);

        return true;
    }
    public override void PostUpdateWorld() {
        //Main.NewText("posX = " + X + " posY = " + Y);
        //WorldGen.PlaceTile(X, Y, TileID.Adamantite);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="startX"></param>
    /// <param name="startY"></param>
    /// <param name="endX">+ смешения в право </param>
    /// <param name="endY">+ смешения в верх </param>
    public static void CleaningAll(int startX, int startY, int endX, int endY) {
        int minX = Math.Min(startX, endX);
        int maxX = Math.Max(startX, endX);
        int minY = Math.Min(startY, endY);
        int maxY = Math.Max(startY, endY);

        for (int x = minX; x <= maxX; x++) {
            for (int y = minY; y <= maxY; y++) {
                if (WorldGen.InWorld(x, y, 10)) {
                    Tile tile = Main.tile[x, y];
                    tile.WallType = WallID.None;
                    tile.HasTile = false;
                    tile.LiquidAmount = 0;
                }
            }
        }
    }
    public static int CheckTileCount(int x, int y, int width, int height, int tileType) {
        int count = 0;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (WorldGen.InWorld(x + i, y - j)) {
                    if (Main.tile[x + i, y - j].TileType == tileType) { count++; }
                }
            }
        }
        return count;
    }   
}