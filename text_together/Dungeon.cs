using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    public class Dungeon
    {
        public string dungeonLevel { get; set; }
        public int stage {get; set;}
        public bool dungeonClear { get; set;}
        public bool gameClear {get; set;}
        public int deadCount { get; set;}

        public Dungeon() { }
        public Dungeon(string dungeonLevel, int stage, bool dungeonClear, bool gameClear, int deadCount)
        {
            this.dungeonLevel = dungeonLevel;
            this.stage = stage;
            this.dungeonClear = dungeonClear;
            this.gameClear = gameClear;
            this.deadCount = deadCount;
        }
    }
}
