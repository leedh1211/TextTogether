﻿using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    // 플레이어 정보 클래스
    public class Player
    {
        public string name { get; set; }
        public string job { get; set; }
        public int level { get; set; }
        public float attack { get; set; }
        public int shield { get; set; }
        public int health { get; set; }
        public int maxHealth { get; set; }
        public int mana { get; set; }
        public int maxMana {  get; set; }
        public int gold { get; set; }
        public int exp { get; set; }
        public int maxEXP {get; set; }

        public Player() { }
        public Player(string name, string job, int level, float attack, int shield, int health, int maxHealth, int mana, int maxMana, int gold, int exp, int maxEXP)
        {
            this.name = name;
            this.job = job;
            this.level = level;
            this.attack = attack;
            this.shield = shield;
            this.health = health;
            this.maxHealth = health;
            this.mana = mana;
            this.maxMana = mana;
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
        public string PlayerAttack(Monster monster, Player player, Skill skill, Dungeon dungeon)
        {
            Random rand = new Random();
            string message;

            // 0.5 오차 반올림
            int round = (int)Math.Round((player.attack + skill.Attack) * 0.1, MidpointRounding.AwayFromZero);

            // 공격력의 10% 오차 적용
            float damage = (rand.Next((int)player.attack + skill.Attack - round, (int)player.attack + skill.Attack + round)) - (monster.shield * 0.7f );
            if(damage <= 1) damage=1;

            // 치명타 구현 ( 15%의 확률로 1.6배의 데미지)
            if(rand.Next(100) < 15)
            {
               damage *= 1.6f;
               message=$"{monster.name} 에게 {skill.Name}을 사용하여 {(int)damage} 의 치명적인 데미지!\n";
            }
            else message=$"{monster.name} 에게 {skill.Name}을 사용하여 {(int)damage} 의 데미지!\n";

            monster.health -= (int)damage;
            player.mana -= skill.Cost;

            // 쓰려트렸을 때
            if(monster.health <= 0)
            {
                message+=$"{monster.name}을 쓰러트렸다!\n";
                QuestManager.Instance.HandleMonsterKill(monster.name);

                // 경험치 획득
                int plusExp = (int)(monster.gold * 0.02);
                player.exp += plusExp;
                message+=$"{plusExp}의 경험치를 획득했다.\n";

                // 경험치 최대치 이상 획득시 레밸업
                while (player.exp >= player.maxEXP)
                {
                    player.LevelUp(player);
                    player.exp -= player.maxEXP;
                    player.maxEXP = (int)(player.maxEXP * 1.5f);
                    message+=$"Lv 가 {player.level}로 올랐다.\n";
                }
                dungeon.deadCount++;
            }

            return message;
        }

    }
}
