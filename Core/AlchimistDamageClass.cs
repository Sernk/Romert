namespace Romert.Core;

public abstract class AlchemistsDamageClass : DamageClass {
    public virtual bool IsFlaskItem => false;
    public override string LocalizationCategory => LocCategory[0];
    public sealed override StatInheritanceData GetModifierInheritance(DamageClass damageClass) {
        float attackSpeed = 0f;
        if (damageClass == Generic) { return new StatInheritanceData(1f, 0f, 0f, 0f, 0f); }
        if (IsFlaskItem && damageClass == Throwing) { attackSpeed = 0.4f; }
        return new StatInheritanceData(0f, 0f, attackSpeed, 0f, 0f);
    }
    public sealed override bool GetEffectInheritance(DamageClass damageClass) {
        if (IsFlaskItem && damageClass == Throwing) { return true; }
        else { return false; }
    }
    public sealed override void SetDefaultStats(Player player) {
        if (GetType().Name != "AlchemistsDamageClass") { player.GetCritChance(this) += 4; }
    }
}