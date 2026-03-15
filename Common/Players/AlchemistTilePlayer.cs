namespace Romert.Common.Players;

public class AlchemistTilePlayer : ModPlayer {
    public bool ActiveAlchemistUI { get; set; }

    public override void Initialize() {
        ActiveAlchemistUI = false;
    }
    public override void ResetEffects() {
    }
}