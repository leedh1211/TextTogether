using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace text_together
{
    internal class Quest
    {
        public string questName;
        public string questInfo;
        public List<QuestGoal> questGoals;
        public List<QuestReward> questRewards;
        public int level;

        public bool isAccepted = false;
        public bool isRewarded { get; set; } = false;
 

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
        public bool IsCompleted => questGoals.All(g => g.IsComplete);

        // 퀘스트 성공했을 경우 보상
        public void GiveReward(Quest quest, Player player)
        {
            // 인벤토리 업데이트
            if (IsCompleted)
            {
                Console.WriteLine($"'{quest.questName}' 퀘스트 완료! 보상을 받았습니다");
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
                        InventoryManager.Instance.inventory.Add(new Item(reward.rewardName, new Effect(reward.rewardType, reward.rewardEffect), reward.rewardInfo, reward.rewardPrice, false, false));
                    }
                }
            }
        }
    }
}
