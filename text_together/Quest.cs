using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_together
{
    internal class Quest
    {
        public string questName;
        public string questInfo;
        public List<QuestGoal> questGoals;
        public List<QuestReward> questRewards;

        public bool isAccepted = false;
        public bool isRewarded { get; set; } = false;
 

        public Quest(string questName, string questInfo, List<QuestGoal> questGoals, List<QuestReward> questRewards)
        {
            this.questName = questName;
            this.questInfo = questInfo;
            this.questGoals = questGoals;
            this.questRewards = questRewards;
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
        public void GiveReward(Quest quest)
        {
            // 인벤토리로 변경 예정
            if (IsCompleted)
            {
                Console.WriteLine($"'{quest.questName}' 퀘스트 완료! 보상을 받았습니다");
                foreach (var reward in quest.questRewards) 
                {
                    Console.WriteLine($"- {reward.rewardName} x{reward.rewardQuantity}");
                }
            }
        }
    }
}
