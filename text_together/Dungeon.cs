using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    class Dungeon
    {
        public string dungeonLevel { get; set; }
        public int stage {get; set;}

        public Dungeon() { }
        public Dungeon(string dungeonLevel, int stage)
        {
            this.dungeonLevel = dungeonLevel;
            this.stage = stage;
        }
    }
}
