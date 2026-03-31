using Romert.Common.Players;
using Romert.Content.Reagents.Flask;
using Romert.Core;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;

namespace Romert.Common.GlobalItems;

// Amplification of the sample if it contains a reagent
public class FlaskItem : GlobalItem {
    public List<FlaskItemData> FlaskData { get; private set; } = [];

    public override bool InstancePerEntity => true;

    public override void HoldItem(Item item, Player player) {
        AlchemistPlayer alchemist = player.Get<AlchemistPlayer>();
        if (item.Get<AlchemicalItems>().isFlask) {
            for (int i = 0; i < item.Get<AlchemicalItems>().FlaskReagents.Length; i++) {
                for (int j = 0; j < alchemist.AlchemistDatas.Count; j++) {
                    item.Get<AlchemicalItems>().FlaskReagents[i].Buff(player, alchemist.AlchemistDatas[j]);
                }
            }
        }
    }
    public override void UpdateInventory(Item item, Player player) {
        if (item.Get<AlchemicalItems>().isFlask) {
            if (!FlaskData.Any()) {
                for (int i = 0; i < item.Get<AlchemicalItems>().FlaskReagents.Length; i++) {
                    AlchemistReagent reagent = item.Get<AlchemicalItems>().FlaskReagents[i];
                    if (reagent.EditItem().Name != "" && reagent != GetReagent<Look>() && reagent != GetReagent<NoN>()) {
                        FlaskItemData r;
                        r = item.Get<AlchemicalItems>().FlaskReagents[i].EditItem();
                        r.SetItem(item);
                        FlaskData.Add(r);
                    }
                }
            }
            for (int j = 0; j < FlaskData.Count; j++) {
                item.rare = FlaskData[j].Rare is 0 ? item.rare : FlaskData[j].Rare;
                item.value = FlaskData[j].Price is 0 ? item.value : FlaskData[j].Price;
            }
        }
        if (!Lists.Items.FlaskItem.Contains(Main.HoverItem.type)) {
            if (Main.rand.NextBool(60)) { FlaskData.Clear(); }
        }
    }
    public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage) {
        if (FlaskData != null) {
            for (int i = 0; i < FlaskData.Count; i++) {
                damage.Flat += FlaskData[i].Damage;
            }
        }
    }
    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
        bool orig = base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        if (FlaskData != null) {
            for (int i = 0; i < FlaskData.Count;) {
                if (item == FlaskData[i].Item) {
                    velocity = FlaskData[i].ProjectileSpeed is 0 ? velocity : velocity * FlaskData[i].ProjectileSpeed;
                    type = FlaskData[i].ProjectileType is 0 ? type : FlaskData[i].ProjectileType;
                    damage = FlaskData[i].ProjectileDamage is 0 ? damage : FlaskData[i].ProjectileDamage;
                    knockback = FlaskData[i].ProjectileKnockback is 0 ? knockback : FlaskData[i].ProjectileKnockback;
                    for (int j = 0; j < item.Get<AlchemicalItems>().FlaskReagents.Length; j++) {
                        AlchemistReagent reagent = item.Get<AlchemicalItems>().FlaskReagents[j];
                        if (reagent.CanNewShot(reagent.Synergy)) {
                            return reagent.Shoot(player, source, position, velocity, type, damage, knockback);
                        }
                        else { i++; j++; }
                    }
                }
                return orig;
            }
            return orig;
        }
        else { return orig; }
    }
}