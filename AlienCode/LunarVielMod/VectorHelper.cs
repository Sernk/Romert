using System;
using System.Runtime.CompilerServices;

namespace Romert.AlienCode.LunarVielMod {
    public static class VectorHelper {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Osc(float from, float to, float speed = 1f, float offset = 0f) {
            float dif = (to - from) / 2f;
            return from + dif + dif * (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * speed + offset));
        }
    }
}