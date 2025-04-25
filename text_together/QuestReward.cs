using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_together
{
    class QuestReward
    {
        public string rewardName { get; set; }
        public int rewardPrice { get; set; }
        public string rewardInfo { get; set; }
        public string rewardType { get; set; }
        public int rewardEffect { get; set; }
        public int rewardCount { get; set; }
        public QuestReward(string rewardName, int rewardPrice, string rewardInfo, string rewardType, int rewardEffect = 0, int rewardCount = 0)
        {
            this.rewardName = rewardName;
            this.rewardPrice = rewardPrice;
            this.rewardInfo = rewardInfo;
            this.rewardType = rewardType;
            this.rewardEffect = rewardEffect;
            this.rewardCount = rewardCount;
        }
    }   
}
