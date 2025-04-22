using System;
using System.Text;

namespace View
{
    internal class view
    {
        static void Main(string[] args)
        {
            // 콘솔크기            
            Console.SetWindowSize(120, 50); // 콘솔창 크기 
            Console.CursorVisible = false;

            //프레임 만들기
            DrawAsciiFrame();

           
            Console.ReadLine();
        }

        // 프레임 그리는 그거
        static void DrawAsciiFrame()
        {
            Console.Clear();

            int width = 100;   // 전체 가로 길이 (콘솔 너비)
            int height = 40;   // 전체 세로 길이 (콘솔 높이)
            int Y = 25;     // 상단/하단을 나누는 줄 위치 숫자를 줄이면 커지고 높이면 작아짐
            int X = 50;     // 하단에서 좌/우 나누는 위치

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 상단 맨 윗줄 or 하단 맨 아랫줄
                    if (y == 0 || y == height - 1)
                    {
                        Console.Write("//");
                        x++; // 칸 건너뛰기
                    }
                    // 상단/하단 나누는 가로줄 (중간선)
                    else if (y == Y)
                    {
                        Console.Write("//");
                        x++;
                    }
                    // 하단에서 좌/우 나누는 세로줄
                    else if (y > Y && (x == X - 1 || x == X))
                    {
                        Console.Write("//");
                        x++;
                    }
                    // 좌측 / 우측 테두리
                    else if (x == 0 || x == width - 2)
                    {
                        Console.Write("//");
                        x++;
                    }
                    // 나머지는 빈 공간
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













/////////////////////////////////////////
//                                      /
//                                      /
// 상단 영역 /
//                                      /
////////////////////////////////////////
//         //               //          /
// 왼쪽 하단 //   오른쪽 하단           /
//         //               //          /
////////////////////////////////////////