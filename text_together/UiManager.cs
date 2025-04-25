using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;


namespace text_together;

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

    static int optionSpace_x = 57;
    static int optionSpace_y = 19;
    static int optionStartPos_x = 61;
    static int optionStartPos_y = 30;

    static int typingDelay = 60;

    static List<Option> currentOptions = new List<Option>();
    static string currentArt = "";
    static string currentUIList = "";
    static bool isResolutionChanged = false;
    public static bool isShopUIList = false;

    //음원 경로
    static string filePath = "../../../../Resources/voice_sans.wav";


    static int[] newLineCnt = new int[] { 0, 0, 0 };


    static List<int[]> cursors = new List<int[]>()
    {
        new int[] { mainStartPos_x, mainStartPos_y },        // 1번 커서
        new int[] { contentStartPos_x, contentStartPos_y },  // 2번 커서
        new int[] { optionStartPos_x, optionStartPos_y }     // 3번 커서
    };


    //텍스트를 담을 버퍼 클래스
    static public class Text
    {
        //아마 한 줄마다 문자열을 만든 후 리스트에 넣어주지않을까
        //그 후 줄마다 출력

        static public List<string> mainText { get; set; } = new List<string>();
        static public List<string> contentText { get; set; } = new List<string>();
        static public List<string> optionText { get; set; } = new List<string>();

        static public List<string> mainTextTemp { get; set; }
        static public List<string> contentTextTemp { get; set; }
        static public List<string> optionTextTemp { get; set; }


    }


    //버퍼에 담을 문자열을 만들 공간
    static StringBuilder textTemp = new StringBuilder();





    static public void SetCursor()
    {
        cursors[0][0] = mainStartPos_x;
        cursors[0][1] = mainStartPos_y;

        cursors[1][0] = contentStartPos_x;
        cursors[1][1] = contentStartPos_y;

        cursors[2][0] = optionStartPos_x;
        cursors[2][1] = optionStartPos_y;
    }

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
            Text.mainText.Clear();
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
            Text.contentText.Clear();
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
            Text.optionText.Clear();
        }
    }

    static public void Write1(int index, string text)
    {
        index -= 1;

        //커서 설정
        Console.SetCursorPosition(cursors[index][0], cursors[index][1]);


        if (index + 1 == 1)
        {

        }

        else if (index + 1 == 2)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);

                if ((textTemp.Length) % mainSpace_x == 0 && i != 0)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);
                    Text.mainText.Add(textTemp.ToString());
                    textTemp.Clear(); // 버퍼를 클리어
                }
            }
        }
    }

    static public void Write(int index, string text)
    {
        index -= 1;
        // 해당 UI의 마지막 커서위치 설정
        Console.SetCursorPosition(cursors[index][0], cursors[index][1]);

        // 상단 UI (index + 1 == 1)
        if (index + 1 == 1)
        {
            if (Text.mainText.Count > 0)
            {
                // 최근 문자열 가져오기
                textTemp.Append(Text.mainText[Text.mainText.Count - 1].ToString());

                // 기존 버퍼 내용 제거
                Text.mainText.RemoveAt(Text.mainText.Count - 1);
            }

            for (int i = 0; i < text.Length; i++)
            {
                textTemp.Append(text[i]);
                Console.Write(text[i]);

                // 현재 커서 위치 확인
                int currentCursorLeft = Console.CursorLeft;

                // 커서가 UI 범위를 벗어나면 줄 바꿈
                if (currentCursorLeft >= mainStartPos_x + mainSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);
                    Text.mainText.Add(textTemp.ToString());
                    textTemp.Clear(); // 버퍼를 클리어
                }
            }

            // 마지막에 남은 텍스트를 추가
            if (textTemp.Length > 0)
            {
                Text.mainText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어
            }
        }

        // 하단 좌측 UI (index + 1 == 2)
        else if (index + 1 == 2)
        {
            if (Text.contentText.Count > 0)
            {
                // 최근 문자열 가져오기
                textTemp.Append(Text.contentText[Text.contentText.Count - 1].ToString());

                // 기존 버퍼 내용 제거
                Text.contentText.RemoveAt(Text.contentText.Count - 1);
            }

            for (int i = 0; i < text.Length; i++)
            {
                textTemp.Append(text[i]);
                Console.Write(text[i]);

                // 현재 커서 위치 확인
                int currentCursorLeft = Console.CursorLeft;

                // 커서가 UI 범위를 벗어나면 줄 바꿈
                if (currentCursorLeft >= contentStartPos_x + contentSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
                    Text.contentText.Add(textTemp.ToString());
                    textTemp.Clear(); // 버퍼를 클리어
                }
            }

            // 마지막에 남은 텍스트를 추가
            if (textTemp.Length > 0)
            {
                Text.contentText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어
            }
        }

        // 하단 우측 UI (index + 1 == 3)
        else if (index + 1 == 3)
        {
            if (Text.optionText.Count > 0)
            {
                // 최근 문자열 가져오기
                textTemp.Append(Text.optionText[Text.optionText.Count - 1].ToString());

                // 기존 버퍼 내용 제거
                Text.optionText.RemoveAt(Text.optionText.Count - 1);
            }

            for (int i = 0; i < text.Length; i++)
            {
                textTemp.Append(text[i]);
                Console.Write(text[i]);

                // 현재 커서 위치 확인
                int currentCursorLeft = Console.CursorLeft;

                // 커서가 UI3의 범위를 벗어나면 줄 바꿈 (UI3는 optionStartPos_x부터 시작)
                if (currentCursorLeft >= optionStartPos_x + optionSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);
                    Text.optionText.Add(textTemp.ToString());
                    textTemp.Clear(); // 버퍼를 클리어
                }
            }

            // 마지막에 남은 텍스트를 추가
            if (textTemp.Length > 0)
            {
                Text.optionText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어
            }
        }

        // 커서위치 반환 (마지막 위치 기억용)
        cursors[index][0] = Console.CursorLeft; // x축
        cursors[index][1] = Console.CursorTop;  // y축
    }

    static public void WriteLine(int index, string text)
    {
        index -= 1;
        // 해당 UI의 마지막 커서위치 설정
        Console.SetCursorPosition(cursors[index][0], cursors[index][1]);

        // 상단 UI (index + 1 == 1)
        if (index + 1 == 1)
        {
            if (Text.mainText.Count > 0)
            {
                // 최근 문자열 가져오기
                textTemp.Append(Text.mainText[Text.mainText.Count - 1].ToString());
                Text.mainText.RemoveAt(Text.mainText.Count - 1);
            }

            //글자를 출력함
            for (int i = 0; i < text.Length; i++)
            {
                textTemp.Append(text[i]);
                Console.Write(text[i]);

                // 현재 커서 위치 확인
                int currentCursorLeft = Console.CursorLeft;

                // 커서가 UI 범위를 벗어나면 줄 바꿈
                if (currentCursorLeft >= mainSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);
                    Text.mainText.Add(textTemp.ToString());
                    textTemp.Clear(); // 버퍼를 클리어
                }

                //안넘어가면 아무것도 안하긴해
                //그러면 textTemp => 버퍼인데 남아있곘지?? 넣기 전이니까
            }


            if (textTemp.Length > 0)
            {
                newLineCnt[index]++;
                Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);
                textTemp.Append("\0");
                Text.mainText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어
            }

            else //이미 줄바꿈을 했는데 글자가 없어서 첫 시작점인경우 
            {
                //해당 내용 임시임 특정조건 만족시 문제 가능성 있음
                newLineCnt[index]++;
                Console.SetCursorPosition(mainStartPos_x, mainStartPos_y + newLineCnt[index]);
                textTemp.Append("\0");
                Text.mainText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어

                cursors[index][0] = Console.CursorLeft;
                cursors[index][1] = Console.CursorTop;
            }


            cursors[index][0] = Console.CursorLeft;
            cursors[index][1] = Console.CursorTop;
        }

        // 하단 좌측 UI (index + 1 == 2)
        else if (index + 1 == 2)
        {
            if (Text.contentText.Count > 0)
            {
                // 최근 문자열 가져오기
                textTemp.Append(Text.contentText[Text.contentText.Count - 1].ToString());
                Text.contentText.RemoveAt(Text.contentText.Count - 1);
            }

            //글자를 출력함
            for (int i = 0; i < text.Length; i++)
            {
                textTemp.Append(text[i]);
                Console.Write(text[i]);

                // 현재 커서 위치 확인
                int currentCursorLeft = Console.CursorLeft;

                // 커서가 UI 범위를 벗어나면 줄 바꿈
                if (currentCursorLeft >= contentSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
                    Text.contentText.Add(textTemp.ToString());
                    textTemp.Clear(); // 버퍼를 클리어
                }

                //안넘어가면 아무것도 안하긴해
                //그러면 textTemp => 버퍼인데 남아있곘지?? 넣기 전이니까
            }


            if (textTemp.Length > 0)
            {
                newLineCnt[index]++;
                Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
                textTemp.Append("\0");
                Text.contentText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어
            }

            else //이미 줄바꿈을 했는데 글자가 없어서 첫 시작점인경우 
            {
                //해당 내용 임시임 특정조건 만족시 문제 가능성 있음
                newLineCnt[index]++;
                Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
                textTemp.Append("\0");
                Text.contentText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어

                cursors[index][0] = Console.CursorLeft;
                cursors[index][1] = Console.CursorTop;
            }


            cursors[index][0] = Console.CursorLeft;
            cursors[index][1] = Console.CursorTop;
        }

        // 하단 우측 UI (index + 1 == 3)
        else if (index + 1 == 3)
        {
            if (Text.optionText.Count > 0)
            {
                // 최근 문자열 가져오기
                textTemp.Append(Text.optionText[Text.optionText.Count - 1].ToString());
                Text.optionText.RemoveAt(Text.optionText.Count - 1);
            }

            //글자를 출력함
            for (int i = 0; i < text.Length; i++)
            {
                textTemp.Append(text[i]);
                Console.Write(text[i]);

                // 현재 커서 위치 확인
                int currentCursorLeft = Console.CursorLeft;

                // 커서가 UI 범위를 벗어나면 줄 바꿈
                if (currentCursorLeft >= optionStartPos_x + optionSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);
                    Text.optionText.Add(textTemp.ToString());
                    textTemp.Clear(); // 버퍼를 클리어
                }

                //안넘어가면 아무것도 안하긴해
                //그러면 textTemp => 버퍼인데 남아있곘지?? 넣기 전이니까
            }


            if (textTemp.Length > 0)
            {
                newLineCnt[index]++;
                Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);
                textTemp.Append("\0");
                Text.optionText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어
            }

            else //이미 줄바꿈을 했는데 글자가 없어서 첫 시작점인경우 
            {
                //해당 내용 임시임 특정조건 만족시 문제 가능성 있음
                newLineCnt[index]++;
                Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLineCnt[index]);
                textTemp.Append("\0");
                Text.optionText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어

                cursors[index][0] = Console.CursorLeft;
                cursors[index][1] = Console.CursorTop;
            }


            cursors[index][0] = Console.CursorLeft;
            cursors[index][1] = Console.CursorTop;
        }


    }

    static public void BackUPWriteLine(int index, string text)
    {
        index -= 1;
        // 해당 UI의 마지막 커서위치 설정
        Console.SetCursorPosition(cursors[index][0], cursors[index][1]);


        // 하단 좌측 UI (index + 1 == 2)
        if (index + 1 == 2)
        {
            if (Text.contentText.Count > 0)
            {
                // 최근 문자열 가져오기
                textTemp.Append(Text.contentText[Text.contentText.Count - 1].ToString());
                Text.contentText.RemoveAt(Text.contentText.Count - 1);
            }

            //글자를 출력함
            for (int i = 0; i < text.Length; i++)
            {
                textTemp.Append(text[i]);
                Console.Write(text[i]);

                // 현재 커서 위치 확인
                int currentCursorLeft = Console.CursorLeft;

                // 커서가 UI 범위를 벗어나면 줄 바꿈
                if (currentCursorLeft >= contentSpace_x)
                {
                    newLineCnt[index]++;
                    Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
                    Text.contentText.Add(textTemp.ToString());
                    textTemp.Clear(); // 버퍼를 클리어
                }

                //안넘어가면 아무것도 안하긴해
                //그러면 textTemp => 버퍼인데 남아있곘지?? 넣기 전이니까
            }


            if (textTemp.Length > 0)
            {
                newLineCnt[index]++;
                Console.SetCursorPosition(contentStartPos_x, contentStartPos_y + newLineCnt[index]);
                //textTemp.Append("\n"); 얘는 안넣어주는거지
                Text.contentText.Add(textTemp.ToString());
                textTemp.Clear(); // 버퍼 클리어
            }

            else //이미 줄바꿈을 했는데 글자가 없어서 첫 시작점인경우 
            {
                cursors[index][0] = Console.CursorLeft;
                cursors[index][1] = Console.CursorTop;
            }


            cursors[index][0] = Console.CursorLeft;
            cursors[index][1] = Console.CursorTop;
        }




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

    static bool isChecking = false;
    public static async Task Call_CheckWindow()
    {
        while (true)
        {
            if (!isChecking)
            {
                _ = Task.Run(() => CheckWindow());
            }

            await Task.Delay(100); // 0.1초마다 체크
        }
    }
    private static readonly object _lockObject = new object();

    static public void CheckWindow()
    {
        lock (_lockObject)
        {
            if (isChecking)
            {
                return;
            }
            isChecking = true;
            isChecking = false;
            // 변경 없으면 아무것도 안 함
            if (prevWidth == Console.WindowWidth && prevHeight == Console.WindowHeight)
            {
                isChecking = false;
                return;
            }

            else
            {
                Console.Clear();
                // 만약 기존크기에셔 변경 될 경우 아래 수행
                UISetup();

                //UI 뿌리기
                //View.DrawAsciiFrame();
                //View.DrawAsciiFrame();



                //해당부분에서 문제가 좀 생기긴하는데 나중에 조건을 걸어야할듯? 예외처리좀 해야할듯
                //Console.SetBufferSize(Console.WindowWidth + 1 , Console.WindowHeight);
                Console.SetBufferSize(Console.WindowWidth, 100);
                //Console.SetWindowSize(Console.WindowWidth , Console.WindowHeight);
                View.DrawUIFast();



                newLineCnt[0] = 0;
                newLineCnt[1] = 0;
                newLineCnt[2] = 0;



                //얘는 새로운 리스트일뿐이야
                Text.mainTextTemp = new List<string>(Text.mainText);
                Text.contentTextTemp = new List<string>(Text.contentText);
                Text.optionTextTemp = new List<string>(Text.optionText);

                Text.mainText.Clear();
                Text.contentText.Clear();
                Text.optionText.Clear();

                List<string> test = new List<string>();

                //문자열 합치기
                StringBuilder temp = new StringBuilder();
                string[] lines;
                string fullText;


                foreach (var a in Text.mainTextTemp)
                {
                    temp.Append(a);
                }

                fullText = temp.ToString();
                lines = fullText.Split('\0');

                // 줄 단위로 다시 나누기
                foreach (string line in lines)
                {
                    test.Add(line);
                }

                if (test[test.Count - 1] == "")
                {
                    test.RemoveAt(test.Count - 1);
                }

                //해상도에 맞게 각 UI의 커서위치 초기화
                SetCursor();

                for (int j = 0; j < test.Count; j++)
                {
                    UIManager.WriteLine(1, test[j]);
                }

                ////////////////////////////////////////////////////
                //UI2번
                //
                //비워줘야함
                temp.Clear();
                test.Clear();
                lines = new string[0];
                foreach (var a in Text.contentTextTemp)
                {
                    temp.Append(a);
                }

                fullText = temp.ToString();
                lines = fullText.Split('\0');


                // 줄 단위로 다시 나누기
                foreach (string line in lines) //이거 크기에 맞게 슬라이스 할 필요가 있음?? 걍 \n만 슬라이스 하는게 맞지않나?
                {
                    /*
                    for (int i = 0; i < line.Length; i += contentSpace_x)
                    {   
                        //아 test 이거 리스트 맞는데?
                        int length = Math.Min(contentSpace_x, line.Length - i);
                        test.Add(line.Substring(i, length));
                    }
                    */

                    test.Add(line);

                }

                if (test[test.Count - 1] == "")
                {
                    test.RemoveAt(test.Count - 1);
                }

                //해상도에 맞게 각 UI의 커서위치 초기화
                SetCursor();

                for (int j = 0; j < test.Count; j++)
                {
                    UIManager.WriteLine(2, test[j]);
                }

                //////////////////////////////////////////////////////
                ////UI 3번
                ///
                //비워줘야함


                temp.Clear();
                test.Clear();

                foreach (var a in Text.optionTextTemp)
                {
                    temp.Append(a);
                }

                fullText = temp.ToString();
                lines = fullText.Split('\0');

                // 줄 단위로 다시 나누기
                foreach (string line in lines)
                {
                    test.Add(line);
                }

                if (test[test.Count - 1] == "")
                {
                    test.RemoveAt(test.Count - 1);
                }

                //해상도에 맞게 각 UI의 커서위치 초기화
                SetCursor();

                for (int j = 0; j < test.Count; j++)
                {
                    UIManager.WriteLine(3, test[j]);
                }



                isResolutionChanged = true;
                inputController(currentOptions);
                isResolutionChanged = false;
                DrawAscii(currentArt);
                if (isShopUIList)
                {
                    ShopManager.Instance.PrintShopItemInfo();
                }

                Text.mainTextTemp.Clear();
                Text.contentTextTemp.Clear();
                Text.optionTextTemp.Clear();

                //Console.CursorVisible = false;// 입력 숨겨주는거 

            }
        }


    }


    //메인UI 높이 비율
    static float mainHeightRatio = 0.65f;

    //콘텐트UI 가로 비율
    static float contentWidthRatio = 0.6f;
    //콘텐트UI 세로 비율
    static float contentHeightRatio = 0.35f; // 세로빼면 되서 자동으로 받을수있지 않을까

    //옵션UI 비율
    static float optionWidthRatio = 0.4f;
    static float optionHeightRatio = 0.65f;



    //실질적으로 변경되어야하는 친구는
    //mainHeightRatio, contentWidthRatio
    static public void UISetup()
    {
        mainSpace_x = Console.WindowWidth - 3;
        mainSpace_y = (int)Math.Round(Console.WindowHeight * mainHeightRatio) - 2;
        mainStartPos_x = 1;
        mainStartPos_y = 1;

        contentSpace_x = (int)Math.Round(Console.WindowWidth * contentWidthRatio) - 4;
        contentSpace_y = (int)Math.Round(Console.WindowHeight * contentHeightRatio) - 3;
        contentStartPos_x = 3; // padding1 + 첫줄 경계선
        contentStartPos_y = (int)Math.Round(Console.WindowHeight * mainHeightRatio) + 1; //downY

        optionSpace_x = (int)Math.Round(Console.WindowWidth * optionWidthRatio) - 3; //해당비율은 1.00 - content가로비율임
        optionSpace_y = (int)Math.Round(Console.WindowHeight * contentHeightRatio) - 3;    //해당 비율 수치는 공간이기때문에 1.00 - main높이비율임
        optionStartPos_x = (int)Math.Round(Console.WindowWidth * contentWidthRatio) + 1;  //x 시작점이 결국 콘텐츠스페이스 +1이니까 content비율을 곱해줌
        optionStartPos_y = (int)Math.Round(Console.WindowHeight * mainHeightRatio) + 1;  //y 시작점이 결국 메인스페이스 +1이니까 main비율을 곱해줌

        ////////////////
        //View클래스 변경
        ////////////////
        View.width = Console.WindowWidth - 1;
        View.height = Console.WindowHeight - 1;
        View.downY = (int)Math.Round(Console.WindowHeight * mainHeightRatio);
        View.highX = (int)Math.Round(Console.WindowWidth * contentWidthRatio);

        //이전 크기 저장
        prevWidth = Console.WindowWidth;
        prevHeight = Console.WindowHeight;

        SetCursor();
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
        int index = 0, prevIndex = 0;
        int page = 0, prevPage = 0;
        RefreshOptionsPage(option, index, page);
        currentOptions = option;
        while (true)
        {
            if (isResolutionChanged)
            {
                return -1; // or 다른 특별한 코드로 루프 탈출
            }

            var key = Console.ReadKey(intercept: true).Key;
            if (key == ConsoleKey.Enter)
            {
                currentOptions = new List<Option>();
                return option[index].value;
            }

            int delta = GetDelta(key);
            if (delta == 0) continue;

            // 1) 새 인덱스·페이지 계산
            int unitMax = (int)Math.Ceiling(option.Count / 6.0) * 6;
            int newIndex = (index + delta + unitMax) % unitMax;
            if (option.Count % 6 != 0 && newIndex >= option.Count)
                newIndex = (delta == 1) ? 0 : option.Count - 1;
            int newPage = newIndex / 6;

            // 2) 페이지가 바뀌었는지 확인
            if (newPage != page)
            {
                RefreshOptionsPage(option, newPage, newIndex);
            }
            else
            {
                // RefreshOptionsPage(option, newPage, newIndex);
                // 같은 페이지 내에서만 ▶만 이동
                int oldLocal = index % 6;
                int newLocal = newIndex % 6;
                MoveHighlight(oldLocal, newLocal);
            }

            // 3) 상태 갱신
            prevIndex = index;
            prevPage = page;
            index = newIndex;
            page = newPage;
        }
    }

    static public int inputController(List<Option> option, int clearindex, string text)
    {
        int index = 0, prevIndex = 0;
        int page = 0, prevPage = 0;
        RefreshOptionsPage(option, index, page);
        currentOptions = option;

        string[] summary = GetText(option, index,text);
        foreach (var line in summary)
        {
            UIManager.WriteLine(2, line); // 오른쪽 정보 영역에 출력
        }

        while (true)
        {
            if (isResolutionChanged)
            {
                return -1; // or 다른 특별한 코드로 루프 탈출
            }

            var key = Console.ReadKey(intercept: true).Key;
            if (key == ConsoleKey.Enter)
            {
                currentOptions = new List<Option>();
                return option[index].value;
            }

            int delta = GetDelta(key);
            if (delta == 0) continue;

            // 1) 새 인덱스·페이지 계산
            int unitMax = (int)Math.Ceiling(option.Count / 6.0) * 6;
            int newIndex = (index + delta + unitMax) % unitMax;
            if (option.Count % 6 != 0 && newIndex >= option.Count)
                newIndex = (delta == 1) ? 0 : option.Count - 1;
            int newPage = newIndex / 6;

            // 2) 페이지가 바뀌었는지 확인
            if (newPage != page)
            {
                RefreshOptionsPage(option, newPage, newIndex);
            }
            else
            {
                // RefreshOptionsPage(option, newPage, newIndex);
                // 같은 페이지 내에서만 ▶만 이동
                int oldLocal = index % 6;
                int newLocal = newIndex % 6;
                MoveHighlight(oldLocal, newLocal);
            }

            // 3) 상태 갱신
            prevIndex = index;
            prevPage = page;
            index = newIndex;
            page = newPage;

            // 4) 커서 바뀔때마다 글자 갱신
            UIManager.Clear(clearindex);
            summary = GetText(option, index, text);
            foreach (var line in summary)
            {
                UIManager.WriteLine(clearindex, line);
            }
        }
    }

    // 각각의 커서에 들어갈 내용 가져오기
    public static string[] GetText(List<Option> options, int index, string input)
    {
        int idx = options[index].value;

        if (input == "skill")
        {
            var skill = SkillManager.Instance.skills[idx - 1];

            return new string[]
            {
                $" {skill.Name}  | 데미지 + {skill.Attack} | 코스트 : {skill.Cost}",
                $"{skill.Description}"
            };
        }
        else if(input == "inventory")
        {
            if (idx == 0)
            {
                return new string[] { " " };
            }

            var item = InventoryManager.Instance.inventory[idx - 1];
            return new string[] { $"{item.name} | {item.effect.type} + {item.effect.value} | {item.info} | {item.quantity}"

            };
        }else if (input == "store")
        {
            if (idx == 0)
            {
                return new string[] { " " };
            }

            var item = ShopManager.Instance.storeItems[idx-1];
            return new string[] { $"{item.name} | {item.effect.type} + {item.effect.value} | {item.info}"}; 
            
        }
        return new string[] { "정보를 불러올 수 없습니다." };
    }

    static void RefreshOptionsPage(List<Option> option, int page, int selectedIndex)
    {
        Clear(3);
        int start = page * 6;
        int countOnPage = Math.Min(6, option.Count - start);
        int maxOptionLength = optionSpace_x - 2;
        for (int i = 0; i < countOnPage; i++)
        {
            string prefix = (start + i == selectedIndex) ? "\u25B7" : "  ";
            Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + i);
            string displayText = TextCutingKorean(option[start + i].text, maxOptionLength);
            Console.Write(prefix + displayText);
        }
    }

    static void MoveHighlight(int oldLocal, int newLocal)
    {
        // 이전 ▶ 지우기
        Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + oldLocal);
        Console.Write(" ");
        // 새 ▶ 그리기
        Console.SetCursorPosition(optionStartPos_x, optionStartPos_y + newLocal);
        Console.Write("\u25B7");
    }

    static int GetDelta(ConsoleKey key) => key switch
    {
        ConsoleKey.W or ConsoleKey.UpArrow => -1,
        ConsoleKey.S or ConsoleKey.DownArrow => +1,
        ConsoleKey.A or ConsoleKey.LeftArrow => -6,
        ConsoleKey.D or ConsoleKey.RightArrow => +6,
        _ => 0
    };

    public static void DrawAscii(string asciiArt)
    {
        currentArt = asciiArt;
        UIManager.Clear(1);
        string[] lines = asciiArt.Split('\n');

        int sectorWidth = UIManager.mainSpace_x;
        int sectorHeight = UIManager.mainSpace_y;
        int startX, startY;

        // 하단 정렬
        startY = UIManager.mainStartPos_y + sectorHeight - lines.Length;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            int lineLength = line.Length;

            // 가운데 정렬
            startX = UIManager.mainStartPos_x + (sectorWidth - lineLength) / 2;

            Console.SetCursorPosition(startX, startY + i);
            Console.Write(line);
        }
    }

    public static string TextCutingKorean(string text, int maxWidth)
    {
        StringBuilder sb = new StringBuilder();
        int width = 0;
        foreach (char c in text)
        {
            int charWidth = IsWideChar(c) ? 2 : 1;
            if (width + charWidth > maxWidth)
            {
                if (charWidth == 2)
                {
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append("...");
                }
                else
                {
                    sb.Remove(sb.Length - 3, 3);
                    sb.Append("...");
                }
                break;
            }
            sb.Append(c);
            width += charWidth;
        }
        return sb.ToString();
    }

    public static bool IsWideChar(char c)
    {
        return (c >= 0xAC00 && c <= 0xD7AF) || // 한글
               (c >= 0x1100 && c <= 0x11FF); // 초성
    }


    static List<int[]> enemyPos = new List<int[]>();



    public static void EnemySetPosition(List<Monster> monsters)
    {
        enemyPos.Clear();
        int tempX;
        int tempY;

        for (int i = 0; i < monsters.Count; i++)
        {   //       (   가로        /        마릿수 +1 )   -     그림 가로 길이 / 2
            tempX = (int)Math.Round((Console.WindowWidth / (monsters.Count + 1f) - (monsters[i].monsterArt[0].Length / 2f)) * (i + 1));
            tempY = (int)Math.Round(Console.WindowHeight * 0.1f);


            enemyPos.Add(new int[] { tempX, tempY });

        }

    }

    static public void DrawEnemyName(int index, Monster monster)
    {

        Console.SetCursorPosition(enemyPos[index][0] - 1, enemyPos[index][1] - 2);
        Console.Write("Lv. " + monster.level);
        Console.Write(" " + monster.name);

    }
    static public void DrawEnemy(int index, Monster monster1)
    {
        int artX;
        int artY;

        artX = enemyPos[index][0];
        artY = enemyPos[index][1];

        for (int i = 0; i < monster1.monsterArt.Length; i++)
        {
            Console.SetCursorPosition(artX, artY + i);
            Console.Write(monster1.monsterArt[i]);

        }

    }

    public static void DrawHPBar(int index, Monster monster)
    {
        StringBuilder hpBar = new StringBuilder();

        int hpRatio;

        //비율을 10칸으로 나눠줌
        hpRatio = (int)Math.Round(((float)monster.health / (monster.maxHealth)) * 10);

        //hpRatio = hpRatio * 10;



        //10칸임 보고 수정해도 될듯?
        hpBar.Append("[");
        for (int i = 0; i < 10; i++)
        {
            if (hpRatio > i)
            {//█
                hpBar.Append("█");
            }
            else
            {
                hpBar.Append("-");
            }
        }
        hpBar.Append("]");

        Console.SetCursorPosition(enemyPos[index][0] - 2, enemyPos[index][1] + monster.monsterArt.Length + 1);
        Console.Write(hpBar.ToString());
        Console.SetCursorPosition(enemyPos[index][0] + 1, enemyPos[index][1] + monster.monsterArt.Length + 2);
        Console.Write($"                                                 ");
        Console.SetCursorPosition(enemyPos[index][0] + 1, enemyPos[index][1] + monster.monsterArt.Length + 2);
        Console.Write($"{monster.health} / {monster.maxHealth}");
    }


    public static void PlayerHPBar(Player player)
    {
        StringBuilder hpBar = new StringBuilder();
        int hpRatio;

        //비율을 10칸으로 나눠줌
        hpRatio = (int)Math.Round(((float)player.health / (player.maxHealth)) * 10);


        //귀찮다
        //int x = playerStartPos_X + (sectorWidth - lineLength) / 2;


        Console.SetCursorPosition(13, 16);
        Console.Write("Lv. " + player.level);


        Console.SetCursorPosition(10, 17);
        hpBar.Append("[");
        for (int i = 0; i < 10; i++)
        {
            if (hpRatio > i)
            {//█
                hpBar.Append("█");
            }
            else
            {
                hpBar.Append("-");
            }
        }
        hpBar.Append("]");
        Console.Write($"{hpBar.ToString()}");
        Console.SetCursorPosition(12, 18);
        Console.Write($"{player.health} / {player.maxHealth}        ");

        Console.WriteLine($"마나 : {player.mana} ");


    }
    static int playerStartPos_X, playerStartPos_Y;

    public static void DrawPlayer(string asciiArt)
    {
        currentArt = asciiArt;
        string[] lines = asciiArt.Split('\n');

        int sectorWidth = UIManager.mainSpace_x;
        int sectorHeight = UIManager.mainSpace_y;


        // 하단 정렬
        playerStartPos_Y = UIManager.mainStartPos_y + sectorHeight - lines.Length;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            int lineLength = line.Length;

            // 가운데 정렬
            //startX = UIManager.mainStartPos_x + (sectorWidth - lineLength) / 2;
            playerStartPos_X = 5;

            Console.SetCursorPosition(playerStartPos_X, playerStartPos_Y + i);
            Console.Write(line);
        }

    }

}