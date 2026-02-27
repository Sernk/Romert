using Romert.Common.GlobalItems;

namespace Romert.Helpers;
// TODO: Normal name for vareable
public static partial class AlchemicalHelper {
    public static void IsTest(this AlchemicalItems items) => items.IsTestDebuffItem = true; // ?
    public static void IsTest(this Item items) => items.GetGlobalItem<AlchemicalItems>().IsTestDebuffItem = true; // >_<
}
