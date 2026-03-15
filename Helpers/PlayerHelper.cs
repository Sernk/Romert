namespace Romert.Helpers;

public static partial class PlayerHelper {
    public static T Get<T>(this Player player) where T : ModPlayer => player.GetModPlayer<T>();
}
