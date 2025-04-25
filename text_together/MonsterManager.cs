using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace text_together
{
    class MonsterManager
    {
        public List<Monster> monsters { get; private set; } = new List<Monster>();
        private Random random = new Random();
        private static MonsterManager instance;
        public static MonsterManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MonsterManager();
                }
                return instance;
            }
        }

        public MonsterManager()
        {
            InitializeMonsters();
        }

        public void InitializeMonsters()
        {
            monsters = new List<Monster>
        {
            new Monster("까부냥", 1, 5f, 5f,0, 12, 100, MonsterType.Unit, "까부냥은 오늘도 까분다.", MonsterAscii.Cat.Art ),
            new Monster("도로롱", 1, 3f, 7f,0, 20, 150, MonsterType.Unit, "도로롱의 털이 좋아보인다.", MonsterAscii.Dog.Art),
            new Monster("치킨", 1, 6f, 4f,0, 10, 200, MonsterType.Unit, "치킨의 때깔이 좋아보인다.", MonsterAscii.Bat.Art),
            new Monster("일렉판다", 5, 10f, 10f,0, 30, 3000, MonsterType.Boss, "판다는 떨고있다.", MonsterAscii.Bat.Art),
            new Monster("파이리", 5, 3f, 7f,0, 20, 150, MonsterType.Unit, "파이리의 꼬리가 좋아보인다.", MonsterAscii.Bat.Art),
            new Monster("꼬부기", 6, 3f, 7f,0, 20, 150, MonsterType.Unit, "꼬부기의 꼬리가 좋아보인다.", MonsterAscii.Bat.Art),
            new Monster("이상해씨", 7, 3f, 7f,0, 20, 150, MonsterType.Unit, "이상해씨의 등?이 좋아보인다.", MonsterAscii.Bat.Art),
            new Monster("피카츄", 10, 10f, 10f,0, 30, 3000, MonsterType.Boss, "발전기로 쓰기 좋아보이는 녀석이 있다.", MonsterAscii.Bat.Art)
            
        };
        }

        // 난이도에 따른 몬스터 스탯 수정
        public void FixMonster(Dungeon dungeon)
        {
            float fix=0;

            if (dungeon.dungeonLevel == "쉬움") return;
            else if(dungeon.dungeonLevel == "보통") fix = 1.2f;
            else if(dungeon.dungeonLevel == "어려움") fix = 1.5f;

            foreach(var monster in monsters)
            {
                monster.attack *= fix;
                monster.shield *= fix;
                monster.health = (int)(monster.health * fix);
            }
        }

        public List<Monster> RandomMonster(int stage)
        {

            List<Monster> newMonster = new List<Monster>();
            int count = random.Next(1, 4);
            MonsterType type = MonsterType.Unit;

            for (int i = 0; i < 1; i++)
            {
                // 몬스터 레벨과 스테이지 값을 통한 몬스터 추출 범위 선정
                if(stage % 5 == 0)type = MonsterType.Boss;
                
                var tempMonster = monsters.Where(m => m.level <= stage + 2 && m.level >= stage - 5 && m.monsterType == type).ToList();
                //
                // 선정된 몬스터를 랜덤 입력
                int index = random.Next(tempMonster.Count);
                var selectMonster = tempMonster[index];

                // 기존 리스트를 건드리지 않기 위해 복사본 생성
                Monster enemy = new Monster(
                    selectMonster.name,
                    selectMonster.level,
                    selectMonster.attack,
                    selectMonster.shield,
                    selectMonster.health,
                    selectMonster.gold,
                    selectMonster.monsterType,
                    selectMonster.monsterInfo,
                    selectMonster.monsterArt
                );

                newMonster.Add(enemy);

                // 몬스터 레벨 수정 (몬스터 기본 레벨 ~ 몬스터 기본 레벨 + 스테이지) 
                newMonster[i].level = random.Next(newMonster[i].level, newMonster[i].level + stage);


                // 몬스터 스탯 수정 (몬스터 기본 스탯 + (몬스터 레벨 * (몬스터 기본 스탯 * 증가 치 )))
                newMonster[i].attack += (int)(enemy.level * (enemy.attack * 0.15f));
                enemy.shield += (int)(enemy.level * (enemy.shield * 0.15f));
                enemy.health += (int)(enemy.level * (enemy.health * 0.12f));
                enemy.gold += random.Next(enemy.gold, enemy.gold + (enemy.gold * enemy.level / stage));

                if(newMonster[i].monsterType == MonsterType.Boss) return newMonster;
            }

            return newMonster;
        }

        public void ResetMonsters()
        {
            InitializeMonsters();
        }
    }


}