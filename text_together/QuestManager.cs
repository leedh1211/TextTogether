using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using textRPG;

namespace text_together
{
    class QuestManager
    {
        // 싱글톤
        private static QuestManager instance;

        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance;
            }
        }
        private List<Quest> quests = new List<Quest>();

        // 퀘스트 생성
        public void QuestInit()
        {
            var quest = new Quest(
            questName: "마을을 위협하는 미니언 처치",
            questInfo: "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!",
            questGoals: new List<QuestGoal>
            {  new QuestGoal("미니언", 5, 0) },
            questRewards: new List<QuestReward>
            {
                new QuestReward("쓸만한 방패", 1000,"튼튼한 방패", "방어구",5),
                new QuestReward("골드", 50," ","G",0)
            });

            quests.Add(quest);
        }

        public void GoQuest(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("퀘스트 목록\n");

                for (int i = 0; i < quests.Count; i++)
                {
                    var quest = quests[i];
                    string status = quest.isRewarded ? "[완료]" :
                                    quest.isAccepted && quest.questGoals.All(g => g.IsComplete) ? "[완료 대기]" :
                                    quest.isAccepted ? "[진행 중]" : "[미수락]";

                    Console.WriteLine($"{i + 1}. {quest.questName} {status}");
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("퀘스트 번호를 입력해주세요: \n");

                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                {
                    return;
                }
                if (input > 0 && input <= quests.Count)
                {
                    QuestUI(quests[input - 1], player);
                }
                else
                {
                    Console.WriteLine("올바른 번호를 입력해주세요.");
                }
            }
        }

        // 퀘스트 수락 여부 체크
        void QuestUI(Quest quest, Player player)
        {
            Console.Clear();
            Console.WriteLine("Quest!!\n");
            Console.WriteLine(quest.questName + "\n");
            Console.WriteLine(quest.questInfo + "\n");
            

            foreach(var goal  in quest.questGoals)
            {
                Console.WriteLine($"- {goal.questTarget} {goal.requiredCount}마리 처치");
                if(quest.isAccepted)
                {
                    Console.WriteLine($"[퀘스트 진행] {quest.questName} - {goal.questTarget}: {goal.currentCount}/{goal.requiredCount}\n");
                }
            }
           
            Console.WriteLine("\n보상");
            foreach(var reward in quest.questRewards)
            {
                Console.WriteLine($"{reward.rewardName}");
            }

            if (!quest.isAccepted)
            {
                Console.WriteLine("\n1. 퀘스트 수락");
                Console.WriteLine("2. 돌아가기");
                int choice = HandleInput(accept: true);
                if (choice == 1)
                {
                    quest.Accept();
                    return;
                }
                else if (choice == 2)
                {
                    return;
                }

            }
            else
            {
                if (quest.questGoals.All(g => g.IsComplete) && !quest.isRewarded)
                {
                    Console.WriteLine("\n1. 보상 받기");
                    Console.WriteLine("2. 돌아가기");
                    int choice = HandleInput(reward: true);
                    if (choice == 1)
                    {
                        quest.GiveReward(quest,player);
                        quest.isRewarded = true;
                    }
                    else if (choice == 2)
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("\n2. 돌아가기");
                    int choice = HandleInput(accept: true);
                    if (choice == 2)
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("다시 입력해주세요");
                    }
                }
            }
        }

        int HandleInput(bool accept = false, bool reward = false)
        {
            while (true)
            {
                Console.Write("\n원하시는 행동을 입력해주세요. \n");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    if (accept)
                    {
                        return 1;
                    }
                    if (reward)
                    {
                        return 1;
                    }
                }
                else if (input == "2")
                {
                    return 2; // 퀘스트목록 창으로 돌아가기
                }
                else
                {
                    Console.WriteLine("다시 입력해주세요.");
                }
            }
        }
        // 몬스터처치 후 퀘스트에 반영
        public void HandleMonsterKill(string monsterName)
        {
            foreach (var quest in quests.Where(q => q.isAccepted && !q.isRewarded))
            {
                foreach (var goal in quest.questGoals)
                {
                    if (!goal.IsComplete && goal.questTarget == monsterName)
                    {
                        goal.AddProgress();
                        Console.WriteLine($"[퀘스트 진행] {quest.questName} - {goal.questTarget}: {goal.currentCount}/{goal.requiredCount}");
                    }
                }
            }
        }
    }
}
