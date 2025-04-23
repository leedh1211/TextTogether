

namespace text_together;

public class TempOptionManager
{
    static public int inputController(List<Option> option)
    {
        int index = 0;
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
            index = (index + delta + count) % count;
            MakeOptionString(option, index);
        }
    }
    
    static void MakeOptionString(List<Option> option, int index)
    {
        foreach (var optionalData in option)
        {
            string text = optionalData.text;
            if (optionalData.value == index)
            {
                text = "\u25b7"+optionalData.text;
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