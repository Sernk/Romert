using Romert.Common.Players;

namespace Romert.Core;

public class RegisterAlchemistData {
    static AlchemistData AlchemistPoisoning;

    internal static void Init() {
        AlchemistPoisoning = new();
    }
    internal static void Reset(AlchemistPlayer player) {
        for (int i = 0; i < player.AlchemistDatas.Count;) {
            player.AlchemistDatas[i].ModifyPointsEarned = 0;
            player.AlchemistDatas[i].ModifyDebuffTime = 0;
            player.AlchemistDatas[i].ModifyMaxPointsToDebuff = 0;
            player.AlchemistDatas[i].ModifyTimeToDeletePoints = 0;
            player.AlchemistDatas[i].IsActive = false;
            i++;
        }
    }
    internal static void Update(AlchemistPlayer player) {
        RegisterData(ref AlchemistPoisoning, player, "AlchemistPoisoning", 2);
    }
    public static AlchemistData RegisterData(ref AlchemistData data, AlchemistPlayer player, string name, int debuff) {
        data = new(name, debuff);
        player.AlchemistDatas.Add(data);
        player.AlchemistDictionary.Add(name, data);
        return data;
    }
}