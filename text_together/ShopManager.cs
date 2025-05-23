﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;

namespace text_together
{
    class ShopManager
    {
        private Player player;
        public List<Item> storeItems;

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

        public List<Item> InitializeStore()
        {
            this.storeItems = new List<Item>();

            // 초기 판매 아이템
            storeItems.Add(new Item("수련자 갑옷", new Effect("방어력", 5), "수련에 도움을 주는 갑옷입니다.", 100, false, false));
            storeItems.Add(new Item("무쇠 갑옷", new Effect("방어력", 9), "무쇠로 만들어져 튼튼한 갑옷입니다.", 150, false, false));
            storeItems.Add(new Item("스파르타 갑옷", new Effect("방어력", 15), "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 350, false, false));
            storeItems.Add(new Item("생명력 포션", new Effect("포션", 10), "생명력을 올리는 포션이다", 50, false, false));
            storeItems.Add(new Item("낡은 검 ", new Effect("공격력", 2), "쉽게 볼 수 있는 낡은 검 입니다.", 50, false, false));
            storeItems.Add(new Item("청동 도끼", new Effect("공격력", 5), "어디선가 사용됐던거 같은 도끼입니다.", 150, false, false));
            storeItems.Add(new Item("스파르타의 창", new Effect("공격력", 7), "스파르타의 전사들이 사용했다는 전설의 창입니다.", 400, false, false));

            return storeItems;
        }

        // 상점 갱신
        public int UpdateShop()
        {
            // 아이템 풀 셔플 리스트 만들기
            List<Item> shuffle = new List<Item>(ItemData.ItemPool);

            Random random = new Random();

            for (int i = shuffle.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Item temp = shuffle[i];
                shuffle[i] = shuffle[j];
                shuffle[j] = temp;
            }

            storeItems.Clear();

            // 6개로 개수제한
            for(int i = 0; i < 6; i++)
            {
                Item original = shuffle[i];

                // 깊은 복사
                Item updateItem = new Item(
                    original.name,
                    new Effect(original.effect.type, original.effect.value),
                    original.info,
                    original.price,
                    false,
                    false,
                    original.maxQuantity
                );

                updateItem.quantity = 1;
                storeItems.Add(updateItem);
            }

            return 6;
        }

        // 상점탭 관리
        public int GoShop(Player player, List<Item> inventory)
        {
            this.player = player;
            while (true)
            {
                UIManager.Clear(1);
                UIManager.DrawAscii(UIAscii.ShopArt);
                UIManager.Clear(2);   
                UIManager.WriteLine(2,"[상점]");
                UIManager.WriteLine(2,"필요한 아이템을 얻을 수 있는 상점입니다.");
                UIManager.WriteLine(2,"[보유 골드]");
                
                UIManager.WriteLine(2,$"{player.gold} G\n");

                // ShopInfo(inventory);
                PrintShopItemInfo();
                
                List<Option> options = new List<Option>
                {
                    new Option { text = "아이템 구매", value = 1 },
                    new Option { text = "아이템 판매", value = 2 },
                    new Option { text = "아이템 뽑기", value = 3 },
                    new Option { text = "장비 강화", value = 4 },
                    new Option { text = "상점 물품 업데이트", value = 5 },
                    new Option { text = "나가기", value = 0 }
                };

                int selectedValue = UIManager.inputController(options);
                UIManager.isShopUIList = false;
                switch (selectedValue)
                {
                    case 1: ItemBuy(inventory); break;
                    case 2: ItemSell(inventory); break;
                    case 3: ItemGatcha(inventory); break;
                    case 4: EnforceItem(inventory); break;
                    case 5: UpdateShop(); break;
                    case 6: GoShop(player, inventory); break;
                    case 0: return 0;
                }
            }
        }

        // 뽑기 기능
        void ItemGatcha(List<Item> inventory)
        {
            UIManager.Clear(2);
            UIManager.Clear(3);

            if (player.gold >= 150)
            {
                player.gold -= 150;

                Random random = new Random();
                int index = random.Next(ItemData.ItemPool.Count);
                Item randomItem = ItemData.ItemPool[index];

                UIManager.WriteLine(2, $"{randomItem.name}을(를) 뽑았습니다!");
                UIManager.WriteLine(2, $"{randomItem.effect.type} +{randomItem.effect.value} | {randomItem.info}");

                AddItem(randomItem, inventory, 150);
            }
            else
            {
                UIManager.WriteLine(2, "저축해서 다시 와 주세요...");
            }

            List<Option> options = new List<Option>
            {
                new Option { text = "나가기", value = 0 }
            };

            UIManager.inputController(options);
        }


        // 아이템들 상점에서 정보
        void ShopInfo( List<Item> inventory)
        {
            UIManager.WriteLine(2,"[아이템 목록]");
            int idx = 1;
            foreach (var item in storeItems)
            {
                bool isHaved = inventory.Any(x => x.name == item.name);
                if (isHaved)
                {
                    UIManager.WriteLine(2,$"- {idx} {item.name.PadRight(10)}|보유 중");
                }
                //| {item.effect.type} + {item.effect.value} | {item.info} | 
                else
                {
                    UIManager.WriteLine(2,$"- {idx} {item.name.PadRight(10)} {item.price} G");
                }
                //| {item.effect.type} + {item.effect.value} | {item.info} |
                idx++;
            }
        }
        // 아이템들 판매시 정보 
        void InventoryInfo(List<Item> inventory)
        {
            int idx = 1;
            // Console.WriteLine("[아이템 목록]");
            foreach (var item in inventory)
            {
                if (item.isEquipped)
                    UIManager.WriteLine(2,$"- {idx} [E]{item.name.PadRight(10)}|{item.price} | {item.quantity}"); //| {item.effect.type} + {item.effect.value}  {item.info} |  
                else //{item.effect.type} + {item.effect.value} | {item.info} |
                    UIManager.WriteLine(2,$"- {idx} {item.name.PadRight(10)}|  {item.price} | {item.quantity}");
                idx++;
            }
        }
        // 아이템 판매하고 UI 갱신
        List<Option> UpdateSellUI(List<Item> inventory)
        {
            UIManager.Clear(2);
            UIManager.Clear(3);
            UIManager.WriteLine(2,"[상점 - 아이템 판매]");
            UIManager.WriteLine(2,"필요 없는 아이템을 판매할 수 있습니다.");

            UIManager.WriteLine(2,"[보유 골드]");
            UIManager.WriteLine(2,$"{player.gold} G");

            InventoryInfo(inventory);
            List<Option> options = new List<Option>
            {
                new Option { text = "나가기", value = 0 },
            };

            int idx = 1;
            foreach (var item in inventory)
            {
                Option targetOption = new Option { text = item.name, value = idx };
                options.Add(targetOption);
                idx++;
            }

            return options;
        }

        // 아이템 판매 관리
        void ItemSell(List<Item> inventory)
        {
            while (true)
            {
                double sellPrice = 0;

                List<Option> options = UpdateSellUI(inventory);
                UIManager.WriteLine(2, "판매하고 싶은 아이템을 선택해주세요.");

                int input = UIManager.inputController(options);

                UIManager.Clear(2);
                UIManager.Clear(3);

                if (input == 0)
                {
                    return;
                }
                else if (inventory[input - 1].quantity > 1)
                {
                    sellPrice = inventory[input - 1].price * 0.85;
                    UIManager.WriteLine(2, $"{inventory[input - 1].name}이 {sellPrice:n1}가격에 팔렸습니다.");
                    inventory[input - 1].quantity -= 1;

                }
                else if (inventory[input - 1].isEquipped)
                {
                    sellPrice = inventory[input - 1].price * 0.85;
                    UIManager.WriteLine(2, $"{inventory[input - 1].name}이 {sellPrice:n1}가격에 팔렸습니다.");
                    inventory[input - 1].isEquipped = false;
                    inventory.RemoveAt(input - 1);
                }
                else
                {
                    sellPrice = inventory[input - 1].price * 0.85;
                    UIManager.WriteLine(2, $"{inventory[input - 1].name}이 {sellPrice:n1}가격에 팔렸습니다.");
                    inventory.RemoveAt(input - 1);
                }

                player.gold += (int)sellPrice;

                List<Option> exitOptions = new List<Option>
                {
                    new Option { text = "나가기", value = 0}
                };

                UIManager.inputController(exitOptions);

                UpdateSellUI(inventory);
            }

        }

        // 아이템 구매할때 UI 갱신
        List<Option> UpdateBuyUI(List<Item> inventory)
        {
            UIManager.Clear(2);
            UIManager.Clear(3);
            UIManager.WriteLine(2,"상점 - 아이템 구매");
            UIManager.WriteLine(2,"필요한 아이템을 얻을 수 있습니다.\n");

            UIManager.WriteLine(2,"[보유 골드]");
            UIManager.WriteLine(2,$"{player.gold} G");

            List<Option> options = new List<Option>
            {
                new Option { text = "나가기", value = 0 },
            };

            int idx = 1;
            foreach (var item in storeItems)
            {
                Option targetOption = new Option { text = item.name, value = idx };
                options.Add(targetOption);
                
                idx++;
            }

            // ShopInfo(inventory);
            PrintShopItemInfo();
            return options;
        }

        // 아이템 구매 관리
        void ItemBuy(List<Item> inventory)
        {
            
            while (true)
            {
                List<Option> options = UpdateBuyUI(inventory);
                UIManager.WriteLine(2, "구매하고 싶은 아이템을 선택해 주세요.");
                int input = UIManager.inputController(options, 2, "store");

                UIManager.Clear(2);
                UIManager.Clear(3);

                if (input == 0)
                {
                    UIManager.isShopUIList = false;
                    return;
                }

                else
                {
                    if (storeItems[input - 1].price > player.gold)
                    {
                        UIManager.WriteLine(2, "Gold가 부족합니다.");
                    }
                    else
                    {
                        AddItem(storeItems[input - 1], inventory, storeItems[input - 1].price);
                    }

                    List<Option> exitOptions = new List<Option>
                    {
                        new Option { text = "나가기", value = 0}
                    };

                    UIManager.inputController(exitOptions);
                }
                UpdateBuyUI(inventory);
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
                    UIManager.WriteLine(2, $"{haveItem.name}을 {haveItem.quantity} 개째 얻었습니다!");
                }
                else
                {
                    UIManager.WriteLine(2, $"{haveItem.name}은 최대 수량입니다. 더 이상 획득할 수 없습니다...");
                }
            }
            else
            {
                player.gold -= price;
                inventory.Add(new Item(item.name, item.effect, item.info, item.price, true, false, item.maxQuantity));
                UIManager.WriteLine(2, $"{item.name}을(를) 획득했습니다!");
            }
        }

        // 아이템 강화
        void EnforceItem(List<Item> inventory)
        {
            while(true)
            {
                UIManager.Clear(2);
                UIManager.Clear(3);

                UIManager.WriteLine(2, "아이템을 강화해봅시다!");
                UIManager.WriteLine(2, "강화할 아이템을 선택해 주세요.");

                InventoryInfo(inventory);

                List<Option> options = new List<Option> { new Option { text = "나가기", value = 0 } };

                for (int i = 0; i < inventory.Count; i++)
                {
                    options.Add(new Option { text = inventory[i].name, value = i + 1 });
                }


                int input = UIManager.inputController(options);

                if (input == 0)
                {
                    return;
                }

                Item upgradeItem = inventory[input - 1];

                // 강화를 거듭할수록 실패확률이 오른다
                int defaultSuccess = 100;
                int penalty = 15;
                int success = defaultSuccess -= (upgradeItem.upgradeLevel * penalty);
                
                // 최소 10퍼센트 성공률
                success = Math.Max(success, 10);


                if(upgradeItem.effect.type == "포션")
                {
                    UIManager.WriteLine(2, "소모품은 강화할 수 없습니다.");
                    continue;
                }

                else if(input < 1 || input > inventory.Count)
                {
                    UIManager.WriteLine(2, "잘못된 입력입니다.");
                    continue;
                }

                else if (player.gold < 100)
                {
                    UIManager.WriteLine(2, "골드가 부족합니다...");
                    break;
                }

                UIManager.Clear(2);
                UIManager.Clear(3);

                UIManager.WriteLine(2, $"{upgradeItem.name}을(를) 강화합니다.");
                UIManager.WriteLine(2, $"현재 {upgradeItem.upgradeLevel}번 강화했습니다.");
                UIManager.WriteLine(2, $"성공 확률: {success} %");
                UIManager.WriteLine(2, "");
                UIManager.WriteLine(2, $"강화하시겠습니까?");

                List<Option> confirm = new List<Option>
                {
                    new Option { text = "예", value = 1 },
                    new Option { text = "아니오", value = 2 }
                };

                int confirmInput = UIManager.inputController(confirm);

                if (confirmInput == 2)
                {
                    continue;
                }

                player.gold -= 100;

                // 장착 중이라면 해제
                if (upgradeItem.isEquipped)
                {
                    upgradeItem.isEquipped = false;
                }

                // 성공률보다 작거나 같은 숫자를 뽑았을 때 성공
                Random randomRoll = new Random();
                int roll = randomRoll.Next(1, 101);

                UIManager.Clear(2);
                UIManager.Clear(3);

                if (roll <= success)
                {
                    upgradeItem.upgradeLevel++;
                    upgradeItem.effect.value += 2;
                    UIManager.WriteLine(2, "강화 성공!");
                    UIManager.WriteLine(2, $"{upgradeItem.name}의 공격력/방어력이 {upgradeItem.effect.value}가 되었습니다.");
                }

                else
                {
                    UIManager.WriteLine(2, "강화 실패...");
                    upgradeItem.quantity--;
                    UIManager.WriteLine(2, "장비가 파괴되었습니다...");

                    if(upgradeItem.quantity == 0)
                    {
                        inventory.Remove(upgradeItem);
                    }
                }

                List<Option> exitOption = new List<Option>
                {
                    new Option { text = "나가기", value = 0 }
                };

                UIManager.inputController(exitOption);
            }
        }
        
        public void PrintShopItemInfo(){
            UIManager.isShopUIList = true;
            int idx = 1;
            // Console.WriteLine("[아이템 목록]");
            int startY = 3 ;
            int startX = Console.WindowWidth - 35;
            List<Item> itemList = InventoryManager.Instance.inventory;
            foreach (var item in storeItems)
            {
                bool isHaved = itemList.Any(x => x.name == item.name);
                string itemText = "";
                if (isHaved)
                    itemText = $"{item.name.PadRight(10)}|보유 중";  
                else //{item.effect.type} + {item.effect.value} | {item.info} |
                    itemText = $"{item.name.PadRight(10)} {item.price} G";
                Console.SetCursorPosition(startX, startY+idx);
                Console.Write(itemText);
                idx++;
            }
        }
    }
}