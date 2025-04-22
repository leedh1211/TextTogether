using System.Net.Mime;
using text_together;
using textRPG;

namespace textRPG;

public class UIManager
{

    static int prevWidth = Console.WindowWidth;
    static int prevHeight = Console.WindowHeight;

    static int mainSpace_x = 118;
    static int mainSpace_y = 28;
    static int mainStartPos_x = 1;
    static int mainStartPos_y = 1;

    static int contentSpace_x = 58;
    static int contentSpace_y = 19;
    static int contentStartPos_x = 1;
    static int contentStartPos_y = 30;

    static int optionSpace_x = 58;
    static int optionSpace_y = 19;
    static int optionStartPos_x = 61;
    static int optionStartPos_y = 30;

    static int typingDelay = 60;

    //음원 경로
    static string filePath = "../../../../Resources/voice_sans.wav";


    static int[] newLineCnt = new int[] { 0, 0, 0 };


    static List<int[]> cursors = new List<int[]>()
    {
        new int[] { mainStartPos_x, mainStartPos_y },        // 1번 커서
        new int[] { contentStartPos_x, contentStartPos_y },  // 2번 커서
        new int[] { optionStartPos_x, optionStartPos_y }     // 3번 커서
    };

    //UI 매개변수를 어떻게 받을까
    //이넘으로 변환해서받기
    //스트링으로 받기
    //정수로 받기
    static public void Clear(int index)
    {
        index -= 1;

        //줄넘김 초기화
        newLineCnt[index] = 0;


        //상단 UI
        if (index + 1 == 1)
        {

            //커서 위치 초기화
            cursors[index][0] = mainStartPos_x;
            cursors[index][1] = mainStartPos_y;

            // 클리어 x,y값은 변경될 가능성 높음
            for (int y = 0; y < mainSpace_y; y++)
            {
                Console.SetCursorPosition(1, 1 + y);

                for (int x = 0; x < mainSpace_x; x++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        //하단 좌측 UI
        else if (index + 1 == 2)
        {
            //커서 위치 초기화
            cursors[index][0] = contentStartPos_x;
            cursors[index][1] = contentStartPos_y;

            //클리어 x,y값은 변경될 가능성 높음
            for (int y = 0; y < contentSpace_y; y++)
            {
                Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + y);

                for (int x = 0; x < contentSpace_x; x++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        //하단 우측 UI
        else if (index + 1 == 3)
        {
            //커서 위치 초기화
            cursors[index][0] = optionStartPos_x;
            cursors[index][1] = optionStartPos_y;

            //클리어 x,y값은 변경될 가능성 높음
            for (int y = 0; y < optionSpace_y; y++)
            {
                Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + y);

                for (int x = 0; x < optionSpace_x; x++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }


    static public void Write(int index, string text)
    {
        index -= 1;
        //해당 UI의 마지막 커서위치 설정
        Console.SetCursorPosition(cursors[index][0], cursors[index][1]);

        //상단 UI
        if (index + 1 == 1)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i % mainSpace_x == 0 && i != 0)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);
                }
            }
        }

        //하단 좌측 UI
        else if (index + 1 == 2)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > contentSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
                }
            }
        }

