using System;
using System.Text;

namespace text_together
{
    public class View
    {

        static public int width = 119;   // 전체 가로 길이 (콘솔 너비)
        static public int height = 50;   // 전체 세로 길이 (콘솔 높이)
        static public int downY;        // 상단/하단을 나누는 줄 위치
        static public int highX;        // 하단에서 좌/우 나누는 위치


        static public void View1()
        {
            // 콘솔크기
            Console.SetBufferSize(121, 51);
            Console.SetWindowSize(121, 51); // 콘솔창 사이즈
            Console.OutputEncoding = Encoding.UTF8; // 유니코드 적용
            Console.CursorVisible = false;// 입력 숨겨주는거 

            //프레임 만들기
            //DrawAsciiFrame();

            //Console.ReadLine();
        }


        public static void DrawUIFast()
        {
            // 콘솔 크기 갱신
            width = Console.WindowWidth-1;
            height = Console.WindowHeight-1;
            downY = (int)Math.Round(height * 0.65);
            highX = (int)Math.Round(width * 0.6);

            StringBuilder buffer = new StringBuilder();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char ch = ' ';

                    if (y == 0) // 맨 윗줄
                        ch = (x == 0) ? '┏' : (x == width - 1) ? '┓' : '━';
                    else if (y == height - 1) // 맨 아랫줄
                        ch = (x == 0) ? '┗' : (x == width - 1) ? '┛' : (x == highX) ? '┻' : '━';
                    else if (y == downY) // 중간 구분선
                        ch = (x == 0) ? '┣' : (x == width - 1) ? '┫' : (x == highX) ? '┳' : '━';
                    else if (x == 0 || x == width - 1) // 좌/우 세로줄
                        ch = '┃';
                    else if (y > downY && x == highX) // 하단 구분 세로줄
                        ch = '┃';

                    buffer.Append(ch);
                }

                // 마지막 줄이 아니면 줄바꿈 추가
                if (y < height - 1)
                    buffer.AppendLine();
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(buffer.ToString());
        }

        // 프레임 그리는 그거
        // public static void DrawAsciiFrame()
        // {
        //     Console.Clear();
        //
        //     
        //
        //     for (int y = 0; y < height; y++)
        //     {
        //         for (int x = 0; x < width; x++)
        //         {
        //             // 상단 맨 윗줄
        //             if (y == 0)
        //             {
        //                 if (x == 0) Console.Write("┏");
        //                 else if (x == width - 1) Console.Write("┓");
        //                 else Console.Write("━");
        //             }
        //
        //             // 하단 맨 아랫줄
        //             else if (y == height - 1)
        //             {
        //                 if (x == 0) Console.Write("┗");
        //                 else if (x == width - 1) Console.Write("┛");
        //                 else if (x == highX) Console.Write("┻");
        //                 else Console.Write("━");
        //             }
        //
        //             // 상단/하단 나누는 중간선
        //             else if (y == downY)
        //             {
        //                 if (x == 0) Console.Write("┣");
        //                 else if (x == width - 1) Console.Write("┫");
        //                 else if (x == highX) Console.Write("┳");
        //                 else Console.Write("━");
        //             }
        //
        //             // 하단의 좌/우 나누는 가운데 세로줄 (하단일 때만)
        //             else if (y > downY && x == highX)
        //             {
        //                 Console.Write("┃");
        //             }
        //
        //             // 좌측 / 우측 세로줄
        //             else if (x == 0 || x == width - 1)
        //             {
        //                 Console.Write("┃");
        //             }
        //
        //             // 나머지 빈 공간
        //             else
        //             {
        //                 Console.Write(" ");
        //             }
        //         }
        //
        //         Console.WriteLine();
        //     }
        // }
    }
}

