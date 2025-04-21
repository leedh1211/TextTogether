using System;
using System.Collections.Generic;
using System.Text;

namespace textRPG
{
    class Dungeon
    {
        public string dungeonLevel { get; set; }
        public int rewardGold { get; set; }
        public int requiredDefense { get; set; }

        public Dungeon() { }
        public Dungeon(string dungeonLevel, int requiredDefense, int rewardGold)
        {
            this.dungeonLevel = dungeonLevel;
            this.requiredDefense = requiredDefense;
            this.rewardGold = rewardGold;
        }
    }
}
