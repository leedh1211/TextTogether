
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
    static void GameStart(Player player, List<Item> items, List<Item> inventory, Dungeon dungeon)
    {
        UIManager.DrawAscii(UIAscii.HomeArt);
        UIManager.WriteLine(2, "스파르타 마을에 오신 여러분 환영합니다.");
        UIManager.WriteLine(2, "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        
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
        
        switch (selectedValue)
        {
            case 1: PlayerManager.Instance.PlayerInfo(player); break;
            case 2: InventoryManager.Instance.GoInventory(player); break;
            case 3: ShopManager.Instance.GoShop(player, inventory); break;
            case 4: DungeonManager.Instance.GoDungeon(player, items, inventory, dungeon); break;
            case 5: RestManager.Instance.GoRest(player, items, inventory); break;
            case 6: RestManager.Instance.GoRest(player, items, inventory); break;
            case 7: return;
        }
        Console.ReadKey();
    }

    static void Main()
    {
        Player player;
        // 빈 인벤토리 만들기
        List<Item> inventory = InventoryManager.Instance.inventory;
        // 상점에 아이템들 추가
        List<Item> items = ShopManager.Instance.InitializeStore(); ;
        // 던전 추가
        Dungeon dungeon;

        if (!GameSaveState.TryLoad(out player, out inventory, out items, out dungeon))
        {
            // 초기 설정
            string playerName = PlayerManager.Instance.CheckName();
            PlayerManager.Job playerJob = PlayerManager.Instance.SelectJob();
            player = new Player(playerName, playerJob.ToString(), 1, 10, 5, 100, 100, 1500, 0, 10);
            items = new List<Item>();
            dungeon = new Dungeon();

            // 던전 추가
            dungeon = new Dungeon("", 0);
        }
        // 게임 시작
        int startActionResult = title.SelectTitleOption();
        if (startActionResult == 0)
        {
            View.View1();
            UIManager.UISetup();
            View.DrawUIFast();
            _ = UIManager.Call_CheckWindow();
            GameStart(player, items, inventory, dungeon);
        }
        GameSaveState.Save(player, inventory, items, dungeon);
    }
}