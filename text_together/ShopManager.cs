using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace textRPG
{
    class ShopManager
    {
        private Player player;
        private List<Item> storeItems;

        // 싱글톤
        private static ShopManager instance;

        public static ShopManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShopManager();
                }
                return instance;
            }
        }

        public List<Item> InitializeStore(Player player)
        {
            this.player = player;
            this.storeItems = new List<Item>();

            // 초기 판매 아이템
            storeItems.Add(new Item("수련자 갑옷", new Effect("방어력", 5), "수련에 도움을 주는 갑옷입니다.", 1000, false, false));
            storeItems.Add(new Item("무쇠 갑옷", new Effect("방어력", 9), "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500, false, false));
            storeItems.Add(new Item("스파르타 갑옷", new Effect("방어력", 15), "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, false));
            storeItems.Add(new Item("낡은 검 ", new Effect("공격력", 2), "쉽게 볼 수 있는 낡은 검 입니다.", 500, false, false));
            storeItems.Add(new Item("청동 도끼", new Effect("공격력", 5), "어디선가 사용됐던거 같은 도끼입니다.", 1500, false, false));
            storeItems.Add(new Item("스파르타의 창", new Effect("공격력", 7), "스파르타의 전사들이 사용했다는 전설의 창입니다.", 4000, false, false));

            return storeItems;
        }

        // 상점탭 관리
        public void GoShop(Player player, List<Item> items, List<Item> inventory)
        {
            int idx = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(idx);
                idx++;
                Console.WriteLine("상점\n 필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.gold} G\n");

                ShopInfo(items, inventory);
                Console.WriteLine();

                Console.WriteLine("1. 아이템 구매 \n2. 아이템 판매\n3. 아이템 뽑기\n0. 나가기");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                {
                    return;
                }
                else if (input == 1)
                {
                    ItemBuy(player, items, inventory);
                }
                else if (input == 2)
                {
                    ItemSell(player, items, inventory);
                }
                else if (input == 3)
                {
                    ItemGatcha(player, inventory);
                }
                else
                {
                    Console.WriteLine("다시 입력해주세요.");
                }
            }
        }
        void ItemGatcha(Player player, List<Item> inventory)
        {
            if(player.gold >= 150)
            {
                player.gold -= 150;

                Random random = new Random();
                int index = random.Next(ItemData.ItemPool.Count);
                Item randomItem = ItemData.ItemPool[index];

                Console.Clear();
                Console.WriteLine($"{randomItem.name}을(를) 뽑았습니다!");
                Console.WriteLine($"{randomItem.effect.type} +{randomItem.effect.value} | {randomItem.info}");
                Console.WriteLine();

                AddItem(randomItem, inventory, 150);
            }
            else
            {
                Console.WriteLine("저축해서 다시 와 주세요...");
            }

            Console.WriteLine("0. 나가기");
            int input = int.Parse(Console.ReadLine());
            if (input == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("다시 입력해주세요.");
            }

        }


        // 아이템들 상점에서 정보
        void ShopInfo(List<Item> items, List<Item> inventory)
        {
            Console.WriteLine("[아이템 목록]");
            int idx = 1;
            foreach (var item in items)
            {
                bool isHaved = inventory.Any(x => x.name == item.name);
                if (isHaved)
                {
                    Console.WriteLine($"- {idx} {item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | 보유 중");
                }
                else
                    Console.WriteLine($"- {idx} {item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.price} G");
                idx++;
            }
            Console.WriteLine();
        }
        // 아이템들 판매시 정보 
        void SellInfo(List<Item> inventory)
        {
            int idx = 1;
            Console.WriteLine("[아이템 목록]");
            foreach (var item in inventory)
            {
                if (item.isEquipped)
                    Console.WriteLine($"- {idx} [E]{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.price}");
                else
                    Console.WriteLine($"- {idx} {item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.price}");
                idx++;
            }
        }
        // 아이템 판매하고 UI 갱신
        void UpdateSellUI(Player player, List<Item> items, List<Item> inventory)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 판매할 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G\n");

            SellInfo(inventory);
            Console.WriteLine();
            Console.WriteLine("0. 나가기\n");

        }
        // 아이템 판매 관리
        void ItemSell(Player player, List<Item> items, List<Item> inventory)
        {

            while (true)
            {
                UpdateSellUI(player, items, inventory);
                Console.WriteLine("판매하고 싶은 아이템 번호를 입력해주세요.");
                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                {
                    return;
                }
                if (input > inventory.Count || input < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }
                if (inventory[input - 1].isEquipped)
                {
                    inventory[input - 1].isEquipped = false;
                }
                double sellPrice = inventory[input - 1].price * 0.85;
                //Console.WriteLine($"{inventory[input-1].name}이 {sellPrice:n1}가격에 팔렸습니다.");
                player.gold += (int)sellPrice;
                inventory.RemoveAt(input - 1);
                UpdateSellUI(player, items, inventory);
            }

        }
        // 아이템 구매하고 UI 갱신
        void UpdateBuyUI(Player player, List<Item> items, List<Item> inventory)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G\n");

            ShopInfo(items, inventory);
            Console.WriteLine();
            Console.WriteLine("0. 나가기\n");
        }
        // 아이템 구매 관리
        void ItemBuy(Player player, List<Item> items, List<Item> inventory)
        {
            while (true)
            {
                UpdateBuyUI(player, items, inventory);
                Console.WriteLine("구매하고 싶은 아이템 번호를 입력해주세요.");
                int input = int.Parse(Console.ReadLine());

                if (input == 0)
                {
                    return;
                }
                if (input > items.Count || input < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

                else
                {

                    if (items[input - 1].price > player.gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        continue;
                    }
                    else
                    {
                        AddItem(items[input-1], inventory, items[input - 1].price);
                        UpdateBuyUI(player, items, inventory);
                    }
                }

            }

        }

        void AddItem(Item item, List<Item> inventory, int price)
        {
            // 인벤토리에 같은 아이템이 있는지 탐색
            var haveItem = inventory.FirstOrDefault(x => x.name == item.name);

            // 이미 가지고 있을 때
            if(haveItem != null)
            {
                if(haveItem.quantity < haveItem.maxQuantity)
                {
                    player.gold -= price;
                    haveItem.quantity++;
                    Console.WriteLine($"{haveItem.name}을 {haveItem.quantity}째 얻었습니다!");
                }
                else
                {
                    Console.WriteLine($"{haveItem.name}은 최대 수량입니다. 더 이상 획득할 수 없습니다...");
                }
            }
            else
            {
                player.gold -= price;
                inventory.Add(new Item(item.name, item.effect, item.info, item.price, true, false, item.maxQuantity));
                Console.WriteLine($"{item.name}을(를) 획득했습니다!");
            }
        }
    }
}
