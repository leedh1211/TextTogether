using System;
using System.Text;

namespace text_together
{
    public class View
    {
        static void View1(string[] args)
        {
            // 콘솔크기            
            Console.SetWindowSize(120, 50); // 콘솔창 사이즈
            Console.OutputEncoding = Encoding.UTF8; // 유니코드 적용
            Console.CursorVisible = false;// 입력 숨겨주는거 

            //프레임 만들기
            DrawAsciiFrame();

            Console.ReadLine();
        }

        // 프레임 그리는 그거
        public static void DrawAsciiFrame()
        {
            Console.Clear();

            int width = 100;   // 전체 가로 길이 (콘솔 너비)
            int height = 40;   // 전체 세로 길이 (콘솔 높이)
            int downY = 20;        // 상단/하단을 나누는 줄 위치
            int highX = 50;        // 하단에서 좌/우 나누는 위치

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 상단 맨 윗줄
                    if (y == 0)
                    {
                        if (x == 0) Console.Write("┏");
                        else if (x == width - 1) Console.Write("┓");
                        else Console.Write("━");
                    }

                    // 하단 맨 아랫줄
                    else if (y == height - 1)
                    {
                        if (x == 0) Console.Write("┗");
                        else if (x == width - 1) Console.Write("┛");
                        else if (x == highX) Console.Write("┻");
                        else Console.Write("━");
                    }

                    // 상단/하단 나누는 중간선
                    else if (y == downY)
                    {
                        if (x == 0) Console.Write("┣");
                        else if (x == width - 1) Console.Write("┫");
                        else if (x == highX) Console.Write("┳");
                        else Console.Write("━");
                    }

                    // 하단의 좌/우 나누는 가운데 세로줄 (하단일 때만)
                    else if (y > downY && x == highX)
                    {
                        Console.Write("┃");
                    }

                    // 좌측 / 우측 세로줄
                    else if (x == 0 || x == width - 1)
                    {
                        Console.Write("┃");
                    }

                    // 나머지 빈 공간
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}

