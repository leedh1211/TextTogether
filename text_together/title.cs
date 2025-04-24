using System.Text;

namespace text_together;

public class title
{
    public static String titleAsciiArt =
        "\n       _______  _______  __   __  _______  _______  _______  _______  _______  _______  __   __  _______  ______   \n      |       ||       ||  |_|  ||       ||       ||       ||       ||       ||       ||  | |  ||       ||    _ |  \n      |_     _||    ___||       ||_     _||_     _||   _   ||    ___||    ___||_     _||  |_|  ||    ___||   | ||  \n        |   |  |   |___ |       |  |   |    |   |  |  | |  ||   | __ |   |___   |   |  |       ||   |___ |   |_||_ \n        |   |  |    ___| |     |   |   |    |   |  |  |_|  ||   ||  ||    ___|  |   |  |       ||    ___||    __  |\n        |   |  |   |___ |   _   |  |   |    |   |  |       ||   |_| ||   |___   |   |  |   _   ||   |___ |   |  | |\n        |___|  |_______||__| |__|  |___|    |___|  |_______||_______||_______|  |___|  |__| |__||_______||___|  |_|\n";

    public static String start =
        // "\n ___  ____   __    ____  ____ \n/ __)(_  _) /__\\  (  _ \\(_  _)\n\\__ \\  )(  /(__)\\  )   /  )(  \n(___/ (__)(__)(__)(_)\\_) (__) \n";
        " _______  _______  _______  ______    _______ \n|       ||       ||   _   ||    _ |  |       |\n|  _____||_     _||  |_|  ||   | ||  |_     _|\n| |_____   |   |  |       ||   |_||_   |   |  \n|_____  |  |   |  |       ||    __  |  |   |  \n _____| |  |   |  |   _   ||   |  | |  |   |  \n|_______|  |___|  |__| |__||___|  |_|  |___|  \n";


    public static String option =
        // "\n _____  ____  ____  ____  _____  _  _ \n(  _  )(  _ \\(_  _)(_  _)(  _  )( \\( )\n )(_)(  )___/  )(   _)(_  )(_)(  )  ( \n(_____)(__)   (__) (____)(_____)(_)\\_)\n";
        " _______  _______  _______  ___   _______  __    _ \n|       ||       ||       ||   | |       ||  |  | |\n|   _   ||    _  ||_     _||   | |   _   ||   |_| |\n|  | |  ||   |_| |  |   |  |   | |  | |  ||       |\n|  |_|  ||    ___|  |   |  |   | |  |_|  ||  _    |\n|       ||   |      |   |  |   | |       || | |   |\n|_______||___|      |___|  |___| |_______||_|  |__|\n";

    public static String Quit =
        // "\n _____  __  __  ____  ____ \n(  _  )(  )(  )(_  _)(_  _)\n )(_)(  )(__)(  _)(_   )(  \n(___/\\\\(______)(____) (__) \n";
        " _______  __   __  ___   _______ \n|       ||  | |  ||   | |       |\n|   _   ||  | |  ||   | |_     _|\n|  | |  ||  |_|  ||   |   |   |  \n|  |_|  ||       ||   |   |   |  \n|      | |       ||   |   |   |  \n|____||_||_______||___|   |___|  \n";
    public static String SelectArrow =
        "\n\u2588\u2588\u2557  \n\u255a\u2588\u2588\u2557 \n \u255a\u2588\u2588\u2557\n \u2588\u2588\u2554\u255d\n\u2588\u2588\u2554\u255d \n\u255a\u2550\u255d  \n     \n";


    public static int SelectTitleOption()
    {
        Console.Clear();
        Console.Title = "Text Together";
        Console.SetBufferSize(120, 50);
        Console.SetWindowSize(120, 50);
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;

        Console.Write(titleAsciiArt);

        int originPadding = 40;
        int arrowPadding = originPadding - 8;
        int startYIndex = 17;

        int[] optionLineYs = new int[3]; // start, option, quit 줄의 Y좌표 저장

        string[] menus = { start, option, Quit };
        for (int i = 0; i < menus.Length; i++)
        {
            int padding = originPadding;
            char targetText;
            optionLineYs[i] = startYIndex;

            for (int j = 0; j < menus[i].Length; j++)
            {
                targetText = menus[i][j];

                if (targetText == '\n')
                {
                    startYIndex++;
                    padding = originPadding;
                }
                else
                {
                    Console.SetCursorPosition(padding, startYIndex);
                    Console.Write(targetText);
                    padding++;
                }
            }

            startYIndex += 1; // 항목 간 여백
        }

        int selectedIndex = 0;

        void DrawArrow()
        {
            string[] arrowLines = SelectArrow.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arrowLines.Length; i++)
            {
                Console.SetCursorPosition(arrowPadding, optionLineYs[selectedIndex] + i);
                Console.Write(arrowLines[i].PadRight(8)); // 이전 화살표 지우기용
            }
        }

        void EraseArrow()
        {
            string[] arrowLines = SelectArrow.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arrowLines.Length; i++)
            {
                Console.SetCursorPosition(arrowPadding, optionLineYs[selectedIndex] + i);
                Console.Write("        "); // 화살표 영역 삭제
            }
        }

        DrawArrow(); // 첫 화살표 출력

        while (true)
        {
            var key = Console.ReadKey(intercept: true).Key;

            if (key == ConsoleKey.Enter)
            {
                return selectedIndex;
            }

            int delta = key switch
            {
                ConsoleKey.W or ConsoleKey.UpArrow => -1,
                ConsoleKey.S or ConsoleKey.DownArrow => 1,
                _ => 0
            };

            if (delta != 0)
            {
                EraseArrow(); // 이전 화살표 삭제
                selectedIndex = (selectedIndex + delta + 3) % 3; // 0~2 범위 유지
                DrawArrow(); // 새 화살표 그리기
            }
        }
    }
}


   
    
    
   
  
  
  
