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

            // 이름, 레벨, 공격력, 방어력, 최대 체력, 체력, 골드(경험치), 타입, 설명, 이미지
            new Monster("까부냥", 1, 5f, 5f,0, 12, 100, MonsterType.Unit, "까부냥은 오늘도 까분다.", MonsterAscii.Cat.Art ),
            new Monster("도로롱", 1, 3f, 7f,0, 20, 100, MonsterType.Unit, "도로롱의 털이 좋아보인다.", MonsterAscii.Dog.Art),
            new Monster("치킨", 1, 6f, 4f,0, 10, 100, MonsterType.Unit, "치킨의 때깔이 좋아보인다.", MonsterAscii.Duck.Art),
            new Monster("파이리", 5, 10f, 10f,0, 30, 2500, MonsterType.Boss, "파이리의 꼬리가 활활 타오른다.", MonsterAscii.Cat.Art),
            //6스테이지
            new Monster("꼬부기", 6, 8f, 10f,0, 25, 250, MonsterType.Unit, "꼬부기의 등껍질이 좋아보인다.", MonsterAscii.Dog.Art),
            new Monster("이상해씨", 7, 7f, 11f,0, 30, 300, MonsterType.Unit, "이상해씨의 등에 벌레가 꼬이고 있다..", MonsterAscii.Duck.Art),
            new Monster("푸린", 10, 12f, 12f,0, 40, 4000, MonsterType.Boss, "푸린이 마이크 음질을 테스트하고 있다.", MonsterAscii.Cat.Art),
            //11스테이지
            new Monster("피카츄", 11, 12f, 10f,0, 25, 600, MonsterType.Unit, "발전기로 쓰기 좋아보이는 녀석이 있다.", MonsterAscii.Bat.Art),
            new Monster("일렉판다", 13, 13f, 13f,0, 35, 1000, MonsterType.Unit, "내가 아는 판다랑은 다른 것 같다.", MonsterAscii.Snowman.Art),
            new Monster("야돈", 15, 15f, 23f,0, 50, 6000, MonsterType.Boss, "야돈이 느리게 걸어오고 있다.", MonsterAscii.Duck.Art),
            // 16스테이지
            new Monster("다크울프", 17, 16f, 10f,0, 25, 1500, MonsterType.Unit, "타고 다니기 좋아보이는 녀석이다.", MonsterAscii.Dog.Art),
            new Monster("헤로롱", 17, 15f, 25f,0, 40, 2500, MonsterType.Unit, "헤로롱이 나를 먹으려고 한다.", MonsterAscii.Snowman.Art),
            new Monster("거북왕", 20, 18f, 22f,0, 70, 10500, MonsterType.Boss, "거북왕의 대포가 나를 가리키고있다.", MonsterAscii.Snowman.Art),
            // 21스테이지
            new Monster("피죤투", 21, 17f, 20f,0, 35, 3000, MonsterType.Unit, "피죤투는 버림받은 것 같다.", MonsterAscii.Duck.Art),
            new Monster("그린모스", 23, 17f, 25f,0, 35, 4500, MonsterType.Unit, "내가 아는 판다랑은 다른 것 같다.", MonsterAscii.Snowman.Art),
            new Monster("잠만보", 25, 15f, 25f,0, 100, 12500, MonsterType.Boss, "야돈이 느리게 걸어오고 있다.", MonsterAscii.Cat.Art),
            //26 스테이지
            new Monster("빙천마", 27, 17f, 25f,0, 50, 5500, MonsterType.Unit, "주변이 얼어붙는 것 같다..", MonsterAscii.Duck.Art),
            new Monster("뮤츠", 27, 17f, 25f,0, 50, 5500, MonsterType.Unit, "뮤츠는 매우 화가나 보인다.", MonsterAscii.Snowman.Art),
            new Monster("포켓몬 마스터", 30, 20f, 25f,0, 70, 20000, MonsterType.Boss, "그는 혼자가 된 것 같다.", MonsterAscii.Bat.Art),

        };
        }

        // 난이도에 따른 몬스터 스탯 수정
        public void FixMonster(Dungeon dungeon)
        {
            float fix = 0;

            if (dungeon.dungeonLevel == "쉬움") return;
            else if (dungeon.dungeonLevel == "보통") fix = 1.2f;
            else if (dungeon.dungeonLevel == "어려움") fix = 1.5f;

            foreach (var monster in monsters)
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

            for (int i = 0; i < count; i++)
            {
                // 몬스터 레벨과 스테이지 값을 통한 몬스터 추출 범위 선정
                if (stage % 5 == 0) type = MonsterType.Boss;

                var tempMonster = monsters.Where(m => m.level <= stage + 2 && m.level >= stage - 4 && m.monsterType == type).ToList();
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
                    selectMonster.maxHealth,
                    selectMonster.health,
                    selectMonster.gold,
                    selectMonster.monsterType,
                    selectMonster.monsterInfo,
                    selectMonster.monsterArt
                );

                newMonster.Add(enemy);

                // 몬스터 레벨 수정 (몬스터 기본 레벨 ~ 몬스터 기본 레벨 + 3) 
                newMonster[i].level = random.Next(newMonster[i].level, newMonster[i].level + 3);


                // 몬스터 스탯 수정 (몬스터 기본 스탯 + (몬스터 레벨 *  증가 치 )))

                newMonster[i].attack += (int)(newMonster[i].level * 0.5f);
                newMonster[i].shield += (int)(newMonster[i].level * 0.5f);
                newMonster[i].health += (int)(newMonster[i].level * 0.2f);
                newMonster[i].gold += random.Next(newMonster[i].gold / 2, newMonster[i].gold + (newMonster[i].gold * newMonster[i].level / 2));
                // newMonster[i].attack += (int)(enemy.level * (enemy.attack * 0.15f));
                // enemy.shield += (int)(enemy.level * (enemy.shield * 0.15f));
                // enemy.health += (int)(enemy.level * (enemy.health * 0.12f));
                // enemy.gold += random.Next(enemy.gold / 2, enemy.gold + (enemy.gold * enemy.level / 2));

                if (newMonster[i].monsterType == MonsterType.Boss) return newMonster;
            }

            return newMonster;
        }

        public void ResetMonsters()
        {
            InitializeMonsters();
        }
    }


}