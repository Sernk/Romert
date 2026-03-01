using Romert.Common.GlobalItems;

namespace Romert.Helpers;
// TODO: Normal name for vareable
public static partial class AlchemicalHelper {
    public static void IsAlchemistPoisoning(this Item item) => item.GetGlobalItem<AlchemicalItems>().IsAlchemistPoisoningItems.Add(item.type);
}
