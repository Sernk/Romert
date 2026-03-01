using System.Collections.Generic;

namespace Romert.Core;
public class AlchemistDataID : ILoadable {
    public static string AlchemistPoisoning { get; private set; } = "AlchemistPoisoning"; // 0 or AlchemistPoisoning

    public static Dictionary<int, string> DataID { get; private set; } = [];

    internal static void Register(int id, string name) => DataID.Add(id, name);
    public static string GetByID(int id) => DataID.TryGetValue(id, out string name) ? name : "error";
    public void Load(Mod mod) {
        Register(0, "AlchemistPoisoning");

    }

    public void Unload() { }
}
