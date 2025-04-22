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


    //UI �Ű������� ��� ������
    //�̳����� ��ȯ�ؼ��ޱ�
    //��Ʈ������ �ޱ�
    //������ �ޱ�
    static public void Clear(int index)
    {

        //��� UI
        if (index == 1)
        {
            // Ŭ���� x,y���� ����� ���ɼ� ����
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

        //�ϴ� ���� UI
        else if (index == 2)
        {
            //Ŭ���� x,y���� ����� ���ɼ� ����
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

        //�ϴ� ���� UI
        else if(index == 3)
        {
            //Ŭ���� x,y���� ����� ���ɼ� ����
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