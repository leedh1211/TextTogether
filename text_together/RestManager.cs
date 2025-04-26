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
        public int GoRest(Player player, List<Item> items, List<Item> inventory)
        {
            while (true)
            {
                UIManager.Clear(1);
                UIManager.Clear(2);
                UIManager.Clear(3);
                UIManager.DrawAscii(UIAscii.RestArt);
                UIManager.WriteLine(2,"휴식하기");
                UIManager.WriteLine(2,$"500G를 내면 체력과 마나를 회복할 수 있습니다. ( 보유 골드 : {player.gold} )");
                UIManager.WriteLine(2,$" [보유 골드 : {player.gold} ]");
                UIManager.WriteLine(2,$" [현재 체력 : {player.health}/100 ]");
                UIManager.WriteLine(2,$" [현재 마나 : {player.mana}/100 ]");
                UIManager.WriteLine(2,"원하시는 행동을 입력해주세요.");
                
                List<Option> options = new List<Option>
                {
                    new Option { text = "휴식하기", value = 1 },
                    new Option { text = "나가기", value = 0 },
                };
                int input = UIManager.inputController(options);
                if (input < 0 || input > 1)
                {
                    UIManager.WriteLine(2,"다시 입력해주세요");
                    continue;
                }
                if (input == 0)
                {
                    return 0;
                }
                if (input == 1)
                {
                    if (player.gold > 500)
                    {
                        player.health = 100;
                        player.mana = 100;
                        player.gold -= 500;
                        UIManager.WriteLine(2,"휴식을 완료했습니다.\n");
                    }
                    else
                    {
                        UIManager.WriteLine(2,"Gold가 부족합니다.\n");
                    }
                }
            }
        }
        // 휴식 탭 UI 업데이트
    }
}
