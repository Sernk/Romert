using System.Collections.Generic;

namespace Romert.Core;
// TODO: Normal name for vareable
public class AlchemistDataID : ILoadable {
    public static string AlchemistPoisoning { get; private set; } = "Test"; // 0 or Test

    public static Dictionary<int, string> DataID { get; private set; } = [];

    public static void Register(int id, string name) => DataID.Add(id, name);
    public static string GetByID(int id) => DataID.TryGetValue(id, out string name) ? name : "Error";
    public void Load(Mod mod) {
        Register(0, "Test");

    }

    public void Unload() { }
}
