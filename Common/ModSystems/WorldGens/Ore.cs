using Romert.Enums;
using Terraria.WorldBuilding;

namespace Romert.Common.ModSystems.WorldGens;

public class Ore {
    public  class Argite : BaseWorldGen {
        public class Style {
            public Vector2 Position;
            byte ы;
        }

        public bool gen = false;
        public override bool GensBool { get => gen; set => gen = value; }
        public override string SaveName => "Argite";
        //public override string VanillaIndexName => "Jungle Temple";
        public override int Index => 1;

        public override bool Do_MakeGen(GenerationProgress progress) {
            //Data data = new();
            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++) {
                int i2 = WorldGen.genRand.Next(RomertVars.JungleRightX, RomertVars.JungleLeftX);
                int j2 = WorldGen.genRand.Next((int)(Main.maxTilesY * .3f), (int)(Main.maxTilesY * .45f));
                Gen(ArgiteOreStyle.Mini, i2, j2);
            }
            return true;
        }
        public void Gen(ArgiteOreStyle style, int posX, int posY) {
            int _posX;
            int _posY;
            switch (style) {
                case ArgiteOreStyle.Mini: _posX = 7; _posY = 10; break;
                case ArgiteOreStyle.Medium: _posX = 12; _posY = 20; break;
                case ArgiteOreStyle.Big: _posX = 50; _posY = 50; ; break;
                default: _posX = 7; _posY = 10; break;
            }
            PygmyArena.CleaningAll(posX, posY, posX + _posX, posY + _posY);
        }
        
    }
}