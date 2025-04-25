using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static text_together.MonsterAscii;

namespace text_together
{


    public class TempUIManager1
    {       //전부 접근가능하게 해주는게 static public , static은 전역선언 =  어디서든 존재함
            //private는 다른곳에서 접근이 어렵다, public은 접근이 가능
            //void = 함수 리턴값 없음 예를 들면 
        static public void DrawEnemy(int CatX, int CatY, Monster monster1)//미리집어넣은 변수를 매개변수라고 한다
        {                                               //설계도  결과물
            int artX;
            int artY;
            //커서 위치
            artX = CatX;
            artY = CatY;
            string[] line = monster1.monsterArt;

            for (int i = 0; i < monster1.monsterArt.Length; i++)
            {
                Console.SetCursorPosition(artX, artY + i);
                Console.Write(monster1.monsterArt[i]);

            }




            





        }
    }
}