        //하단 우측 UI
        else if (index + 1 == 3)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > optionSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);
                }
            }
        }


        //커서위치 반환 (마지막 위치 기억용)
        cursors[index][0] = Console.CursorLeft; //x축
        cursors[index][1] = Console.CursorTop;  //y축

    }

    static public void WriteLine(int index, string text)
    {
        index -= 1;
        //해당 UI의 마지막 커서위치 설정
        Console.SetCursorPosition(cursors[index][0], cursors[index][1]);

        //상단 UI
        if (index + 1 == 1)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > mainSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);
                }
            }

            newLineCnt[index]++;
            Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);
        }

        //하단 좌측 UI
        else if (index + 1 == 2)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > contentSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
                }
            }

            newLineCnt[index]++;
            Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
        }

        //하단 우측 UI
        else if (index + 1 == 3)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > optionSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);
                }
            }

            newLineCnt[index]++;
            Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);
        }


        //커서위치 반환 (마지막 위치 기억용)
        cursors[index][0] = Console.CursorLeft; //x축
        cursors[index][1] = Console.CursorTop;  //y축

    }


    static public void Typing(int index, string text)
    {
        index -= 1;
        //해당 UI의 마지막 커서위치 설정
        Console.SetCursorPosition(cursors[index][0], cursors[index][1]);

        //상단 UI
        if (index + 1 == 1)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i % mainSpace_x == 0 && i != 0)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);

                    //해당 문자 표현시 소리재생 X
                    if (text[i] == ' ' || text[i] == '.')
                    {

                    }

                    else
                    {
                        if (filePath != null)
                        {   //음악관련으로 대기시에 메인 쓰레드와 분리해서 메인 쓰레드가 정지되지 않게함
                            Thread thread = new Thread(SoundManager.sound);
                            thread.Start();
                        }
                    }
                }

                Thread.Sleep(typingDelay);
            }
        }

        //하단 좌측 UI
        else if (index + 1 == 2)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > contentSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);

                    //해당 문자 표현시 소리재생 X
                    if (text[i] == ' ' || text[i] == '.')
                    {

                    }

                    else
                    {
                        if (filePath != null)
                        {   //음악관련으로 대기시에 메인 쓰레드와 분리해서 메인 쓰레드가 정지되지 않게함
                            Thread thread = new Thread(SoundManager.sound);
                            thread.Start();
                        }
                    }
                }
                Thread.Sleep(typingDelay);
            }
        }

        //하단 우측 UI
        else if (index + 1 == 3)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > optionSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);

                    //해당 문자 표현시 소리재생 X
                    if (text[i] == ' ' || text[i] == '.')
                    {

                    }

                    else
                    {
                        if (filePath != null)
                        {   //음악관련으로 대기시에 메인 쓰레드와 분리해서 메인 쓰레드가 정지되지 않게함
                            Thread thread = new Thread(SoundManager.sound);
                            thread.Start();
                        }
                    }
                }
                Thread.Sleep(typingDelay);
            }
        }


        //커서위치 반환 (마지막 위치 기억용)
        cursors[index][0] = Console.CursorLeft; //x축
        cursors[index][1] = Console.CursorTop;  //y축

    }

    static public void TypingLine(int index, string text)
    {
        index -= 1;
        //해당 UI의 마지막 커서위치 설정
        Console.SetCursorPosition(cursors[index][0], cursors[index][1]);

        //상단 UI
        if (index + 1 == 1)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > mainSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);

                    //해당 문자 표현시 소리재생 X
                    if (text[i] == ' ' || text[i] == '.')
                    {

                    }

                    else
                    {
                        if (filePath != null)
                        {   //음악관련으로 대기시에 메인 쓰레드와 분리해서 메인 쓰레드가 정지되지 않게함
                            Thread thread = new Thread(SoundManager.sound);
                            thread.Start();
                        }
                    }
                }
                Thread.Sleep(typingDelay);
            }

            newLineCnt[index]++;
            Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);
        }

        //하단 좌측 UI
        else if (index + 1 == 2)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > contentSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);

                    //해당 문자 표현시 소리재생 X
                    if (text[i] == ' ' || text[i] == '.')
                    {

                    }

                    else
                    {
                        if (filePath != null)
                        {   //음악관련으로 대기시에 메인 쓰레드와 분리해서 메인 쓰레드가 정지되지 않게함
                            Thread thread = new Thread(SoundManager.sound);
                            thread.Start();
                        }
                    }
                }
                Thread.Sleep(typingDelay);
            }

            newLineCnt[index]++;
            Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
        }

        //하단 우측 UI
        else if (index + 1 == 3)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                if (i > optionSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);

                    //해당 문자 표현시 소리재생 X
                    if (text[i] == ' ' || text[i] == '.')
                    {

                    }

                    else
                    {
                        if (filePath != null)
                        {   //음악관련으로 대기시에 메인 쓰레드와 분리해서 메인 쓰레드가 정지되지 않게함
                            Thread thread = new Thread(SoundManager.sound);
                            thread.Start();
                        }
                    }
                }
                Thread.Sleep(typingDelay);
            }

            newLineCnt[index]++;
            Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);
        }


        //커서위치 반환 (마지막 위치 기억용)
        cursors[index][0] = Console.CursorLeft; //x축
        cursors[index][1] = Console.CursorTop;  //y축

    }

    //타이핑 효과
    static public void TypingText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            Console.Write(text[i]);

            //해당 문자 표현시 소리재생 X
            if (text[i] == ' ' || text[i] == '.')
            {

            }

            else
            {
                if (filePath != null)
                {   //음악관련으로 대기시에 메인 쓰레드와 분리해서 메인 쓰레드가 정지되지 않게함
                    Thread thread = new Thread(SoundManager.sound);
                    thread.Start();
                }
            }

            //타이핑 후 대기시간
            Thread.Sleep(60);
        }

    }


    
    static public void CheckWindow()
    {
        // 변경 없으면 아무것도 안 함
        if (prevWidth == Console.WindowWidth && prevHeight == Console.WindowHeight)
        {
            return;
        }

        else
        {
            // 만약 기존크기에셔 변경 될 경우 아래 수행
            mainSpace_x = Console.WindowWidth - 2;
            mainSpace_y = (int)Math.Round(Console.WindowHeight * 0.6);
            mainStartPos_x = 1;
            mainStartPos_y = 1;

            contentSpace_x = (int)Math.Round(Console.WindowWidth * 0.5) - 2;
            contentSpace_y = (int)Math.Round(Console.WindowHeight * 0.4) - 1;
            contentStartPos_x = 1;
            contentStartPos_y = (int)Math.Round(Console.WindowHeight * 0.6);

            optionSpace_x = (int)Math.Round(Console.WindowWidth * 0.5) - 2;
            optionSpace_y = (int)Math.Round(Console.WindowHeight * 0.4) - 1;
            optionStartPos_x = (int)Math.Round(Console.WindowWidth * 0.5) + 1;
            optionStartPos_y = (int)Math.Round(Console.WindowHeight * 0.6);


            ////////////////
            //View클래스 변경
            ////////////////
            View.width = Console.WindowWidth;
            View.height = Console.WindowHeight;
            View.downY = (int)Math.Round(Console.WindowHeight * 0.4);
            View.highX = (int)Math.Round(Console.WindowWidth * 0.5);

            //이전 크기 저장
            prevWidth = Console.WindowWidth;
            prevHeight = Console.WindowHeight;

            //UI 뿌리기
            View.DrawAsciiFrame();
        }

        //변경된 크기를 기준으로 UI 뿌려주기
    }
    static public void test()
    {
        Console.Title = " ";
        Console.CursorVisible = false;
        Console.SetBufferSize(120, 50);
        Console.SetWindowSize(120, 50);

        for (int i = 0; i < 120 * 50; i++)
        {
            Console.Write("A");
        }



        Console.ReadKey();



    }

    
    static public int inputController(List<Option> option)
    {
        int index = option.Count-1;
        int count = option.Count;
        MakeOptionString(option, index);


        while (true)
        {
            var key = Console.ReadKey(intercept: true).Key;
            if (key == ConsoleKey.Enter)
                return option[index].value;

            int delta = GetDelta(key);
            if (delta == 0)
                continue;  

            // 순환(ring) 방식으로 인덱스 보정
            Clear(3);
            index = (index + delta + count) % count;
            MakeOptionString(option, index);
        }
    }
    
    static void MakeOptionString(List<Option> option, int index)
    {
        string text = "";
        for (int i = 0; i < option.Count; i++)
        {
            if (i == index)
            {
                text = "\u25b7"+option[i].text;
            }
            else
            {
                text = option[i].text;
            }
            UIManager.WriteLine(3,text);
        }
    }
    
    static int GetDelta(ConsoleKey key) => key switch
    {
        ConsoleKey.W or ConsoleKey.UpArrow   => -1,
        ConsoleKey.S or ConsoleKey.DownArrow => +1,
        _                                    => 0
    };
}