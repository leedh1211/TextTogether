using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace text_together
{
    internal class Quest
    {
        public string questName { get; set; }
        public string questInfo { get; set; }
        public List<QuestGoal> questGoals { get; set; }
        public List<QuestReward> questRewards { get; set; }
        public int level { get; set; }

        public bool isAccepted { get; set; } = false;
        public bool isRewarded { get; set; } = false;
        public bool isCompleted { get; set; } = false;
 

        public Quest(string questName, string questInfo, List<QuestGoal> questGoals, List<QuestReward> questRewards,int level)
        {
            this.questName = questName;
            this.questInfo = questInfo;
            this.questGoals = questGoals;
            this.questRewards = questRewards;
            this.level = level;
        }

        // 퀘스트 수락
        public void Accept()
        {
            isAccepted = true;
            Console.WriteLine("퀘스트를 수락했습니다.");
        }

        // 퀘스트 전체가 성공했는지 체크
        public void IsCompleted()
        {
            isCompleted = questGoals.All(g => g.IsComplete());
        }

        // 퀘스트 성공했을 경우 보상
        public void GiveReward(Quest quest, Player player, List<Item> inventory)
        {
            if (this.isCompleted)
            {
                foreach (var reward in quest.questRewards) 
                {
                    if (reward.rewardType == "G")
                    {
                        player.gold += reward.rewardPrice;
                    }
                    else if(reward.rewardType == "경험치")
                    {
                        player.exp += reward.rewardEffect;
                    }
                    else
                    {
                        inventory.Add(new Item(reward.rewardName, new Effect(reward.rewardType, reward.rewardEffect), reward.rewardInfo, reward.rewardPrice, false, false));
                    }
                }
            }
            return;
        }
    }
}
