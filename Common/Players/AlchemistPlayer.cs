using Romert.Core;
using System.Collections.Generic;

namespace Romert.Common.Players;

public class AlchemistPlayer : ModPlayer {
    #region Flag
    public int BonusPointsToDebuff;
    public int BonusPointsEarned;
    public int BonusDebuffTime;
    public int BonusDeletePoints;
    public int TimeSinceLastImpact;
    public int TimeSnake;
    public int TimeToDeletePoints;
    public int CurrentTime;
    #endregion

    #region Propertis
    public int DebuffTimeTotal => CurrentAlchemist.DebuffTimeTotal + BonusDebuffTime;
    public int PointsToDebuffTotal => CurrentAlchemist.PointsToDebuffTotal + BonusPointsToDebuff;
    public int PointsEarnedTotal => CurrentAlchemist.PointsEarnedTotal + BonusPointsEarned;
    public int TimeToDeletePointsTotal => CurrentAlchemist.TimeToDeletePointsTotal + TimeToDeletePoints;
    public int DeletePointsTotal => CurrentAlchemist.DeletePointsTotal + BonusDeletePoints;

    public bool ActiveCurrentAlchemist { get; private set; }

    public AlchemistData CurrentAlchemist { get; private set; }
    public List<AlchemistData> AlchemistDatas { get; private set; }
    public Dictionary<string, AlchemistData> AlchemistDictionary { get; private set; }

    public static AlchemistPlayer GetPlayer(Player player) => player.GetModPlayer<AlchemistPlayer>();
    #endregion

    public override void Initialize() {
        BonusPointsToDebuff = 0;
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
        BonusPointsToDebuff = 0;
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
        ActiveCurrentAlchemist = CurrentAlchemist.IsActive;
        CurrentTime++;
        PreAddDebuff();
        if (TimeSnake > 0) TimeSnake--;
    }
    void PreAddDebuff() {
        DeletePoints();
        AddDebuff();
    }
    void DeletePoints() {
        if (CurrentAlchemist.CurrentProgress != 0) {
            if (TimeSinceLastImpact == 0) {
                if (CurrentTime >= TimeToDeletePointsTotal) {
                    CurrentAlchemist.DeletePoints(this);
                    TimeSinceLastImpact = 0;
                    CurrentTime = 0;
                }
            }
            else { if (TimeSinceLastImpact > 0) { TimeSinceLastImpact--; } }
        }
    }
    void AddDebuff() {
        if (CurrentAlchemist.CurrentProgress >= PointsToDebuffTotal) {
            Player.AddBuff(CurrentAlchemist.Debuff, DebuffTimeTotal);
            CurrentAlchemist.ResetPoints();
        }
    }
    public bool AddPoints(int id, bool flag) {
        if (AlchemistDictionary == null) { return flag; }
        AlchemistDictionary.TryGetValue(AlchemistDataID.GetByID(id), out AlchemistData alchemistData);
        if (alchemistData.CurrentProgress != PointsToDebuffTotal) { alchemistData.AddPoints(this); }
        TimeSinceLastImpact = 45; TimeSnake = 75;
        return flag;
    }
}