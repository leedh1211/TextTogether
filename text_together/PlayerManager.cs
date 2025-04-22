using System;
using System.Collections.Generic;
using System.Text;

namespace textRPG
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
        public void PlayerInfo(Player player)
        {
            Console.Clear();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv. {player.level}");
            Console.WriteLine($"{player.name} ( {player.job} )");
            Console.WriteLine($"공격력 : {player.attack}");
            Console.WriteLine($"방어력 : {player.shield}");
            Console.WriteLine($"체 력 : {player.health}");
            Console.WriteLine($"Gold : {player.gold}G\n");

            Console.WriteLine($"0. 나가기\n");
            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                int n = int.Parse(Console.ReadLine());
                if (n == 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("0을 입력하셔야 나가실 수 있습니다.");
                }
            }
        }
    }
}
