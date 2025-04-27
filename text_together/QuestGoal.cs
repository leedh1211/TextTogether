using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_together
{
    class QuestGoal
    {
        public string questTarget { get; set; }
        public int requiredCount { get; set; }
        public int currentCount { get;  set; }
        public bool isQuestGoalCompleted { get; set; }
        public QuestGoal(string questTarget, int requiredCount, int currentCount)
        {
            this.questTarget = questTarget;
            this.requiredCount = requiredCount;
            this.currentCount = 0;
        }

        // 퀘스트 진행 중 체크
        public void AddProgress()
        {
            if (!this.isQuestGoalCompleted)
                currentCount++;
        }

        // 퀘스트 성공 여부 체크
        public bool IsComplete()
        {
            if (currentCount >= requiredCount)
            {
                isQuestGoalCompleted = true;
            }
            else
                isQuestGoalCompleted = false;
            return isQuestGoalCompleted;
        }
    }
}
