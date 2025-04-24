using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_together
{
    public static class BackPackArt //가방 아스키아트
    {
        private static string[] artLines = new string[]
        {
            "    _________    ",
            "   /         \\  ",
            "  |  _______  |  ",
            "  | |       | |  ",
            "  | |_______| |  ",
            "  |  [_____]  |  ",
            "  \\___________/  "
        };

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
