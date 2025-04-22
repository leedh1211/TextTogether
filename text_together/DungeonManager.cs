using System;
using System.Collections.Generic;
using System.Text;

namespace textRPG
{
    class DungeonManager
    {

        public string message ="";
        public MonsterManager enemy = new MonsterManager();

        public int deadCount = 0;
        private static DungeonManager instance;

        public static DungeonManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DungeonManager();
                }
                return instance;
            }
        }
        public void GoDungeon(Player player, List<Item> items, List<Item> inventory, Dungeon dungeon)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전입장");
                Console.WriteLine("이곳에서 던전으로 들어가기전 난이도를 설정 할 수 있습니다.\n");

                Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전    | 방어력 17 이상 권장");
                Console.WriteLine("0.나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = int.Parse(Console.ReadLine());
                if (input < 0 || input > 3)
                {
                    Console.WriteLine("다시 입력해주세요.");
                }

                int orgGold = player.gold;
                int orgHealth = player.health;
                if (input == 0)
                {
                    return;
                }
                if (input == 1)
                {
                    dungeon.dungeonLevel = "쉬움";
                    BaseDungeon(player, dungeon);
                    //ResultDungeon(player, items, inventory, BattleDungeon(player, dungeons[0].requiredDefense, dungeons[0].rewardGold, rand), dungeons[0].dungeonLevel, orgGold, orgHealth);

                }
                else if (input == 2)
                {
                    dungeon.dungeonLevel = "보통";
                    DungeonRaid(player, dungeon);
                }
                else if (input == 3)
                {

                }
            }
        }

        public void BaseDungeon(Player player, Dungeon dungeon)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("베이스 캠프");
                Console.WriteLine("이곳에서 나아가기 전 활동을 할 수 있습니다.\n");

                //PlayerInfo

                Console.WriteLine("1. 나아가기");
                Console.WriteLine("2. 휴식하기");
                Console.WriteLine("3. 저장");
                Console.WriteLine("4. 상태보기");   
                Console.WriteLine("0.나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = int.Parse(Console.ReadLine());
                if (input < 0 || input > 4)
                {
                    Console.WriteLine("다시 입력해주세요.");
                }
                if (input == 0)
                {
                    return;
                }
                if (input == 1)
                {
                    DungeonRaid(player, dungeon);
                }
                else if (input == 2)
                {
                    dungeon.dungeonLevel = "보통";
                    DungeonRaid(player, dungeon);
                }
                else if (input == 3)
                {

                }
                else if (input == 4)
                {
                    PlayerManager.Instance.PlayerInfo(player);
                }
            }
        }

        // 던전 몬스터 조우
        public void DungeonRaid(Player player, Dungeon dungeon)
        {
            dungeon.stage += 1;
            List<Monster> monster = enemy.RandomMonster(dungeon.stage);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"현재 난이도 : {dungeon.dungeonLevel}");
                Console.WriteLine("현재 스테이지 : {0} \n", dungeon.stage);

                for (int i = 0; i < monster.Count; i++)
                {
                    Console.Write("{0} 출현!", monster[i].name);
                    Console.WriteLine(monster[i].health <= 0 ? "Dead" : $"HP : {monster[i].health} ");
                    Console.WriteLine("Level : {0} \n", monster[i].level);
                    //Console.WriteLine("Pow : {0} ", monster[i].attack);
                    //Console.WriteLine("Def : {0} ", monster[i].shield);
                    //Console.WriteLine("Gold : {0} \n", monster[i].gold);
                }


                Console.WriteLine("[플레이어] 체력 : {0}", player.health);
                Console.WriteLine("체력 : {0}", player.health);
                Console.WriteLine("마나 : {0}", player.mana);
                Console.WriteLine("레벨 : {0} \n", player.level);

                Console.WriteLine("1. 공격 ");
                Console.WriteLine("0. 나가기 \n");

                if(deadCount == monster.Count)
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                Console.Write(">>");

                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                    return;
                else if (input == 1)
                {
                    MonsterSelect(player, dungeon, monster);
                    if(deadCount >= monster.Count) 
                    {
                        ResultDungeon(monster, player, dungeon);
                        deadCount = 0;
                        return;
                    }

                }
                else
                {
                    Console.WriteLine("다시 입력해주세요");
                    continue;
                }
            }
        }

        public void Skills(List<Monster> monster, Player player, Dungeon dungeon)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"현재 난이도 : {dungeon.dungeonLevel}");
                Console.WriteLine("현재 스테이지 : {0} \n", dungeon.stage);

                for (int i = 0; i < monster.Count; i++)
                {
                    Console.Write("{0} 출현!", monster[i].name);
                    Console.WriteLine(monster[i].health <= 0 ? "Dead" : $"HP : {monster[i].health} ");
                    Console.WriteLine("Level : {0} \n", monster[i].level);
                    //Console.WriteLine("Pow : {0} ", monster[i].attack);
                    //Console.WriteLine("Def : {0} ", monster[i].shield);
                    //Console.WriteLine("Gold : {0} \n", monster[i].gold);
                }


                Console.WriteLine("[플레이어] 체력 : {0}", player.health);
                Console.WriteLine("체력 : {0}", player.health);
                Console.WriteLine("마나 : {0}", player.mana);
                Console.WriteLine("레벨 : {0} \n", player.level);
            }
        }

        public void MonsterSelect(Player player, Dungeon dungeon, List<Monster> monster)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"현재 난이도 : {dungeon.dungeonLevel}");
                Console.WriteLine("현재 스테이지 : {0} \n", dungeon.stage);

                for (int i = 0; i < monster.Count; i++)
                {
                    Console.Write($"{i + 1} : {monster[i].name} ");
                    Console.WriteLine(monster[i].health <= 0 ? "Dead" : $"HP : {monster[i].health} ");
                }

                Console.WriteLine($"\n[플레이어]");
                Console.WriteLine($"체력 : {player.health}");
                Console.WriteLine($"플레이어 Lv : {player.level}");
                Console.WriteLine($"적 데카 Lv : {deadCount}");

                // if
                if(deadCount == monster.Count)
                {
                    ResultDungeon(monster, player, dungeon);
                    return;
                }

                Console.Write(">>");

                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                    return;
                else if (input >= 1 && input <= monster.Count)
                {
                    // 이미 죽인 몬스터는 예외 처리
                    if (monster[input - 1].health <= 0)
                    {
                        Console.WriteLine("이미 죽어있습니다.");
                        continue;
                    }
                    player.PlayerAttack(monster[input - 1], player);

                    // 죽였을 때 경험치 보상
                    if(monster[input - 1].health <= 0)
                    {
                        ResultExp(monster[input-1], player);
                    }

                    // 몬스터 수 만큼 공격 받음
                    for (int i = 0; i < monster.Count; i++)
                        monster[i].MonsterAttack(monster[i], player);

                    return;
                }
                else
                {
                    Console.WriteLine("다시 입력해주세요");
                    continue;
                }
            }
        }
        
        // 경험치 보상
        public void ResultExp(Monster monster, Player player)
        {
            player.exp += (int)(monster.gold * 0.02);
            while (player.exp >= player.maxEXP)
                {
                    player.LevelUp(player);
                    player.exp -= player.maxEXP;
                    player.maxEXP = (int)(player.maxEXP * 1.5f);
                }
            deadCount++;

        }

        // 던전 결과
        public void ResultDungeon(List<Monster> monster, Player player, Dungeon dungeons)
        {
            int resultGold = 0;
            // 골드 보상
            for (int i = 0; i < monster.Count; i++)
            {
                resultGold += monster[i].gold;
            }
            player.gold += resultGold;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("클리어!");

                Console.WriteLine($"스테이지 - {dungeons.stage} 을 클리어 하였습니다.\n");

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"{resultGold} Gold 획득");

                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                    return;
                else
                {
                    Console.WriteLine("다시 입력해주세요");
                    continue;
                }
            }
        }
    }
}
