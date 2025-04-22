using System.Net.Mime;
using text_together;

namespace textRPG;

public class UIManager
{
    static int MainSpace_x = 117;
    static int MainSpace_y = 30;

    static int ContentSpace_x = 28;
    static int ContentSpace_y = 20;

    static int OptionSpace_x = 28;
    static int OptionSpace_y = 20;


    //UI 매개변수를 어떻게 받을까
    //이넘으로 변환해서받기
    //스트링으로 받기
    //정수로 받기
    static public void Clear(int index)
    {

        //상단 UI
        if (index == 1)
        {
            // 클리어 x,y값은 변경될 가능성 높음
            for (int y = 0; y < MainSpace_y; y++)
            {
                Console.SetCursorPosition(1, 1 + y);

                for (int x = 0; x < MainSpace_x; x++)
                {  
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        //하단 좌측 UI
        else if (index == 2)
        {
            //클리어 x,y값은 변경될 가능성 높음
            for (int y = 0; y < ContentSpace_y; y++)
            {
                Console.SetCursorPosition(1, 21 + y);

                for (int x = 0; x < ContentSpace_x; x++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        //하단 우측 UI
        else if(index == 3)
        {
            //클리어 x,y값은 변경될 가능성 높음
            for (int y = 0; y < OptionSpace_y; y++)
            {
                Console.SetCursorPosition(61, 21 + y);

                for (int x = 0; x < OptionSpace_x; x++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }


    static public void Write()
    {

    }
}




//-------------------------------------------------------------------------------------\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//|                                                                                   |\\
//=====================================================================================\\
//|                                                |                                  |\\
//|                                                |                                  |\\
//|                                                |                                  |\\
//|                                                |                                  |\\
//|                                                |                                  |\\
//|                                                |                                  |\\
//|                                                |                                  |\\
//|                                                |                                  |\\
//|                                                |                                  |\\
//|                                                |                                  |\\
//|====================================================================================\\