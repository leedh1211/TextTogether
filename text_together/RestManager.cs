using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    

    class RestManager
    {
        // 싱글톤
        private static RestManager instance;

        public static RestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RestManager();
                }
                return instance;
            }
        }
        // 휴식 탭 관리
        public void GoRest(Player player, List<Item> items, List<Item> inventory)
        {
            while (true)
            {
                UdateRestUI(player);
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                int input = int.Parse(Console.ReadLine());

                if (input < 0 || input > 1)
                {
                    Console.WriteLine("다시 입력해주세요");
                    continue;
                }
                if (input == 0)
                {
                    return;
                }
                if (input == 1)
                {
                    if (player.gold > 500)
                    {
                        player.health = 100;
                        player.gold -= 500;
                        Console.WriteLine("휴식을 완료했습니다.\n");
                        UdateRestUI(player);
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.\n");
                    }
                }
            }
        }
        // 휴식 탭 UI 업데이트
        void UdateRestUI(Player player)
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500G를 내면 체력을 회복할 수 있습니다. ( 보유 골드 : {player.gold} )\n");

            Console.WriteLine("1. 휴식하기 \n0. 나가기\n");
        }
    }
}
