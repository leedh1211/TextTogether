
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using NAudio.Wave;
using text_together;



class Solution
{
    
    // 메인 화면 탭 관리
    static void GameStart(Player player, List<Item> items, List<Item> inventory, Dungeon dungeon, List<Quest> quests)
    {
        int status = 0;
        try
        {
            while (true)
            {
                Console.Clear();
                View.DrawUIFast();
                switch (status)
                {
                    case 0: status = mainMenu(dungeon); break;
                    case 1: status = PlayerManager.Instance.PlayerInfo(player); break;
                    case 2: status = InventoryManager.Instance.GoInventory(player, inventory); break;
                    case 3: status = ShopManager.Instance.GoShop(player, inventory); break;
                    case 4: status = DungeonManager.Instance.GoDungeon(player, items, inventory, dungeon); break;
                    case 5: status = RestManager.Instance.GoRest(player, items, inventory); break;
                    case 6: status = QuestManager.Instance.GoQuest(player, quests, inventory); break;
                    case 7: return;
                }
            }
        }

        catch
        {

        }
    }

    static void Main()
    {
        SoundManager.Sound_Init();
        Player player;
        // 빈 인벤토리 만들기
        List<Item> inventory = InventoryManager.Instance.inventory;
        // 상점에 아이템들 추가
        List<Item> items = ShopManager.Instance.InitializeStore(); ;
        // 던전 추가
        Dungeon dungeon; 
        // 퀘스트 초기화
        List<Quest> quests = QuestManager.Instance.quests;
        // 게임 시작
        int startActionResult = title.SelectTitleOption();
        _ = UIManager.Call_CheckWindow();
        if (startActionResult == 0)
        {
            int selectFile = title.SelectSaveFile();
            Console.Clear();
            View.View1();
            UIManager.UISetup();
            View.DrawUIFast();
            String FileName = "slot"+selectFile+".json";
            if (!GameSaveState.TryLoad(out player, out inventory, out items,out quests, out dungeon, FileName))
            {
                // 초기 설정
                string playerName = PlayerManager.Instance.CheckName();
                PlayerManager.Job playerJob = PlayerManager.Instance.SelectJob();
                player = new Player(playerName, playerJob.ToString(), 1, 10, 5,100, 100,100, 100, 1500, 0, 10);
                items = new List<Item>();
                dungeon = new Dungeon();
                quests = QuestManager.Instance.QuestInit();
        
                inventory = InventoryManager.Instance.inventory;
        
                // 던전 추가
                dungeon = new Dungeon("", 1,false, false, 0, false);
            }

            GameStart(player, items, inventory, dungeon, quests);

            GameSaveState.Save(player, inventory, items,quests, dungeon, FileName);
        }
        else
        {
            return;
        }
    }

    static int mainMenu(Dungeon dungeon)
    {

        
        UIManager.DrawAscii(UIAscii.HomeArt);
        UIManager.Clear(2);
        UIManager.WriteLine(2, "스파르타 마을에 오신 여러분 환영합니다.");
        UIManager.WriteLine(2, "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        
        Random rand = new Random();
        
        SoundManager.Stop_BGM();
        Thread thread = new Thread(() => SoundManager.BGM(rand.Next(9, 16)));
        thread.Start();
        
        List<Option> options = new List<Option>
        {
            new Option { text = "상태보기", value = 1 },
            new Option { text = "인벤토리", value = 2 },
            new Option { text = "상점", value = 3 },
            new Option { text = "던전 입장", value = 4 },
            new Option { text = "휴식하기", value = 5 },
            new Option { text = "퀘스트", value = 6 },
            new Option { text = "종료하기", value = 7 },
        };

        int selectedValue = UIManager.inputController(options);
        return selectedValue;
    }
}