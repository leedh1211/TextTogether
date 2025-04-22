using textRPG;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using NAudio.Wave;


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
            // 아이템 추가
            items.Add(new Item("수련자 갑옷", new Effect("방어력", 5), "수련에 도움을 주는 갑옷입니다.", 1000, false, false));
            items.Add(new Item("무쇠 갑옷", new Effect("방어력", 9), "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500, false, false));
            items.Add(new Item("스파르타 갑옷", new Effect("방어력", 15), "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, false));
            items.Add(new Item("낡은 검 ", new Effect("공격력", 2), "쉽게 볼 수 있는 낡은 검 입니다.", 600, false, false));
            items.Add(new Item("청동 도끼", new Effect("공격력", 5), "어디선가 사용됐던거 같은 도끼입니다.", 1500, false, false));
            items.Add(new Item("스파르타의 창", new Effect("공격력", 7), "스파르타의 전사들이 사용했다는 전설의 창입니다.", 4000, false, false));
            // 던전 추가
            dungeon = new Dungeon("", 0);
        }
        // 게임 시작
        GameStart(player, items, inventory, dungeon);
        GameSaveState.Save(player, inventory, items, dungeon);
    }
}