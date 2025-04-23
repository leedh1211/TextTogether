using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
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
        public int quantity { get; set; }
        public int maxQuantity { get; set; }
        public int upgradeLevel { get; set; }

        public Item() { }
        public Item(string name, Effect effect, string info, int price, bool isHave, bool isEquipped, int maxQuantity = 2)
        {
            this.name = name;
            this.effect = effect;
            this.info = info;
            this.price = price;
            this.isHave = isHave;
            this.isEquipped = isEquipped;
            this.quantity = 1;
            this.maxQuantity = maxQuantity;
            this.upgradeLevel = 0;
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
            new Item("삐약부츠", new Effect("방어력", 3), "걸을 때마다 삐약삐약. 은신엔 치명적이지만 귀여움은 +100.", 1300, false, false),
            new Item("분노의 마라탕", new Effect("공격력", 7), "입에서 불이 나올 만큼 맵다. 적도 같이 울게 만든다.", 2200, false, false),
            new Item("전설의 냄비뚜껑", new Effect("방어력", 8), "어머니의 사랑이 담긴 무적의 방패. 단점: 무겁다.", 2700, false, false),
            new Item("감성 기타", new Effect("공격력", 4), "잔잔한 멜로디로 적의 심장을 후벼판다.", 1600, false, false),
            new Item("고대의 슬리퍼", new Effect("공격력", 6), "집안에서 가장 강력한 무기. 맞으면 정신적 데미지 +999.", 2000, false, false),
            new Item("치즈 방패", new Effect("방어력", 5), "말랑하지만 단단한 수수께끼의 방어구. 고양이들이 좋아한다.", 1900, false, false),
            new Item("생명력 포션", new Effect("포션",3), "생명력을 올리는 포션이다",500,false,false)
        };
    }
}
