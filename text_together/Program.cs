using textRPG;
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
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태보기 \n2. 인벤토리 \n3. 상점\n4. 던전 입장\n5. 휴식하기\n6. 종료하기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = int.Parse(Console.ReadLine());
            if (input == 6)
            {
                return;
            }
            if (input == 1)
                PlayerManager.Instance.PlayerInfo(player);
            else if (input == 2)
            {
                InventoryManager.Instance.GoInventory(player, items, inventory);
            }
            else if (input == 3)
            {
                ShopManager.Instance.GoShop(player, items, inventory);
            }
            else if (input == 4)
            {
                DungeonManager.Instance.GoDungeon(player, items, inventory, dungeon);
            }
            else if (input == 5)
            {
                RestManager.Instance.GoRest(player, items, inventory);
            }
            else
            {
                Console.WriteLine("다시 입력해주세요.");
            }
        }
    }
    static void Main()
    {
        Player player;
        // 빈 인벤토리 만들기
        List<Item> inventory;
        // 상점에 아이템들 추가
        List<Item> items;
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
            
            // 인벤토리 초기화
            inventory = InventoryManager.Instance.inventory;

            // 상점 초기화
            items = ShopManager.Instance.InitializeStore(player);

            // 던전 추가
            dungeon = new Dungeon("", 0);
        }
        // 게임 시작
        GameStart(player, items, inventory, dungeon);
        GameSaveState.Save(player, inventory, items, dungeon);
    }
}