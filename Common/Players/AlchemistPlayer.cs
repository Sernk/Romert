using Romert.Common.GlobalItems;
using Romert.Core;
using System.Collections.Generic;

namespace Romert.Common.Players;

public class AlchemistPlayer : ModPlayer {
    public int BonusMaxPointsToDebuff;
    public int BonusPointsEarned;
    public int BonusDebuffTime;
    public int BonusDeletePoints;
    public int TimeSinceLastImpact;
    public int TimeSnake = 0;
    public int TimeToDeletePoints;
    public int CurrentTime;

    public bool ActiveCurrentAlchemist { get; internal set; }

    public AlchemistData CurrentAlchemist { get; private set; }
    public List<AlchemistData> AlchemistDatas { get; private set; }
    public Dictionary<string, AlchemistData> AlchemistDictionary { get; private set; }

    public static AlchemistPlayer GetPlayer(Player player) => player.GetModPlayer<AlchemistPlayer>();
    public override void Initialize() {
        BonusMaxPointsToDebuff = 0;
        BonusPointsEarned = 0; 
        BonusDebuffTime = 0;
        BonusDeletePoints = 0;
        TimeSinceLastImpact = 0;
        TimeSnake = 0;
        CurrentTime = 0;
        ActiveCurrentAlchemist = false;
        CurrentAlchemist = new("MainAlchemist");
        AlchemistDatas = [];
        AlchemistDictionary = [];
        RegisterAlchemistData.Init();
    }
    public override void ResetEffects() {
        BonusMaxPointsToDebuff = 0;
        BonusPointsEarned = 0;
        BonusDebuffTime = 0;
        BonusDeletePoints = 0;
        RegisterAlchemistData.Reset(this);
    }
    public override void OnEnterWorld() => RegisterAlchemistData.Update(this);
    public override void PostUpdate() {
        for (int i = 0; i < AlchemistDatas.Count;) {
            if (AlchemistDatas[i].IsActive) { CurrentAlchemist = AlchemistDatas[i]; }
            i++;
        }
        CurrentTime++;
        PreAddBuff();
        if (TimeSnake > 0) TimeSnake--;
    }
    void PreAddBuff() {
        if (CurrentAlchemist.CurrentProgress != 0) {
            if (TimeSinceLastImpact == 0) {
                if (CurrentTime >= CurrentAlchemist.TimeToDeletePointsTotal + TimeToDeletePoints) {
                    CurrentAlchemist.DeletePoints(this);
                    TimeSinceLastImpact = 0;
                    CurrentTime = 0;
                }
            }
            else { if (TimeSinceLastImpact > 0) { TimeSinceLastImpact--; } }
        }
        AddDebuff();
    }
    // TODO: Micro fix
    public void AddDebuff() {
        if (CurrentAlchemist.CurrentProgress >= CurrentAlchemist.PointsToDebuffTotal + BonusMaxPointsToDebuff) {
            Player.AddBuff(CurrentAlchemist.Debuff, CurrentAlchemist.DebuffTimeTotal + BonusDebuffTime);
            CurrentAlchemist.ResetPoints();
        }
    }
    public override bool CanUseItem(Item item) {
        bool orgin = base.CanUseItem(item);
        AlchemicalItems alchemicalItems = item.GetGlobalItem<AlchemicalItems>();
        if (alchemicalItems.IsAlchemistPoisoningItems.Contains(item.type)) { BaseLogic(0, orgin); TimeSinceLastImpact = 45; TimeSnake = 75; }
        return orgin;
    }
    public bool BaseLogic(int id, bool flag) {
        if (AlchemistDictionary == null) { return flag; }
        AlchemistDictionary.TryGetValue(AlchemistDataID.GetByID(id), out AlchemistData alchemistData);
        if (alchemistData.CurrentProgress != alchemistData.PointsToDebuffTotal + BonusMaxPointsToDebuff) { alchemistData.AddPoints(this); }
        return flag;
    }
}