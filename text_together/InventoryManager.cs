using System;
using System.Collections.Generic;
using System.Text;

namespace textRPG
{
    class InventoryManager
    {
        // 싱글톤
        private static InventoryManager instance;

        public List<Item> inventory = new List<Item>();

        public static InventoryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InventoryManager();
                }
                return instance;
            }
        }
        void InventoryInfo(List<Item> inventory)
        {
            Console.WriteLine("[아이템 목록]");
            foreach (var item in inventory)
            {
                if (item.isEquipped)
                    Console.WriteLine($"[E]{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info}");
                else
                    Console.WriteLine($"{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info}");
            }
            Console.WriteLine();
        }
        // 아이템들 장착여부 정보
        void EquippedInfo(List<Item> inventory)
        {
            int idx = 1;
            Console.WriteLine("[아이템 목록]");
            foreach (var item in inventory)
            {
                if (item.isEquipped)
                    Console.WriteLine($"- {idx} [E]{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info}");
                else
                    Console.WriteLine($"- {idx} {item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info}");
                idx++;
            }
        }

        // 인벤토리탭 관리
        public void GoInventory(Player player, List<Item> items, List<Item> inventory)
        {
            // inventory가 null인 경우 초기화
            if (inventory == null)
            {
                inventory = new List<Item>(); // 인벤토리 초기화
                Console.WriteLine("인벤토리가 초기화되었습니다.");
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리 할 수 있습니다.\n");

                InventoryInfo(inventory);

                Console.WriteLine("1.장착 관리 \n2.나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                int input = int.Parse(Console.ReadLine());
                if (input == 1)
                {
                    EquippedManage(player, items, inventory);
                }
                else if (input == 2)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("다시 입력해주세요.");
                }
            }
        }
        // 장착탭 관리
        void EquippedManage(Player player, List<Item> items, List<Item> inventory)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리 할 수 있습니다.\n");
                EquippedInfo(inventory);


                Console.WriteLine("\n0. 나가기 \n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                int input = int.Parse(Console.ReadLine());
                int index;
                if (input == 0)
                {
                    return;
                }
                else if (inventory[input - 1].isHave)
                {
                    if (IsSameEffectEquipped(inventory, inventory[input - 1].effect.type, out index))
                    {
                        inventory[index].isEquipped = false;
                        inventory[input - 1].isEquipped = true;
                        if (inventory[input - 1].effect.type == "방어력")
                        {
                            player.shield -= inventory[index].effect.value;
                            player.shield += inventory[input - 1].effect.value;
                        }
                        else
                        {
                            player.attack -= inventory[index].effect.value;
                            player.attack += inventory[input - 1].effect.value;
                        }
                        Console.WriteLine($"{inventory[index].name}을 장착 해제하고 {inventory[input - 1].name} 장착하였습니다.\n");
                    }
                    else
                    {
                        inventory[input - 1].isEquipped = true;
                        if (inventory[input - 1].effect.type == "방어력")
                        {
                            player.shield += inventory[input - 1].effect.value;
                        }
                        else
                        {
                            player.attack += inventory[input - 1].effect.value;
                        }
                        Console.WriteLine($"{inventory[input - 1].name}을 장착하였습니다.\n");
                    }
                }
                else if (inventory[input - 1].isHave)
                {
                    Console.WriteLine("아이템을 가지고 있지 않습니다. 다시 입력해주세요 \n");
                }
            }

        }
        // 아이템 장착하고 있었는지 확인
        bool IsSameEffectEquipped(List<Item> inventory, string type, out int index)
        {
            foreach (var item in inventory)
            {
                if (item.isEquipped && item.effect.type == type)
                {
                    index = inventory.IndexOf(item);
                    return true; // 이미 같은 종류 장착 중
                }
            }
            index = -1;
            return false;
        }
    }
}
