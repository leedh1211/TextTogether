using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace text_together
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
        void InventoryInfo()
        {
            UIManager.Clear(2);
            UIManager.Clear(3);

            UIManager.WriteLine(2, "[아이템 목록]");

            foreach (var item in inventory)
            {
                if (item.isEquipped)
                    UIManager.WriteLine(2, $"[E]{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.quantity}");
                else
                    UIManager.WriteLine(2, $"{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.quantity}");
            }
        }

        List<Option> DungeonInventoryInfo()
        {
            UIManager.WriteLine(2, "[아이템 목록]");

            List<Option> options = new List<Option>
            {
                new Option { text = "나가기", value = 0}
            };

            int idx = 1;

            foreach (var item in inventory)
            {
                if (item.effect.type == "포션")
                    UIManager.WriteLine(2, $"{item.name.PadRight(10)}| {item.effect.value} | {item.info} | {item.quantity}");
                    options.Add(new Option { text = item.name, value = idx });
                    idx++;
            }
            return options; 
        }

        // 아이템들 장착여부 정보
        List<Option> EquippedInfo()
        {
            List<Option> options = new List<Option>
            {
                new Option { text = "나가기", value = 0 }
            };

            int idx = 1;
            UIManager.WriteLine(2, "[아이템 목록]");
            foreach (var item in inventory)
            {
                string equipText;

                if (item.isEquipped)
                    equipText = $"- {idx} [E]{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.quantity}";
                else
                    equipText = $"- {idx} {item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.quantity}";

                UIManager.WriteLine(2, equipText);

                options.Add(new Option { text = item.name, value = idx });
                idx++;
            }
            return options;
        }

        // 던젼 인벤토리 관리
        public void GoDungeonInventory(Player player)
        {
            // inventory가 null인 경우 초기화
            if (inventory == null)
            {
                inventory = new List<Item>(); // 인벤토리 초기화
            }
            
            while (true)
            {
                UIManager.Clear(2);
                UIManager.Clear(3);

                UIManager.WriteLine(2, "인벤토리");
                UIManager.WriteLine(2, "포션 아이템을 먹어서 스텟을 올릴 수 있다!");

                List<Option> options = DungeonInventoryInfo();

                UIManager.WriteLine(2, "원하시는 행동을 선택해주세요.");

                int input = UIManager.inputController(options);
               
                if (input > 0 && input <= inventory.Count - 1)
                {
                    ManagePotion(player,input);
                }
            }
        }

        // 포션 관리
        public void ManagePotion(Player player,int input)
        {
            var item = inventory[input - 1];

            if (inventory[input - 1].effect.type == "포션")
            {
                UIManager.WriteLine(2, $"{item.name}을 마셨다.");
                UIManager.WriteLine(2, $"{item.info} 효능이 발동하였다.");

                if (item.name.Contains("생명력"))
                    player.health += item.effect.value;

                else if (item.name.Contains("마나"))
                    player.mana += item.effect.value;

                item.quantity--;
                if (item.quantity <= 0)
                {
                    inventory.RemoveAt(input - 1);
                    UIManager.WriteLine(2, "포션을 모두 사용하여 인벤토리에서 제거되었습니다.");
                }
            }
            else
            {
                UIManager.WriteLine(2, "해당 아이템은 포션이 아닙니다.");
            }
        }

        // 인벤토리탭 관리
        public int GoInventory(Player player)
        {
            // inventory가 null인 경우 초기화
            if (inventory == null)
            {
                inventory = new List<Item>(); // 인벤토리 초기화
            }

            while (true)
            {
                UIManager.Clear(2);
                UIManager.Clear(3);

                UIManager.WriteLine(2,"인벤토리");
                UIManager.WriteLine(2,"보유 중인 아이템을 관리 할 수 있습니다.\n");

                InventoryInfo();

                List<Option> options = new List<Option>
                {
                    new Option { text = "장착 관리", value = 1 },
                    new Option { text = "나가기", value = 0 }
                };

                int selectedValue = UIManager.inputController(options);

                switch (selectedValue)
                {
                    case 1:
                        EquippedManage(player);
                        break;
                    case 0:
                        return 0;
                }
            }
        }
        // 장착탭 관리
        void EquippedManage(Player player)
        {
            while (true)
            {
                UIManager.Clear(2);
                UIManager.Clear(3);

                UIManager.WriteLine(2, "인벤토리 - 장착 관리");
                UIManager.WriteLine(2, "보유 중인 아이템을 관리 할 수 있습니다.\n");

                List<Option> options = EquippedInfo();

                UIManager.WriteLine(2, "원하시는 행동을 입력해주세요.");
                int input = UIManager.inputController(options);

                if (input == 0)
                {
                    return;
                }
                else if (input > 0 && input <= inventory.Count)
                {
                    var item = inventory[input - 1];
                    int index;

                    if (item.effect.type == "포션")
                    {
                        UIManager.WriteLine(2, "포션은 장착할 수 없습니다.");
                    }

                    // 장착중인 아이템일 경우 장착해제
                    else if (item.isEquipped)
                    {
                        UIManager.WriteLine(2, $"{inventory[input - 1].name}을 장착 해제했습니다.");
                        item.isEquipped = false;

                        if (item.effect.type == "방어력")
                            player.shield -= item.effect.value;
                        else
                            player.attack -= item.effect.value;

                        continue;
                    }

                    else if (IsSameEffectEquipped(item.effect.type, out index))
                    {
                        inventory[index].isEquipped = false;
                        item.isEquipped = true;
                        if (item.effect.type == "방어력")
                        {
                            player.shield -= inventory[index].effect.value;
                            player.shield += item.effect.value;
                        }
                        else
                        {
                            player.attack -= inventory[index].effect.value;
                            player.attack += item.effect.value;
                        }
                        UIManager.WriteLine(2, $"{inventory[index].name}을 장착 해제하고 {item.name} 장착하였습니다.\n");
                    }
                    else
                    {
                        item.isEquipped = true;
                        if (item.effect.type == "방어력")
                        {
                            player.shield += item.effect.value;
                        }
                        else
                        {
                            player.attack += item.effect.value;
                        }
                        UIManager.WriteLine(2, $"{item.name}을 장착하였습니다.\n");
                    }
                }
            }

        }
        // 아이템 장착하고 있었는지 확인
        bool IsSameEffectEquipped(string type, out int index)
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
