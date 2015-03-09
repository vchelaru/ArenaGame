using System.Collections;

namespace ArenaGame.CharacterProfiles
{
    public class CharacterProfile
    {
        public string Name { get; set; }
        public int Gold { get; set; }

        public int SpeedStat { get; set; }
        public int MeleeStat { get; set; }
        public int RangedStat { get; set; }
        public int HealthStat { get; set; }
    }
}
