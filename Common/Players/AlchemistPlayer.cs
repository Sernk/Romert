using Romert.Core;
using System.Collections.Generic;

namespace Romert.Common.Players;
// TODO: Normal name for vareable
public class AlchemistPlayer : ModPlayer {
    public const int MaxTimeToDebuff = 100;
    public int ModifyMaxTimeToDebuff;

    public AlchemistData CurrentAlchemist { get; private set; }
    public List<AlchemistData> AlchemistDatas { get; private set; }
    public Dictionary<string, AlchemistData> AlchemistDictionary { get; private set; }
    AlchemistData Test;

    public override void Initialize() {
        ModifyMaxTimeToDebuff = 0;
        CurrentAlchemist = new AlchemistData();
        Test = new AlchemistData();
        AlchemistDatas = [];
        AlchemistDictionary = [];
    }
    public override void ResetEffects() {
        ModifyMaxTimeToDebuff = 0;
    }
    public override void OnEnterWorld() {
        Test = RegisterData(1, 60, 0);
    }
    public override void PostUpdate() {
        if (CurrentAlchemist.Debuff == 0) {
            for (int i = 0; i < AlchemistDatas.Count;) {
                if (AlchemistDatas[i].ProgressToDebuff > 1) {
                    CurrentAlchemist = AlchemistDatas[i];
                }
                i++;
            }
        }
        AddDebuff();
    }
    public void AddDebuff() {
        if (CurrentAlchemist.ProgressToDebuff == MaxTimeToDebuff + ModifyMaxTimeToDebuff) {
            Player.AddBuff(CurrentAlchemist.Debuff, CurrentAlchemist.DebuffTime);
            CurrentAlchemist.ProgressToDebuff = 0;
        }
    }
    public AlchemistData RegisterData(int debuff, int debuffTime, int progress) {
        AlchemistData data = new(debuff, debuffTime, progress);
        AlchemistDatas.Add(data);
        AlchemistDictionary.Add(nameof(Test), data);
        return data;
    }
}