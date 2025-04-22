using textRPG;

namespace text_together;

public class TempOptionManager
{
    public int? inputController(List<Option> option)
    {
        int index = 0;
        int count = option.Count;
        UIManager.Clear(2);

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
            UIManager.Clear(2);
        }
    }
    
    int GetDelta(ConsoleKey key) => key switch
    {
        ConsoleKey.W or ConsoleKey.UpArrow   => -1,
        ConsoleKey.S or ConsoleKey.DownArrow => +1,
        _                                    => 0
    };
}