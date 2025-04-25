using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace text_together
{

    public enum MonsterType
    {
        Unit,
        Boss
    }
    // 몬스터 설정
    public class Monster
    {
        public string name { get; set; }
        public int level { get; set; }
        public float attack { get; set; }
        public float shield { get; set; }
        public int health { get; set; }

        public int maxHealth { get; set; }
        public int gold { get; set; }
        public MonsterType monsterType {get; set;}
        public string monsterInfo {get; set; }
        public string[] monsterArt { get; set; }

        public Monster() { }
        public Monster(string name, int level, float attack, float shield,int maxHealth, int health, int gold, MonsterType type, string monsterInfo, string[] monsterArt)
        {
            this.name = name;
            this.level = level;
            this.attack = attack;
            this.shield = shield;
            this.maxHealth = health;
            this.health = health;
            this.gold = gold;
            this.monsterType = type;
            this.monsterInfo = monsterInfo;
            this.monsterArt = monsterArt;
        }

        public string MonsterAttack(Monster monster, Player player)
        {
            Random rand = new Random();
            string message="";

            if (monster.health <= 0)
            {
                message = $"{monster.name}은 쓰러져서 움직일 수 없다.";
                return message;
            }

            // 0.5 오차 반올림 
            int round = (int)Math.Round(monster.attack * 0.1, MidpointRounding.AwayFromZero);

            // 공격력의 10% 오차 적용
            int damage = (int)(rand.Next((int)monster.attack - round, (int)monster.attack + round)) - (int)(player.shield * 0.7f );
            if(damage <= 0) damage=1;  
            player.health -= damage;
            message=$"{monster.name}으로부터 {damage}의 데미지를 입었다.";

            return message;
        }
    }

    

}