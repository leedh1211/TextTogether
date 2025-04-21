using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace textRPG
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

        public Monster(string name, int level, float attack, float shield, int health, int gold)
        {
            this.name = name;
            this.level = level;
            this.attack = attack;
            this.shield = shield;
            this.health = health;
            this.gold = gold;
        }
    }

    class MonsterManager
    {
        public List<Monster> monsters { get; private set; } = new List<Monster>();
        private Random random = new Random();

        public MonsterManager()
        {
            // 몬스터 생성 ( 이름, 레벨, 공격력, 방어력, 체력, 골드)
            monsters.Add(new Monster("까부냥", 1, 5f, 5f, 12, 100));
            monsters.Add(new Monster("도로롱", 1, 3f, 7f, 20, 150));
            monsters.Add(new Monster("치킨", 1, 6f, 4f, 10, 300));
            monsters.Add(new Monster("보스", 7, 10f, 10f, 100, 3000));
        }

        public Monster RandomMonster(int stage)
        {
            // 몬스터 레벨과 스테이지 값을 통한 몬스터 추출 범위 선정
            var tempMonster = monsters.Where(m => m.level <= stage + 2 && m.level >= stage - 5).ToList();
            
            //
            // 선정된 몬스터를 랜덤 입력
            int index = random.Next(tempMonster.Count);
            var baseMonster = tempMonster[index];

            Monster enemy = new Monster(
                baseMonster.name,
                baseMonster.level,
                baseMonster.attack,
                baseMonster.shield,
                baseMonster.health,
                baseMonster.gold
            );


            // 몬스터 레벨 수정 (몬스터 기본 레벨 ~ 몬스터 기본 레벨 + 스테이지) 
            enemy.level = random.Next(enemy.level, enemy.level+stage);


            // 몬스터 스탯 수정 (몬스터 기본 스탯 + (몬스터 레벨 * (몬스터 기본 스탯 * 증가 치 )))
            enemy.attack += (int)(enemy.level * (enemy.attack * 0.15f));
            enemy.shield += (int)(enemy.level * (enemy.shield * 0.15f));
            enemy.health += (int)(enemy.level * (enemy.health * 0.12f));
            enemy.gold += random.Next(enemy.gold, enemy.gold + (enemy.gold * enemy.level / stage));

            return enemy;
        }
    }

}