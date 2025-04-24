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
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요 : \n");
            string s = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("입력하신 이름은 : " + s + " 입니다.\n");
            return s;
        }

        // 이름 저장
        public string CheckName()
        {
            while (true)
            {
                Console.Clear();
                string name = WriteName();
                Console.WriteLine("1. 저장 \n2. 취소 \n");
                Console.WriteLine("원하시는 행동을 입력해주세요");
                int n = int.Parse(Console.ReadLine());

                if (n == 1)
                {
                    Console.Clear();
                    return name;
                }
                else if (n == 2)
                {
                    Console.WriteLine();
                    continue;
                }
                Console.WriteLine("1과 2 중 하나를 입력해주세요.\n");
            }
        }

        // 직업 선택 정보 enum
        public enum Job
        {
            전사 = 1, 도적 = 2
        }

        public Job SelectJob()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            while (true)
            {
                Console.WriteLine("원하시는 직업을 선택해주세요.");
                Console.WriteLine();

                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 도적");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                int n = int.Parse(Console.ReadLine());

                if (n == 1 || n == 2)
                    return (Job)n;
                Console.WriteLine("1과 2 중 하나를 입력해주세요.");
                Console.WriteLine();
            }
        }
        // 플레이어 상태탭 관리
        public int PlayerInfo(Player player)
        {
            
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
