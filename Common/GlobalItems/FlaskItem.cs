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
                for (int k = 0; k < alchemist.AlchemistDatas.Count; k++) {
                    item.Get<AlchemicalItems>().FlaskReagents[i].Buff(player, alchemist.AlchemistDatas[k]);
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
                        r.GetItem(item);
                        FlaskData.Add(r);
                    }
                }
            }
            for (int k = 0; k < FlaskData.Count; k++) {
                item.rare = FlaskData[k].Rare is 0 ? item.rare : FlaskData[k].Rare;
                item.value = FlaskData[k].Price is 0 ? item.value : FlaskData[k].Price;
            }
        }
        if (Main.HoverItem.type == ItemID.None) {
            if (Main.rand.NextBool(30)) { FlaskData.Clear(); }
        }
        else { 
            if (Main.HoverItem.netID > 0) {
                if (!Main.HoverItem.Get<AlchemicalItems>().isFlask) {
                    if (Main.rand.NextBool(30)) { FlaskData.Clear(); }
                }
            }
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
                    for (int k = 0; k < item.Get<AlchemicalItems>().FlaskReagents.Length; k++) {
                        AlchemistReagent reagent = item.Get<AlchemicalItems>().FlaskReagents[k];
                        if (reagent.CanNewShot(reagent.Synergy)) {
                            return reagent.Shoot(player, source, position, velocity, type, damage, knockback);
                        }
                        else { i++; k++; }
                    }
                }
                return orig;
            }
            return orig;
        }
        else { return orig; }
    }
}