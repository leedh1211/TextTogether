using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


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
        private List<Quest> quests;
        // 보상 프리셋 정의
        public QuestReward Gold(int amount = 50) =>
            new QuestReward("골드", amount, "다양한 물품을 구매할 수 있는 골드이다.", "G");

        public QuestReward Exp(int amount = 10) =>
            new QuestReward("경험치", amount, "경험치를 모아서 레벨업을 해보아요", "경험치", amount);

        public QuestReward Armor(string name, int price, string info, int effect, int count = 1) =>
            new QuestReward(name, price, info, "방어구", effect, count);
        public QuestReward Weapon(string name, int price, string info, int effect, int count = 1) =>
           new QuestReward(name, price, info, "공격력", effect, count);
    
        // 퀘스트 생성
        public List<Quest> QuestInit()
        {
            this.quests = new List<Quest>();
            quests.Add(new Quest(
            "도로롱의 위협",
            "밤마다 도로롱이 마을 주변을 떠돌며 이상한 소리를 냅니다.\n주민들이 잠을 못 자요!",
            new List<QuestGoal>
            {  new QuestGoal("도로롱", 5, 0) },
            new List<QuestReward>
            {  Exp(10),Gold(50), Armor("수련자 갑옷", 500, "수련에 도움을 주는 갑옷입니다.", 5) }, 2));
            quests.Add(new Quest(
            "치킨의 복수",
            "수상한 치킨들이 던전을 습격하고 있습니다. \n평화를 지켜주세요!",
            new List<QuestGoal>
            {  new QuestGoal("치킨", 3, 0) },
            new List<QuestReward>
            {  Exp(10),Gold(50), Armor("수련자 갑옷", 500, "수련에 도움을 주는 갑옷입니다.", 5) },1));
            quests.Add(new Quest(
             "까부냥 소탕 작전",
             "까부냥들이 마을 근처에서 귀여움을 무기로 소란을 피우고 있어요! 모두 진정시켜 주세요!",
            new List<QuestGoal>
            {  new QuestGoal("까부냥", 5, 0) },
            new List<QuestReward>{  Exp(10),Gold(50)},3));
            quests.Add(new Quest(
            "최종 결전! 보스를 물리쳐라",
            "모든 재앙의 원흉 '보스'가 눈을 떴습니다. 모두의 힘을 모아 쓰러뜨리세요!",
            new List<QuestGoal>
            {  new QuestGoal("도로롱", 5, 0) },
            new List<QuestReward>{  Exp(10),Gold(50)},4));


            return quests;
        }

        public void GoQuest(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("퀘스트 목록\n");

                var availableQuests = quests.Where(q => q.level <= player.level).ToList();

                if (availableQuests.Count == 0)
                {
                    
                    Console.WriteLine("수행할 수 있는 퀘스트가 없습니다.\n");
                    Console.WriteLine("0. 나가기");
                    if (int.TryParse(Console.ReadLine(), out int zero) && zero == 0)
                        return;
                    continue;
                }
                for (int i = 0; i < availableQuests.Count; i++)
                {
                    var quest = availableQuests[i];
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
                if (input > 0 && input <= availableQuests.Count)
                {
                    QuestUI(availableQuests[input - 1], player);
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
                if(reward.rewardType == "G")
                    Console.WriteLine($"{reward.rewardPrice}G");
                else if(reward.rewardType == "경험치")
                    Console.WriteLine($"{reward.rewardPrice}EXP");
                else
                    Console.WriteLine($"{reward.rewardName} x {reward.rewardCount}");

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
