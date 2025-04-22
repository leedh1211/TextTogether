using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace text_together
{

    // 몬스터 설정
    class Monster
    {
        public string name { get; set; }
        public int level { get; set; }
        public float attack { get; set; }
        public float shield { get; set; }
        public int health { get; set; }
        public int gold { get; set; }

        public Monster() { }
        public Monster(string name, int level, float attack, float shield, int health, int gold)
        {
            this.name = name;
            this.level = level;
            this.attack = attack;
            this.shield = shield;
            this.health = health;
            this.gold = gold;
        }

        public void MonsterAttack(Monster monster, Player player)
        {
            Random rand = new Random();

            // 몬스터 피가 0 이하면 데미지 없이 리턴
            if(monster.health <= 0)
            {
                return;
            }

            // 0.5 오차 반올림 
            int round = (int)Math.Round(monster.attack * 0.1, MidpointRounding.AwayFromZero);

            // 공격력의 10% 오차 적용
            int damage = (int)(rand.Next((int)monster.attack - round, (int)monster.attack + round)) - (int)(player.shield * 0.7f );
            if(damage <= 0) damage=1;  
            player.health -= damage;
        }
    }

    

}