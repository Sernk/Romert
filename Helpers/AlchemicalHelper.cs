using Romert.Common.GlobalItems;
using Romert.Core;
using System.Collections.Generic;

namespace Romert.Helpers;
public static partial class AlchemicalHelper {
    public static T Get<T>(this Item item) where T : GlobalItem => item.GetGlobalItem<T>();
    public static AlchemicalItems Get(Item item) => item.GetGlobalItem<AlchemicalItems>();
    public static void IsAlchemistPoisoning(this Item item) => IsDebuffItem(item, Get(item).IsAlchemistPoisoningItems);
    public static bool IsFlaskItem(this Item item) => Get(item).isFlask = true;
    public static void IsDebuffItem(Item item, HashSet<int> debuff) {
        debuff.Add(item.type);
        IsFlaskItem(item);
    }
}
