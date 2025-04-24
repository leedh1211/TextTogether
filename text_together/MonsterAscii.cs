using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_together;

public class MonsterAscii
{
    public static class Cat //고양이
    {
        public static readonly string[] Art = new string[]
        {
            "  /\\_/\\ ",
            " (  -.-  ) ",
            "/       \\ ",
            "(  ___   )"
        };

        public static void Draw()
        {
            foreach (var line in Art)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
    public static class Dog //멍무이
    {
        public static readonly string[] Art = new string[]
        {
            "  /‾‾‾\\  ",
            " ( •ᴥ• ) ",
            " /|___|\\ ",
            "(_/   \\_)"
        };

        public static void Draw()
        {
            foreach (var line in Art)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
    public static class Bat //박쥐
    {
        public static readonly string[] Art = new string[]
        {
            " /\\_/\\  ",
            "(｡•ᴥ•｡)",
            " /   \\  ",
            " ^^ ^^ "
        };

        public static void Draw()
        {
            foreach (var line in Art)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
    public static class Duck //오리
    {
        public static readonly string[] Art = new string[]
        {
            "  __    ",
            "<(o )___",
            " (   ._> ",
            "  `---’ "
        };

        public static void Draw()
        {
            foreach (var line in Art)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
    public static class Snowman //신난눈사람
    {
        public static readonly string[] Art = new string[]
        {
            "  (•ᴗ•)  ",
            " <(   )> ",
            "  /   \\  ",
            "  \"   \"  "
        };

        public static void Draw()
        {
            foreach (var line in Art)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
}


