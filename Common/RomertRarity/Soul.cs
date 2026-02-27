using Romert.AlienCode.LunarVielMod;
using System.Collections.Generic;

namespace Romert.Common.RomertRarity {
    public class Soul : ModRarity {
        static List<RaritySparkle> SparkleList = [];
        static readonly Color SolarColor = new(255, 170, 60);
        static readonly Color VortexColor = new(80, 190, 255);
        static readonly Color NebulaColor = new(200, 100, 255);
        static readonly Color StardustColor = new(150, 255, 220);

        static readonly Color[] colors1 = [SolarColor, VortexColor, NebulaColor, StardustColor];

        public override Color RarityColor => Color.White;
        public static void DrawCustomTooltipLine(DrawableTooltipLine tooltipLine) {
            Color cycleColor = AnimatedColor(colors1, 120);
            RarityHelper.DrawBaseTooltipTextAndGlow(tooltipLine, Color.Black, cycleColor, new Color?(new Color(12, 26, 47)), null, new Vector2(0.75f, 0.5f));
        }
        public static Color AnimatedColor(Color[] colors, byte time = 60) {
            int transitionTime = time;
            int colorCount = colors.Length;
            int totalTime = transitionTime * colorCount;

            int timer = (int)(Main.GameUpdateCount % totalTime);
            int index = timer / transitionTime;
            float t = timer % transitionTime / (float)transitionTime;

            Color from = colors[index % colorCount];
            Color to = colors[(index + 1) % colorCount];

            return Color.Lerp(from, to, t);
        }
    }
}