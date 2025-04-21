using System;
using System.Collections.Generic;
using System.Text;

namespace textRPG
{
    // 플레이어 정보 클래스
    class Player
    {
        public string name { get; set; }
        public string job { get; set; }
        public int level { get; set; }
        public float attack { get; set; }
        public int shield { get; set; }
        public int health { get; set; }
        public int gold { get; set; }
        public Player() { }
        public Player(string name, string job, int level, float attack, int shield, int health, int gold)
        {
            this.name = name;
            this.job = job;
            this.level = level;
            this.attack = attack;
            this.shield = shield;
            this.health = health;
            this.gold = gold;
        }

        // 레벨 업
        public void LevelUp(Player player)
        {
            player.level += 1;
            player.attack += 0.5f;
            player.shield += 1;
        }

    }
}
