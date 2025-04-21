using System;
using System.Collections.Generic;
using System.Text;

namespace textRPG
{
    // 아이템 정보
    class Item
    {
        public bool isEquipped { get; set; }
        public string name { get; set; }
        public Effect effect { get; set; }
        public string info { get; set; }
        public int price { get; set; }
        public bool isHave { get; set; }
        public Item() { }
        public Item(string name, Effect effect, string info, int price, bool isHave, bool isEquipped)
        {
            this.name = name;
            this.effect = effect;
            this.info = info;
            this.price = price;
            this.isHave = isHave;
            this.isEquipped = isEquipped;
        }
    }
}
