using System;

namespace text_together
{
    public static class PlayerBehind //사람 뒷모습
    {
        // 사람 뒷 모습 아스키아트
        private static string[] artLines = new string[]
        {
            "    -#@@@@@@@#~     ",
            "   -@@@@@@@@@@@~    ",
            "  -@@@@@@@@@@@@@~   ",
            "  #@@@!     ;@@@@~  ",
            " -@@@!       ;@@@@  ",
            " #@@@         ;@@@  ",
            " @@@!          @@@  ",
            " @@@~         -@@@  ",
            " @@@@~        #@@@  ",
            " ;@@@@       -@@@@  ",
            "  ;@@@~     -@@@@!  ",
            "   @@@@~    #@@@!   ",
            "   ;@@@@    *@@=    ",
            " -#@@@@!    #@@@~   ",
            "-@@@@@@     ;@@@@~  ",
            "@@@@@@!      ;@@@@~ ",
            "@@@!          ;@@@@ ",
            "@@!            ;@@@ ",
            "@@              @@@ "
        };

        //불러올때 쓰는곳
        public static void Draw(int startX = 0, int startY = 0)
        {
            for (int i = 0; i < artLines.Length; i++)
            {
                
                Console.SetCursorPosition(startX, startY + i);                
                Console.WriteLine(artLines[i]);
            }
        }
    }
}
