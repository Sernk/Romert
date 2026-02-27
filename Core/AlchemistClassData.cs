namespace Romert.Core;

public class AlchemistData {
    public int Debuff;
    public int DebuffTime;
    public int ProgressToDebuff;

    public AlchemistData() { }
    public AlchemistData(int debuff, int debuffTime, int progressToDebuff) {
        Debuff = debuff;
        DebuffTime = debuffTime;
        ProgressToDebuff = progressToDebuff;
    }
}