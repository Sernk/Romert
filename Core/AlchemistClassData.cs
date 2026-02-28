using Romert.Common.Players;

namespace Romert.Core;

/// <summary>
/// Represents alchemical debuff data used by <see cref="AlchemistPlayer"/>.
/// </summary>
public class AlchemistData(string name = "error", int debuff = -1) {
    public int Debuff { get; private set; } = debuff;
    public string Name { get; private set; } = name;
    public int CurrentProgress { get; private set; }

    public const int BaseDebuffTime = 60;
    /// <summary> Changes to these variables only affect specific AlchemistData. </summary>
    public int ModifyDebuffTime;

    public const int BasePointsToDebuff = 100;
    /// <summary> Changes to these variables only affect specific AlchemistData. </summary>
    public int ModifyMaxPointsToDebuff;

    public const int BasePointsEarned = 1;
    /// <summary> Changes to these variables only affect specific AlchemistData. </summary>
    public int ModifyPointsEarned;
    public int DebuffTimeTotal => BaseDebuffTime + ModifyDebuffTime;
    public int PointsToDebuffTotal => BasePointsToDebuff + ModifyMaxPointsToDebuff;
    public int PointsEarnedTotal => BasePointsEarned + ModifyPointsEarned;
    public int AddPoints(AlchemistPlayer player) => CurrentProgress += PointsEarnedTotal + player.BonusPointsEarned;
    public void ResetPoints() => CurrentProgress = 0;
    public void EditsDebuff (int newDebuff) => Debuff = newDebuff;
}