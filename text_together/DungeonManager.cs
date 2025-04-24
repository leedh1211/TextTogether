using NAudio.Codecs;
using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    class DungeonManager
    {

        public string message = "";
        public MonsterManager enemy = new MonsterManager();
        public SkillManager skill = new SkillManager();
        Random rand = new Random();

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
        public int GoDungeon(Player player, List<Item> items, List<Item> inventory, Dungeon dungeon)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전입장");
                Console.WriteLine("이곳에서 던전으로 들어가기전 난이도를 설정 할 수 있습니다.\n");

                int orgGold = player.gold;
                int orgHealth = player.health;

                List<Option> options = new List<Option>
                {
                new Option { text = "난이도 : 쉬움", value = 1 },
                new Option { text = "난이도 : 보통", value = 2 },
                new Option { text = "난이도 : 어려움", value = 3 },
                new Option { text = "나가기", value = 0 },
                };

                int selectedValue = UIManager.inputController(options);

                switch (selectedValue)
                {
                    case 1:
                        while (dungeon.gameClear == false)
                        {
                            // 몬스터 난이도 설정
                            dungeon.dungeonLevel = "쉬움";
                            MonsterManager.Instance.FixMonster(dungeon);

                            // 몬스터 리스트 리셋
                            MonsterManager.Instance.ResetMonsters();

                            // 보스 스테이지 도달 전 및 시작 전 베이스 캠프
                            if (dungeon.stage % 5 == 0) BaseDungeon(player, dungeon, items, inventory);
                            DungeonRaid(player, dungeon);

                        }
                        break;
                    case 2:
                        while (dungeon.gameClear == false)
                        {
                            // 몬스터 난이도 설정
                            dungeon.dungeonLevel = "쉬움";
                            MonsterManager.Instance.FixMonster(dungeon);

                            // 몬스터 리스트 리셋
                            MonsterManager.Instance.ResetMonsters();

                            // 보스 스테이지 도달 전 및 시작 전 베이스 캠프
                            if (dungeon.stage % 5 == 0) BaseDungeon(player, dungeon, items, inventory);
                            DungeonRaid(player, dungeon);

                        }
                        break;
                    case 3:
                        while (dungeon.gameClear == false)
                        {
                            // 몬스터 난이도 설정
                            dungeon.dungeonLevel = "쉬움";
                            MonsterManager.Instance.FixMonster(dungeon);

                            // 몬스터 리스트 리셋
                            MonsterManager.Instance.ResetMonsters();

                            // 보스 스테이지 도달 전 및 시작 전 베이스 캠프
                            if (dungeon.stage % 5 == 0) BaseDungeon(player, dungeon, items, inventory);
                            DungeonRaid(player, dungeon);

                        }
                        break;
                    case 0: return 0;
                }

            }
        }

        public void BaseDungeon(Player player, Dungeon dungeon, List<Item> items, List<Item> inventory)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("베이스 캠프");
                Console.WriteLine("이곳에서 나아가기 전 활동을 할 수 있습니다.\n");

                List<Option> options = new List<Option>
                {
                new Option { text = "나아가기", value = 1 },
                new Option { text = "휴식하기", value = 2 },
                new Option { text = "상점", value = 3 },
                new Option { text = "상태보기", value = 4 },
                new Option { text = "나가기", value = 0 },
                };

                int selectedValue = UIManager.inputController(options);

                switch (selectedValue)
                {
                    case 1: DungeonRaid(player, dungeon); break;
                    case 2: break;
                    case 3: ShopManager.Instance.GoShop(player, inventory); break;
                    case 4: PlayerManager.Instance.PlayerInfo(player); break;
                    case 0: return;
                }
            }
        }

        // 던전 몬스터 조우
        public void DungeonRaid(Player player, Dungeon dungeon)
        {
            List<Monster> monster = enemy.RandomMonster(dungeon.stage);
            bool skip = false;
            dungeon.dungeonClear = false;

            while (dungeon.dungeonClear == false)
            {
                message = "";
                Console.Clear();
                if (skip)
                {
                    message = "도망치기에 실패하였다!! \n 도망치다가 몬스터에게 한 방 맞아서 체력이 5 줄어들었다.";
                    player.health -= 5;
                    skip=false;
                }

                Console.WriteLine($"현재 난이도 : {dungeon.dungeonLevel}");
                Console.WriteLine("현재 스테이지 : {0} \n", dungeon.stage);

                OutputMonster(monster);

                Console.WriteLine("");
                Console.WriteLine("[플레이어]");
                Console.WriteLine("체력 : {0}", player.health);
                Console.WriteLine("마나 : {0}", player.mana);
                Console.WriteLine("Lv : {0} \n", player.level);

                if(message != "") Console.WriteLine(message);
                else Console.WriteLine($"{monster[rand.Next(0, monster.Count)].monsterInfo}");

                List<Option> options = new List<Option>
                {
                    new Option { text = "공격", value = 1 },
                    new Option { text = "도망가기", value = 0 },

                };

                int selectedValue = UIManager.inputController(options);

                switch (selectedValue)
                {
                    case 1: SkillManager.Instance.SelectSkill(monster, player, dungeon); break;
                    case 0:
                        {
                            bool success = rand.Next(0, 100) < 50;
                            if (success)
                            {
                                LeaveRaid(player);
                                return;

                            }
                            else
                            {
                                skip = true;
                                continue;
                            }
                        }
                        
                }
                Console.WriteLine(message);
            }
        }


        public void MonsterSelect(Player player, Dungeon dungeon, List<Monster> monsters, Skill skill)
        {
            
            while (dungeon.dungeonClear == false)
            {
                List<Option> options = new List<Option>();
                Console.Clear();

                Console.WriteLine($"현재 난이도 : {dungeon.dungeonLevel}");
                Console.WriteLine("현재 스테이지 : {0} \n", dungeon.stage);
                
                OutputMonster(monsters);
                int i = 0;
                foreach (var monster in monsters)
                {
                    i++;
                    options.Add(new Option
                    {
                       
                        text = $"[Lv. {monster.level}] {monster.name} \n", value = i,
                    });
                }
                    options.Add(new Option{ text = "뒤로가기", value = 0, });

                Console.WriteLine(message);

                int selectedValue = UIManager.inputController(options);

                switch (selectedValue)
                {
                    case 0: return;
                    default:
                        {
                            if (monsters[selectedValue-1].health <= 0)
                            {
                                message = "대상으로 선택할 수 없습니다.";
                                continue;
                            }
                            BattleInfo(monsters, monsters[selectedValue-1], player, skill, dungeon);
                            return;
                        }
                }
                Console.WriteLine($"\n[플레이어]");
                Console.WriteLine($"체력 : {player.health}");
            }
        }

        public void BattleInfo(List<Monster> monsters, Monster selectMonster, Player player, Skill skill, Dungeon dungeon)
        {
            bool isPlayerAttack = false;
            for (int j = 0; j < monsters.Count + 1; j++)
            {
                Console.Clear();

                // 플레이어의 공격 (1회)
                if (!isPlayerAttack)
                {
                    message = player.PlayerAttack(selectMonster, player, skill, dungeon);
                    isPlayerAttack = true;
                }

                OutputMonster(monsters);

                Console.WriteLine("\n[플레이어]");
                Console.WriteLine("체력 : {0}", player.health);
                Console.WriteLine("마나 : {0}", player.mana);
                Console.WriteLine($"Exp : {player.exp} / {player.maxEXP}");
                Console.WriteLine("Lv: {0}", player.level);

                Console.WriteLine();
                Console.WriteLine(message);
                Console.ReadLine();

                // 전부 다 처치 시 보상
                if (dungeon.deadCount == monsters.Count)
                {
                    ResultDungeon(monsters, player, dungeon);
                    return;
                }


                if (j == monsters.Count) return;

                message = monsters[j].MonsterAttack(monsters[j], player);
            }

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

            Console.Clear();

            Console.WriteLine("클리어!");

            Console.WriteLine($"스테이지 - {dungeons.stage} 을 클리어 하였습니다.\n");

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"{resultGold} Gold 획득");


            Console.ReadLine();

            message = "";
            dungeons.deadCount = 0;
            dungeons.dungeonClear = true;
            dungeons.stage++;
        }

        // 레이드에서 도망
        public void LeaveRaid(Player player)
        {
            Console.Clear();
            Console.WriteLine("당신은 재빨리 던전을 빠져나왔습니다.\n");
            int lostCoin = RandomNumber(50);
            Console.Write($"\n도망가는 동안 {lostCoin} Gold 잃었습니다!      \n");
            player.gold -= lostCoin;
            Console.WriteLine("\n아무키나 누르시면 던전입구로 갑니다.");
            Console.ReadLine();
        }

        // 숫자 랜덤
        public int RandomNumber(int num)
        {
            int finalNum = 0;
            finalNum = rand.Next(0, num);

            return finalNum;
        }

        // 사망 페널티
        public void DeathPenalty(Player player)
        {
            Console.WriteLine("플레이어가 사망하였습니다.");
            int randExp = RandomNumber(10);
            int randCoin = RandomNumber(10);

            player.exp -= randExp;
            player.gold -= randCoin;

            Console.WriteLine($"경험치가 {player.exp}만큼 소실되었습니다.");
            Console.WriteLine($"금화가 {player.gold}만큼 소실되었습니다.");

            bool isSteal = rand.Next(0, 100) < 50;

            // 인벤토리에서 가져가게끔
            if (isSteal)
            {
                // develop 브런치에서 pull해오면 추가 작성 예정
            }

            Console.WriteLine("\n아무키나 누르시면 던전입구로 갑니다.");
            Console.ReadLine();
        }
        public void OutputMonster(List<Monster> monster)
        {
            foreach (var monsters in monster)
                {
                    Console.Write($"[Lv. {monsters.level}] {monsters.name}  | ");
                    Console.WriteLine(monsters.health <= 0 ? "Dead" : $"HP : {monsters.health} ");
                }
        }
    }

}
