using Romert.Core;
using System.Collections.Generic;

namespace Romert.Common.Players;

public class AlchemistPlayer : ModPlayer {
    public int BonusMaxPointsToDebuff;
    public int BonusPointsEarned;
    public int BonusDebuffTime;

    public AlchemistData CurrentAlchemist { get; private set; }
    public List<AlchemistData> AlchemistDatas { get; private set; }
    public Dictionary<string, AlchemistData> AlchemistDictionary { get; private set; }

    public override void Initialize() {
        BonusMaxPointsToDebuff = 0;
        BonusPointsEarned = 0; 
        BonusDebuffTime = 0;
        CurrentAlchemist = new("MainAlchemist");
        RegisterAlchemistData.Init();
        AlchemistDatas = [];
        AlchemistDictionary = [];
    }
    public override void ResetEffects() {
        BonusMaxPointsToDebuff = 0;
        BonusPointsEarned = 0;
        BonusDebuffTime = 0;
        RegisterAlchemistData.Reset(this);
    }
    public override void OnEnterWorld() => RegisterAlchemistData.Update(this);
    public override void PostUpdate() {
        if (CurrentAlchemist.Debuff == -1) {
            for (int i = 0; i < AlchemistDatas.Count;) {
                if (AlchemistDatas[i].CurrentProgress > 1) { CurrentAlchemist = AlchemistDatas[i]; }
                i++;
            }
        }
        AddDebuff();
    }
    public void AddDebuff() {
        if (CurrentAlchemist.CurrentProgress >= CurrentAlchemist.PointsToDebuffTotal + BonusMaxPointsToDebuff) {
            Player.AddBuff(CurrentAlchemist.Debuff, CurrentAlchemist.DebuffTimeTotal + BonusDebuffTime);
            CurrentAlchemist.ResetPoints();
        }
    }
}