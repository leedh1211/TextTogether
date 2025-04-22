using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    // 방어구 타입과 스텟 정보
    class Effect
    {
        public string type { get; set; }
        public int value { get; set; }

        public Effect() { }

        public Effect(string type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
