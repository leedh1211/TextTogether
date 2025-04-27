using NAudio.Codecs;
using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    class DungeonManager
    {
        bool skip = false;
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
            UIManager.Change_isDungeon();

            while (true)
            {
                dungeon.gameClear = false;

                UIManager.Clear(1);
                UIManager.Clear(2);
                UIManager.Clear(3);
                UIManager.DrawAscii(UIAscii.DungeonArt);
                UIManager.WriteLine(2,"던전입장");
                UIManager.WriteLine(2,"이곳에서 던전으로 들어가기전 난이도를 설정 할 수 있습니다.");

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
                        // 몬스터 리스트 리셋
                        MonsterManager.Instance.ResetMonsters();
                        // 몬스터 난이도 설정
                        dungeon.dungeonLevel = "쉬움";
                        MonsterManager.Instance.FixMonster(dungeon);
                        while (dungeon.gameClear == false)
                        {
                            // 보스 스테이지 도달 전 및 시작 전 베이스 캠프
                            if (dungeon.stage % 5 == 0) BaseDungeon(player, dungeon, items, inventory);
                            else DungeonRaid(player, dungeon);
                            if (skip)
                            {
                                UIManager.Change_isDungeon();
                                return 0;
                            }
                            // 플레이어 체력 0이하가 되면 처음 페이지로 리턴
                            if (dungeon.isPlayerDead == true)
                            {
                                UIManager.Change_isDungeon();
                                return 0;
                            }
                            // 엔딩 설정
                            if (dungeon.stage == 31)
                            {
                                UIManager.Change_isDungeon();
                                EndingCredit(dungeon);
                            }
                        }
                        break;
                    case 2:
                        MonsterManager.Instance.ResetMonsters();
                        dungeon.dungeonLevel = "보통";
                        MonsterManager.Instance.FixMonster(dungeon);
                        while (dungeon.gameClear == false)
                        {
                            if (dungeon.stage % 5 == 0) BaseDungeon(player, dungeon, items, inventory);
                            else DungeonRaid(player, dungeon);
                            if (skip)
                            {
                                UIManager.Change_isDungeon();
                                return 0;
                            }
                            if (dungeon.isPlayerDead == true)
                            {
                                UIManager.Change_isDungeon();
                                return 0;
                            }
                            if (dungeon.stage == 31)
                            {
                                UIManager.Change_isDungeon();
                                EndingCredit(dungeon);
                            }
                        }
                        break;
                    case 3:
                        MonsterManager.Instance.ResetMonsters();
                        dungeon.dungeonLevel = "어려움";
                        MonsterManager.Instance.FixMonster(dungeon);
                        while (dungeon.gameClear == false)
                        {
                            if (dungeon.stage % 5 == 0) BaseDungeon(player, dungeon, items, inventory);
                            else DungeonRaid(player, dungeon);
                            if (skip)
                            {
                                UIManager.Change_isDungeon();
                                return 0;
                            }

                            if (dungeon.isPlayerDead == true)
                            {
                                UIManager.Change_isDungeon();
                                return 0;
                            }
                            if (dungeon.stage == 31)
                            {
                                UIManager.Change_isDungeon();
                                EndingCredit(dungeon);
                            }
                        }
                        break;
                    case 0: return 0;
                }
                return 0;

            }
        }

        public void BaseDungeon(Player player, Dungeon dungeon, List<Item> items, List<Item> inventory)
        {
            while (dungeon.stage % 5 == 0 && dungeon.isPlayerDead == false)
            {
                UIManager.Clear(2);
                UIManager.WriteLine(2,"베이스 캠프");
                UIManager.WriteLine(2,"이곳에서 나아가기 전 활동을 할 수 있습니다.");

                List<Option> options = new List<Option>
                {
                new Option { text = "나아가기", value = 1 },
                new Option { text = "휴식하기", value = 2 },
                new Option { text = "상점", value = 3 },
                new Option { text = "인벤토리", value = 4 },
                new Option { text = "상태보기", value = 5 },
                new Option { text = "나가기", value = 0 },
                };

                int selectedValue = UIManager.inputController(options);

                switch (selectedValue)
                {
                    case 1: DungeonRaid(player, dungeon); break;
                    case 2: RestManager.Instance.GoRest(player, items, inventory); break;
                    case 3: ShopManager.Instance.GoShop(player, inventory); break;
                    case 4: InventoryManager.Instance.GoInventory(player,inventory); break;
                    case 5: PlayerManager.Instance.PlayerInfo(player); break;
                    case 0: return;
                }
            }
        }

        // 던전 몬스터 조우
        public void DungeonRaid(Player player, Dungeon dungeon)
        {
            SoundManager.Stop_BGM();
            Thread thread = new Thread(() => SoundManager.BGM(rand.Next(0, SoundManager.BGM_List.Count)));
            thread.Start();

            UIManager.Clear(1);
            List<Monster> monster = enemy.RandomMonster(dungeon.stage);
            skip = false;
            dungeon.dungeonClear = false;
            dungeon.isPlayerDead = false;

            UIManager.EnemySetPosition(monster);

            for (int i = 0; i < monster.Count; i++)
            {
                UIManager.DrawEnemy(i, monster[i]);
                UIManager.DrawEnemyName(i, monster[i]);
                monster[i].maxHealth = monster[i].health;
            }


            UIManager.DrawPlayer(UIAscii.PlayerBehind);

                while (dungeon.dungeonClear==false)
            {
                message = "";
                UIManager.Clear(2);
                if (skip)
                {
                    message = "도망치기에 실패하였다!!  도망치다가 몬스터에게 한 방 맞아서 체력이 5 줄어들었다.";
                    player.health -= 5;
                    skip=false;
                }

                if(player.health <= 0 )
                {
                    DeathPenalty(player, dungeon);
                    dungeon.dungeonClear=true;
                    player.health = 100;
                    break;
                }

                UIManager.WriteLine(2,$"현재 난이도 : {dungeon.dungeonLevel}");
                UIManager.WriteLine(2,$"현재 스테이지 : {dungeon.stage} ");
                UIManager.WriteLine(2,"");


                for(int i = 0; i< monster.Count; i++)
                {
                    UIManager.DrawHPBar(i, monster[i]);
                    UIManager.PlayerHPBar(player);
                }
                

                if(message != "") UIManager.WriteLine(2,message);
                else UIManager.TypingLine(2,$"{monster[rand.Next(0, monster.Count)].monsterInfo}");

                List<Option> options = new List<Option>
                {
                    new Option { text = "공격", value = 1 },
                    new Option { text = "인벤토리", value = 2 },
                    new Option { text = "도망가기", value = 0 },
                };

                int selectedValue = UIManager.inputController(options);

                switch (selectedValue)
                {
                    case 1: SkillManager.Instance.SelectSkill(monster, player, dungeon); break;
                    case 2: InventoryManager.Instance.GoDungeonInventory(player); break;
                    case 0:
                        {
                            bool success = rand.Next(0, 100) < 50;
                            if (success)
                            {
                                LeaveRaid(player);
                                dungeon.deadCount = 0;
                                skip = true;
                                return;

                            }
                            else
                            {
                                skip = true;
                                continue;
                            }
                        }
                        
                }
                UIManager.WriteLine(2,message);
            }
        }


        public void MonsterSelect(Player player, Dungeon dungeon, List<Monster> monsters, Skill skill)
        {

            
            while (dungeon.dungeonClear == false)
            {
                List<Option> options = new List<Option>();
                //UIManager.Clear(1);
                UIManager.Clear(2);
                UIManager.Clear(3);

                UIManager.WriteLine(2,$"현재 난이도 : {dungeon.dungeonLevel}");
                UIManager.WriteLine(2,$"현재 스테이지: {dungeon.stage} " );
                
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

                UIManager.WriteLine(2,message);


                UIManager.Change_isTarget();
                int selectedValue = UIManager.inputController(options);
                
                switch (selectedValue)
                {
                    case 0: UIManager.Change_isTarget(); UIManager.ClearTargetBOx(); Console.ForegroundColor = ConsoleColor.White; ; return;
                    default:
                        {
                            if (monsters[selectedValue-1].health <= 0)
                            {
                                message = "대상으로 선택할 수 없습니다.";
                                continue;
                            }
                            UIManager.ClearTargetBOx();
                            UIManager.Change_isTarget();
                            BattleInfo(monsters, monsters[selectedValue-1], player, skill, dungeon);
                            UIManager.DrawHPBar(selectedValue - 1 , monsters[selectedValue - 1]);



                            return;
                        }
                }
                UIManager.WriteLine(2,$"[플레이어]");
                UIManager.WriteLine(2,$"체력 : {player.health}");
            }
        }

        public void BattleInfo(List<Monster> monsters, Monster selectMonster, Player player, Skill skill, Dungeon dungeon)
        {
            bool isPlayerAttack = false;
            for (int j = 0; j < monsters.Count + 1; j++)
            {
                //UIManager.Clear(1);
                UIManager.Clear(2);
                UIManager.Clear(3);

                // 플레이어의 공격 (1회)
                if (!isPlayerAttack)
                {
                    message = player.PlayerAttack(selectMonster, player, skill, dungeon);
                    isPlayerAttack = true;
                }

                OutputMonster(monsters);

                List<string> messages = new List<string>();
                string[] massageTemp = message.Split('\n');

                


                for(int i = 0; i < massageTemp.Length; i++) { 
                    UIManager.TypingLine(2, massageTemp[i]);
                }
                
                NextEnter();

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

            UIManager.Clear(1);
            UIManager.Clear(2);
            UIManager.Clear(3);
            
            UIManager.WriteLine(2,"클리어!");

            UIManager.WriteLine(2,$"스테이지 - {dungeons.stage} 을 클리어 하였습니다.");

            UIManager.WriteLine(2,"[탐험 결과]");
            UIManager.WriteLine(2,$"{resultGold} Gold 획득");

            NextEnter();

            message = "";
            dungeons.deadCount = 0;
            dungeons.dungeonClear = true;
            dungeons.stage++;
        }

        // 레이드에서 도망
        public void LeaveRaid(Player player)
        {
            SoundManager.Stop_BGM();
            UIManager.Clear(1);
            UIManager.Clear(2);
            UIManager.Clear(3);
            UIManager.WriteLine(2,"당신은 재빨리 던전을 빠져나왔습니다.");
            int lostCoin = RandomNumber(50);
            UIManager.Write(2,$"도망가는 동안 {lostCoin} Gold 잃었습니다!      ");
            player.gold -= lostCoin;
            List<Option> options = new List<Option>
            {
                new Option { text = "확인", value = 0 },
            };
            UIManager.inputController(options);
        }

        // 숫자 랜덤
        public int RandomNumber(int num)
        {
            int finalNum = 0;
            finalNum = rand.Next(0, num);

            return finalNum;
        }

        // 사망 페널티
        public void DeathPenalty(Player player, Dungeon dungeon)
        {
            UIManager.WriteLine(2,"당신의 눈앞이 어두워졌다... ");
            int randExp = RandomNumber(10);
            int randCoin = RandomNumber(10);

            dungeon.deadCount = 0;
            dungeon.stage = 1;
            dungeon.isPlayerDead = true;

            player.exp -= randExp;
            player.gold -= randCoin;

            UIManager.WriteLine(2,$"경험치가 {player.exp}만큼 소실되었습니다.");
            UIManager.WriteLine(2,$"금화가 {player.gold}만큼 소실되었습니다.");

            // bool isSteal = rand.Next(0, 100) < 50;

            // // 인벤토리에서 가져가게끔
            // if (isSteal)
            // {
            //     // develop 브런치에서 pull해오면 추가 작성 예정
            // }

            UIManager.WriteLine(2,"아무키나 누르시면 던전입구로 갑니다.");
            NextEnter();
            return;
        }

        public static String getMonsterInfoText(Monster monster)
        {
            string monsterText = "[Lv." + monster.level + "]"+monster.name;
            if (monster.health <= 0)
            {
                monsterText += "Dead";
            }
            else
            {
                monsterText += "HP : "+monster.health;
            }
            return monsterText;
        }
        
        public void OutputMonster(List<Monster> monster)
        {

            // 수정
            foreach (var monsters in monster)
                {
                    //UIManager.Write(2,$"[Lv. {monsters.level}] {monsters.name}  | ");
                    //UIManager.WriteLine(2,monsters.health <= 0 ? "Dead" : $"HP : {monsters.health} ");
                }
        }

        public void EndingCredit(Dungeon dungeon)
        {
            Console.Clear();
            Console.WriteLine($"축하합니다. {dungeon.dungeonLevel} 난이도를 클리어 하셨습니다.");
            Console.WriteLine("만든이 주루룩");
            NextEnter();

            dungeon.stage = 1;
            dungeon.gameClear = true;
        }


        public void NextEnter()
        {
            List<Option> options = new List<Option>
            {
                new Option { text = "확인", value = 0 },
            };
            UIManager.inputController(options);
        }
    }

}
