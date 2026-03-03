using Romert.Common.Players;

namespace Romert.Core;

/// <summary>Represents alchemical debuff data used by <see cref="AlchemistPlayer"/>. </summary>
public class AlchemistData(string name = "error", int debuff = -1, string barColor = "error", string modName = "Romert") {
    public bool IsActive { get; set; } = false;
    public string Name { get; private set; } = name;
    /// <summary> For custom texture bar and skull. </summary>
    public string ModName { get; private set; } = modName;
    public string BarColorName { get; private set; } = barColor;
    public int Debuff { get; private set; } = debuff;
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

    public const int BaseTimeToDeletePoints = 180;
    /// <summary> Changes to these variables only affect specific AlchemistData. </summary>
    public int ModifyTimeToDeletePoints;

    public const int BaseDeletePoints = 1;
    public int ModifyDeletePoints;

    public int DebuffTimeTotal => BaseDebuffTime + ModifyDebuffTime;
    public int PointsToDebuffTotal => BasePointsToDebuff + ModifyMaxPointsToDebuff;
    public int PointsEarnedTotal => BasePointsEarned + ModifyPointsEarned;
    public int TimeToDeletePointsTotal => BaseTimeToDeletePoints + ModifyTimeToDeletePoints;
    public int DeletePointsTotal => BaseDeletePoints + ModifyDeletePoints;

    public int AddPoints(AlchemistPlayer player) => CurrentProgress += PointsEarnedTotal + player.BonusPointsEarned;
    public void ResetPoints() => CurrentProgress = 0;
    public void DeletePoints(AlchemistPlayer player) => CurrentProgress -= DeletePointsTotal + player.BonusDeletePoints;
    public void EditsDebuff(int newDebuff) => Debuff = newDebuff;
    public void EditBar(string newName) => BarColorName = newName;
}