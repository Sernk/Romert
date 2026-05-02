using Romert.Content.Tiles;
using Romert.Enums;
using Terraria.WorldBuilding;

namespace Romert.Common.ModSystems.WorldGens;

public class Ore {
    public  class Argite : BaseWorldGen {
        public class Style {
            /// Start Position
            public Vector2 Position;
            /// 0 - non, 1 - Stone, 2 - ArgiteOreTile, 3 - empty 
            byte[,] BlockType;
            /// 0 - empty, 1 - hamer, 2 - /|, 3 - |/, 4 - \|, 5 - |\
            byte[,] SlopeType;

            public void Gen(ArgiteOreStyle style) {
                switch (style) {
                    case ArgiteOreStyle.Mini: {
                        BlockType = new byte[,] {
                            {1,1,1,1,1,1,1,0,0}, // 1
                            {1,2,2,2,2,2,1,1,0}, // 2
                            {1,2,2,3,1,1,1,1,1}, // 3
                            {1,2,3,3,1,3,2,2,1}, // 4
                            {1,2,2,2,1,2,2,2,1}, // 5
                            {1,2,2,3,1,3,3,2,1}, // 6
                            {1,1,1,1,1,3,2,2,1}, // 7
                            {0,1,1,2,2,2,2,2,1}, // 8
                            {0,0,1,1,1,1,1,1,1}  // 9
                        };
                        SlopeType = new byte[,] {
                            {0,0,0,0,0,0,0,0,0}, // 1
                            {0,0,0,0,0,0,0,0,0}, // 2
                            {0,0,5,0,0,0,0,0,0}, // 3
                            {0,0,0,0,0,0,0,0,0}, // 4
                            {0,0,0,0,0,0,0,0,0}, // 5
                            {0,0,0,0,0,0,0,0,0}, // 6
                            {0,0,0,0,0,0,4,0,0}, // 7
                            {0,0,0,0,0,0,0,0,0}, // 8
                            {0,0,0,0,0,0,0,0,0}  // 9
                        };  break;
                    }
                    case ArgiteOreStyle.Medium: {
                        BlockType = new byte[,] {
                            {1,1,1,1,1,1,1,1,1,1}, // 1
                            {1,2,2,1,1,1,2,2,2,1}, // 2
                            {1,1,1,1,2,2,2,2,2,1}, // 3
                            {1,2,2,2,2,3,3,2,2,1}, // 4
                            {1,1,2,2,3,3,3,2,1,1}, // 5
                            {1,1,2,3,3,3,2,2,1,1}, // 6
                            {1,2,2,3,3,2,2,2,2,1}, // 7
                            {1,2,2,2,2,2,1,1,1,1}, // 8 
                            {1,2,2,2,1,1,1,2,2,1}, // 9 
                            {1,1,1,1,1,1,1,1,1,1}  // 10
                        };
                        SlopeType = new byte[,] {
                            {0,0,0,0,0,0,0,0,0,0}, // 1
                            {0,0,0,0,0,0,0,0,0,0}, // 2
                            {0,0,0,0,0,0,0,0,0,0}, // 3
                            {0,0,0,0,5,0,0,0,0,0}, // 4
                            {0,0,0,5,0,0,0,0,0,0}, // 5
                            {0,0,0,0,0,0,4,0,0,0}, // 6
                            {0,0,0,0,0,4,0,0,0,0}, // 7
                            {0,0,0,0,0,0,0,0,0,0}, // 8
                            {0,0,0,0,0,0,0,0,0,0}, // 9
                            {0,0,0,0,0,0,0,0,0,0}  // 10
                        };  break;
                    }
                    case ArgiteOreStyle.Big: {
                        BlockType = new byte[,] {
                            {0,0,1,1,1,1,1,0,0}, // 1
                            {1,1,1,1,1,1,1,1,1}, // 2
                            {1,1,1,1,1,1,1,1,1}, // 3
                            {1,2,3,1,2,1,3,2,1}, // 4
                            {1,2,2,3,2,3,2,2,1}, // 5
                            {1,2,2,2,2,2,2,2,1}, // 6
                            {1,2,2,2,2,2,2,2,1}, // 7
                            {1,1,1,2,2,2,1,1,1}, // 8
                            {0,1,2,3,2,3,2,1,0}, // 9
                            {0,1,2,3,2,3,2,1,0}, // 10
                            {0,1,2,2,2,2,2,1,0}, // 11
                            {0,1,2,2,2,2,2,1,0}, // 12
                            {0,1,1,1,1,1,1,1,0}, // 13
                        };
                        SlopeType = new byte[,] {
                            {0,0,0,0,0,0,0,0,0}, // 1
                            {0,0,0,0,0,0,0,0,0}, // 2
                            {0,0,0,0,0,0,0,0,0}, // 3
                            {0,0,0,0,0,0,0,0,0}, // 4
                            {0,0,3,0,0,0,4,0,0}, // 5
                            {0,0,0,0,0,0,0,0,0}, // 6
                            {0,0,0,0,0,0,0,0,0}, // 7
                            {0,0,0,2,0,5,0,0,0}, // 8
                            {0,0,0,0,0,0,0,0,0}, // 9
                            {0,0,0,0,0,0,0,0,0}, // 10
                            {0,0,0,0,0,0,0,0,0}, // 11
                            {0,0,0,0,0,0,0,0,0}, // 12
                            {0,0,0,0,0,0,0,0,0}, // 13
                        };  break;     
                    }
                }
                for (int y = 0; y < BlockType.GetLength(0); y++) {
                    for (int x = 0; x < BlockType.GetLength(1); x++) {
                        int worldX = (int)Position.X + x;
                        int worldY = (int)Position.Y - y;

                        if (!WorldGen.InWorld(worldX, worldY, 10)) { continue; }

                        Tile tile = Framing.GetTileSafely(worldX, worldY);

                        if (BlockType[y, x] > 0) { tile.ClearTile(); }
                        switch (BlockType[y, x]) {
                            case 0: break;
                            case 1: tile.TileType = TileID.Stone; tile.HasTile = true; break;
                            case 2: tile.TileType = (ushort)TileType<ArgiteOreTile>(); tile.HasTile = true; break;
                            case 3: break;
                        }
                        switch (SlopeType[y, x]) {
                            case 0: break;
                            case 1: tile.IsHalfBlock = true; break;
                            case 2: tile.Slope = Terraria.ID.SlopeType.SlopeDownRight; break;
                            case 3: tile.Slope = Terraria.ID.SlopeType.SlopeUpLeft; break;
                            case 4: tile.Slope = Terraria.ID.SlopeType.SlopeUpRight; break;
                            case 5: tile.Slope = Terraria.ID.SlopeType.SlopeDownLeft; break;
                        }
                    }
                }
            }
        }

        public bool gen = false;
        public override bool GensBool { get => gen; set => gen = value; }
        public override string SaveName => "Argite";
        public override int Index => 1;

        public override bool Do_MakeGen(GenerationProgress progress) {
            Style style = new();
            for (int k = 0; k < 60; k++) {
                int i2 = WorldGen.genRand.Next(RomertVars.JungleRightX, RomertVars.JungleLeftX);
                int j2 = WorldGen.genRand.Next((int)(Main.maxTilesY * .3f), (int)(Main.maxTilesY * .45f));
                ArgiteOreStyle argite = Main.rand.Next(0, 3) switch {
                    0 => ArgiteOreStyle.Mini,
                    1 => ArgiteOreStyle.Medium,
                    2 => ArgiteOreStyle.Big,
                    _ => throw new System.NotImplementedException(),
                };
                style.Position = new Vector2(i2, j2);
                style.Gen(argite);
            }
            return true;
        }           
    }
}