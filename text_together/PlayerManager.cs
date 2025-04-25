using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    class PlayerManager
    {
        // 싱글톤
        private static PlayerManager instance;

        public static PlayerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerManager();
                }
                return instance;
            }
        }

        // 이름 입력 
        public string WriteName()
        {
            UIManager.Clear(1);
            UIManager.Clear(2);
            UIManager.Clear(3);
            UIManager.DrawAscii(UIAscii.HomeArt);
            UIManager.WriteLine(2,"스파르타 던전에 오신 여러분 환영합니다.");
            UIManager.WriteLine(2,"");
            UIManager.WriteLine(2,"원하시는 이름을 작성해주세요.");
            UIManager.WriteLine(3,"");
            string s = Console.ReadLine();
            Console.WriteLine();
            UIManager.WriteLine(2,"입력하신 이름은 : " + s + " 입니다.");
            return s;
        }

        // 이름 저장
        public string CheckName()
        {
            while (true)
            {
                string name = WriteName();
                List<Option> options = new List<Option>
                {
                    new Option { text = "확인", value = 1 },
                    new Option { text = "취소", value = 2 },
                };
                int n = UIManager.inputController(options);
                if (n == 1)
                {
                    return name;
                }
                else if (n == 2)
                {
                    Console.WriteLine();
                }
            }
        }

        // 직업 선택 정보 enum
        public enum Job
        {
            전사 = 1, 도적 = 2
        }

        public Job SelectJob()
        {
            UIManager.Clear(1);
            UIManager.Clear(2);
            UIManager.Clear(3);
            UIManager.DrawAscii(UIAscii.HomeArt);
            UIManager.WriteLine(2,"스파르타 던전에 오신 여러분 환영합니다.");
            UIManager.WriteLine(2,"");
            UIManager.WriteLine(2,"원하시는 직업을 선택해주세요.");
            List<Option> options = new List<Option>
            {
                new Option { text = "전사", value = 1 },
                new Option { text = "도적", value = 2 },
            };
            int selectedValue = UIManager.inputController(options);
            return (Job) selectedValue;
            // while (true)
            // {
            //     Console.WriteLine("원하시는 직업을 선택해주세요.");
            //     Console.WriteLine();
            //
            //     Console.WriteLine("1. 전사");
            //     Console.WriteLine("2. 도적");
            //     Console.WriteLine();
            //     Console.WriteLine("원하시는 행동을 입력해주세요");
            //     int n = int.Parse(Console.ReadLine());
            //
            //     if (n == 1 || n == 2)
            //         return (Job)n;
            //     Console.WriteLine("1과 2 중 하나를 입력해주세요.");
            //     Console.WriteLine();
            // }
        }
        // 플레이어 상태탭 관리
        public int PlayerInfo(Player player)
        {
            UIManager.DrawAscii(UIAscii.StatusArt);
            UIManager.WriteLine(2, "캐릭터의 정보가 표시됩니다.\n");
            UIManager.WriteLine(2,$"Lv. {player.level}");
            UIManager.WriteLine(2,$"{player.name} ( {player.job} )");
            UIManager.WriteLine(2,$"공격력 : {player.attack}");
            UIManager.WriteLine(2,$"방어력 : {player.shield}");
            UIManager.WriteLine(2,$"체 력 : {player.health}");
            UIManager.WriteLine(2,$"Gold : {player.gold}G\n");
            
            List<Option> options = new List<Option>
            {
                new Option { text = "나가기", value = 0 },
            };
            int selectedValue = UIManager.inputController(options);
            return selectedValue;
        }
    }
}
