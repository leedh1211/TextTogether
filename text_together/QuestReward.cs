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
        public int rewardQuantity;
        public string rewardType;
        public QuestReward(string rewardName, int rewardQuantity, string rewardType)
        {
            this.rewardName = rewardName;
            this.rewardQuantity = rewardQuantity;
            this.rewardType = rewardType;
        }
    }
}
