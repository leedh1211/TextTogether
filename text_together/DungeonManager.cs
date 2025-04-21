using System;
using System.Collections.Generic;
using System.Text;

namespace textRPG
{
    class DungeonManager
    {
        public int stage=0;
        public MonsterManager enemy = new MonsterManager();
        // 싱글톤
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
        public void GoDungeon(Player player, List<Item> items, List<Item> inventory, List<Dungeon> dungeons)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전입장");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

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
                Random rand = new Random();
                int orgGold = player.gold;
                int orgHealth = player.health;
                if (input == 0)
                {
                    return;
                }
                if (input == 1)
                {
                    DungeonRaid();
                    //ResultDungeon(player, items, inventory, BattleDungeon(player, dungeons[0].requiredDefense, dungeons[0].rewardGold, rand), dungeons[0].dungeonLevel, orgGold, orgHealth);

                }
                else if (input == 2)
                {
                    ResultDungeon(player, items, inventory, BattleDungeon(player, dungeons[1].requiredDefense, dungeons[1].rewardGold, rand), dungeons[1].dungeonLevel, orgGold, orgHealth);

                }
                else if (input == 3)
                {
                    ResultDungeon(player, items, inventory, BattleDungeon(player, dungeons[2].requiredDefense, dungeons[2].rewardGold, rand), dungeons[2].dungeonLevel, orgGold, orgHealth);
                }

            }


        }

        // 던전 몬스터 조우
        public void DungeonRaid()
        {
            stage +=1;
            var monster = enemy.RandomMonster(stage);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("현재 스테이지 : {0} \n", stage);

                Console.WriteLine("{0} 출현!", monster.name);
                Console.WriteLine("HP : {0} ", monster.health);
                Console.WriteLine("Level : {0}", monster.level);
                Console.WriteLine("Pow : {0} ", monster.attack);
                Console.WriteLine("Def : {0} ", monster.shield);
                Console.WriteLine("Gold : {0} \n", monster.gold);


                Console.WriteLine("0. 나가기 \n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                Console.Write(">>");
                
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

        // 던전 결과 여부 확인
        public bool BattleDungeon(Player player, int shieldLevel, int successGold, Random rand)
        {
            if (player.shield < shieldLevel && rand.Next(100) < 40)
            {
                player.health /= 2;
                return false;
            }
            else
            {
                int dif = player.shield - shieldLevel;
                int heal = rand.Next(20 + dif, 36 + dif); // 35 포함
                double bonusRate = rand.Next((int)player.attack, (int)player.attack * 2) / 100;
                player.LevelUp(player);
                int reward = successGold + successGold * (int)bonusRate;

                player.health -= heal;
                player.gold += reward;
                return true;
            }
        }
        // 던전 결과
        public void ResultDungeon(Player player, List<Item> items, List<Item> inventory, bool isSuccess, string level, int orgGold, int orgHealth)
        {

            while (true)
            {
                Console.Clear();
                if (isSuccess)
                    Console.WriteLine("던전 클리어!");
                else
                    Console.WriteLine("던전 실패!");
                Console.WriteLine($"{level} 던전을 {(isSuccess ? "클리어" : "실패")} 하였습니다.\n");

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {orgHealth} -> {player.health}");
                Console.WriteLine($"Gold {orgGold} -> {player.gold}");

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
