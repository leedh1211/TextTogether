using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
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
        public int mana { get; set; }
        public int gold { get; set; }
        public int exp { get; set; }
        public int maxEXP {get; set; }

        public Player() { }
        public Player(string name, string job, int level, float attack, int shield, int health, int mana, int gold, int exp, int maxEXP)
        {
            this.name = name;
            this.job = job;
            this.level = level;
            this.attack = attack;
            this.shield = shield;
            this.health = health;
            this.mana = mana;
            this.gold = gold;
            this.exp = exp;
            this.maxEXP = maxEXP;
        }

        // 레벨 업
        public void LevelUp(Player player)
        {
            player.level += 1;
            player.attack += 0.5f;
            player.shield += 1;
        }

        // 플레이어의 공격
        public string PlayerAttack(Monster monster, Player player, Skill skill)
        {
            Random rand = new Random();
            string message;

            // 0.5 오차 반올림
            int round = (int)Math.Round((player.attack + skill.Attack) * 0.1, MidpointRounding.AwayFromZero);

            // 공격력의 10% 오차 적용
            float damage = (rand.Next((int)player.attack + skill.Attack - round, (int)player.attack + skill.Attack + round)) - (monster.shield * 0.7f );
            if(damage <= 0) damage=1;

            // 치명타 구현 ( 15%의 확률로 1.6배의 데미지)
            if(rand.Next(100) < 15)
            {
               damage *= 1.6f;
               message = $"{monster.name} 에게 {(int)damage} 의 치명적인 데미지!";
            }
            else message = $"{monster.name} 에게 {(int)damage} 의 데미지!";

            monster.health -= (int)damage;
            player.mana -= skill.Cost;

            return message;
        }

    }
}
