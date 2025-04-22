using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_together
{
    class QuestReward
    {
        public string rewardName;
        public int rewardPrice;
        public string rewardInfo;
        public string rewardType;
        public int rewardEffect;
        public QuestReward(string rewardName, int rewardPrice, string rewardInfo, string rewardType, int rewardEffect)
        {
            this.rewardName = rewardName;
            this.rewardPrice = rewardPrice;
            this.rewardInfo = rewardInfo;
            this.rewardType = rewardType;
            this.rewardEffect = rewardEffect; 
        }
    }
}
