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
                    while(dungeon.gameClear == false)
                    {
                        // 몬스터 난이도 설정
                        dungeon.dungeonLevel = "쉬움";
                        MonsterManager.Instance.FixMonster(dungeon);
                        
                        // 몬스터 리스트 리셋
                        MonsterManager.Instance.ResetMonsters();

                        // 보스 스테이지 도달 전 및 시작 전 베이스 캠프
                        if(dungeon.stage % 5 == 0) BaseDungeon(player, dungeon, items, inventory);
                        DungeonRaid(player, dungeon);
                
                    }
                }
                else if (input == 2)
                {
                    dungeon.dungeonLevel = "보통";
                        MonsterManager.Instance.FixMonster(dungeon);

                        MonsterManager.Instance.ResetMonsters();

                        if(dungeon.stage % 5 == 0) BaseDungeon(player, dungeon, items, inventory);
                        DungeonRaid(player, dungeon);
                }
                else if (input == 3)
                {
                    dungeon.dungeonLevel = "어려움";
                        MonsterManager.Instance.FixMonster(dungeon);
                        
                        MonsterManager.Instance.ResetMonsters();

                        if(dungeon.stage % 5 == 0) BaseDungeon(player, dungeon, items, inventory);
                        DungeonRaid(player, dungeon);
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

                Console.WriteLine("1. 나아가기");
                Console.WriteLine("2. 휴식하기");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 상태보기");
                Console.WriteLine("5. 저장");
                Console.WriteLine("0. 나가기\n");
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
                    return;
                }
                else if (input == 2)
                {

                }
                else if (input == 3)
                {
                    ShopManager.Instance.GoShop(player, inventory);
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
            bool skip = false;
            dungeon.dungeonClear = false;
            
            while (dungeon.dungeonClear == false)
            {
                Console.Clear();
                if (skip)
                {
                    message = "도망치기에 실패하였다!! \n 도망치다가 몬스터에게 한 방 맞아서 체력이 5 줄어들었다.";
                    player.health -= 5;
                }

                Console.WriteLine($"현재 난이도 : {dungeon.dungeonLevel}");
                Console.WriteLine("현재 스테이지 : {0} \n", dungeon.stage);

                for (int i = 0; i < monster.Count; i++)
                {
                    Console.Write($"[Lv. {monster[i].level}] {monster[i].name}  | ");
                    Console.WriteLine(monster[i].health <= 0 ? "Dead" : $"HP : {monster[i].health} ");
                    //Console.WriteLine("Pow : {0} ", monster[i].attack);
                    //Console.WriteLine("Def : {0} ", monster[i].shield);
                    //Console.WriteLine("Gold : {0} \n", monster[i].gold);
                }

                Console.WriteLine("");
                Console.WriteLine("[플레이어]");
                Console.WriteLine("체력 : {0}", player.health);
                Console.WriteLine("마나 : {0}", player.mana);
                Console.WriteLine("Lv : {0} \n", player.level);

                Console.WriteLine("1. 공격 ");
                Console.WriteLine("0. 도망가기 \n");

                Console.WriteLine(message);

                // if (dungeon.deadCount == monster.Count)
                //     Console.WriteLine("원하시는 행동을 입력해주세요.");

                Console.Write(">>");

                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                {
                    bool success = rand.Next(0, 100) < 50;
                    Console.WriteLine("입력값: " + input);
                    if (success)
                    {
                        LeaveRaid(player);
                        return;
                    }
                    else
                    {
                        skip = true;
                    }
                }
                else if (input == 1 || skip)
                {
                    SkillManager.Instance.SelectSkill(monster, player, dungeon);
                }
                else
                {
                    Console.WriteLine("다시 입력해주세요");
                    continue;
                }
            }
        }


        public void MonsterSelect(Player player, Dungeon dungeon, List<Monster> monster, Skill skill)
        {
            while (dungeon.dungeonClear == false)
            {
                Console.Clear();

                Console.WriteLine($"현재 난이도 : {dungeon.dungeonLevel}");
                Console.WriteLine("현재 스테이지 : {0} \n", dungeon.stage);

                for (int i = 0; i < monster.Count; i++)
                {
                    Console.Write($"{i+1}. [Lv. {monster[i].level}] {monster[i].name}  | ");
                    Console.WriteLine(monster[i].health <= 0 ? "Dead" : $"HP : {monster[i].health} ");
                }

                Console.WriteLine($"\n[플레이어]");
                Console.WriteLine($"체력 : {player.health}");

                Console.WriteLine(message);
                Console.Write(">>");

                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                    return;
                else if (input >= 1 && input <= monster.Count)
                {
                    // 이미 죽인 몬스터는 예외 처리
                    if (monster[input - 1].health <= 0)
                    {
                        message = "대상으로 선택할 수 없습니다.";
                        continue;
                    }
                    BattleInfo(monster, monster[input - 1], player, skill, dungeon);
                    return;
                }
                else
                {
                    Console.WriteLine("다시 입력해주세요");
                    continue;
                }
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

                for (int i = 0; i < monsters.Count; i++)
                {
                    Console.Write($"[Lv. {monsters[i].level}] {monsters[i].name}  | ");
                    Console.WriteLine(monsters[i].health <= 0 ? "Dead" : $"HP : {monsters[i].health} ");
                }

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
                dungeons.deadCount=0;
                dungeons.dungeonClear = true;
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

    }

}
