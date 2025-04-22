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

    static class ItemData
    {
        public static List<Item> ItemPool { get; private set; } = new List<Item>()
        {
            new Item("고양이 발톱", new Effect("공격력", 3), "은밀하고 빠른 공격을 위한 장비. 간지러울 수도 있음.", 900, false, false),
            new Item("오리 튜브", new Effect("방어력", 4), "물에도 뜨고, 적의 공격도 튕겨냅니다. 아마도요.", 1200, false, false),
            new Item("용의 송곳니", new Effect("공격력", 10), "진짜 용의 송곳니인지 의심스러운, 그러나 강력한 무기.", 3000, false, false),
            new Item("닌자 두건", new Effect("방어력", 6), "은신 +10, 간지 +50. 정작 방어력도 있다.", 2000, false, false),
            new Item("버섯 갑옷", new Effect("방어력", 7), "푹신푹신. 스파르타인에게는 모욕적인 방어구.", 2500, false, false),
            new Item("하트망치", new Effect("공격력", 5), "사랑을 담아 내려치는 무기. 정신적인 데미지는 추가.", 1800, false, false),
        };
    }
}
