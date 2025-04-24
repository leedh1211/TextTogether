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
        private List<Quest> quests = new List<Quest>();
        // 보상 프리셋 정의
        QuestReward Gold(int amount = 50) =>
            new QuestReward("골드", amount, "다양한 물품을 구매할 수 있는 골드이다.", "G");

        QuestReward Exp(int amount = 10) =>
            new QuestReward("경험치", amount, "경험치를 모아서 레벨업을 해보아요", "경험치", amount);

        QuestReward Armor(string name, int price, string info, int effect, int count = 1) =>
            new QuestReward(name, price, info, "방어구", effect, count);
        QuestReward Weapon(string name, int price, string info, int effect, int count = 1) =>
           new QuestReward(name, price, info, "공격력", effect, count);
    
        // 퀘스트 생성
        public List<Quest> QuestInit()
        {
            this.quests = new List<Quest>();
            quests.Add(new Quest(
            "도로롱의 위협",
            "밤마다 도로롱이 마을 주변을 떠돌며 이상한 소리를 냅니다.주민들이 잠을 못 자요!",
            new List<QuestGoal>
            {  new QuestGoal("도로롱", 5, 0) },
            new List<QuestReward>
            {  Exp(10),Gold(50), Armor("수련자 갑옷", 500, "수련에 도움을 주는 갑옷입니다.", 5) }, 2));
            quests.Add(new Quest(
            "치킨의 복수",
            "수상한 치킨들이 던전을 습격하고 있습니다. 평화를 지켜주세요!",
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

        public int GoQuest(Player player)
        {
            while (true)
            {
                UIManager.Clear(1);
                UIManager.Clear(2);
                UIManager.Clear(3);
                List<Option> options = new List<Option>();
                UIManager.WriteLine(2,"퀘스트 목록");
                options.Add(new Option { text = "나가기", value = 0, });
                var availableQuests = quests.Where(q => q.level <= player.level).ToList();

                if (availableQuests.Count == 0)
                {

                    Console.WriteLine("수행할 수 있는 퀘스트가 없습니다.\n");
                    continue;
                }
                for (int i = 0; i < availableQuests.Count; i++)
                {
                    var quest = availableQuests[i];
                    string status = quest.isRewarded ? "[완료]" :
                                    quest.isAccepted && quest.questGoals.All(g => g.IsComplete) ? "[완료 대기]" :
                                    quest.isAccepted ? "[진행 중]" : "[미수락]";
                    options.Add(new Option { text = $"{quest.questName} \n", value = i + 1, });
                    UIManager.WriteLine(2, $"{quest.questName} {quest.questInfo} {status}");
                }

                int selectedValue = UIManager.inputController(options);

                if (selectedValue == 0)
                {
                    return 0;
                }
                else if (availableQuests.Count == 0)
                {
                    UIManager.WriteLine(2,"퀘스트가 없습니다.");
                    continue;
                }
                else
                {
                    QuestUI(availableQuests[selectedValue - 1], player);
                }
            }
        }

        // 퀘스트 수락 여부 체크
        void QuestUI(Quest quest, Player player)
        {
            UIManager.Clear(1);
            UIManager.Clear(2);
            UIManager.Clear(3);
            UIManager.WriteLine(2, $"{quest.questName} \n{quest.questInfo}");


            foreach (var goal  in quest.questGoals)
            {
                UIManager.WriteLine(2,$"- {goal.questTarget} {goal.requiredCount}마리 처치");
                if(quest.isAccepted)
                {
                    UIManager.WriteLine(2,$"[퀘스트 진행] {quest.questName} - {goal.questTarget}: {goal.currentCount}/{goal.requiredCount}\n");
                }
            }
           
            UIManager.WriteLine(2, "\n보상");
            foreach(var reward in quest.questRewards)
            {
                if(reward.rewardType == "G")
                    UIManager.WriteLine(2, $"{reward.rewardPrice}G");
                else if(reward.rewardType == "경험치")
                    UIManager.WriteLine(2, $"{reward.rewardPrice}EXP");
                else
                    UIManager.WriteLine(2, $"{reward.rewardName} x {reward.rewardCount}");

            }
            List<Option> options = new List<Option>();

            options.Add(new Option { text = "나가기", value = 0, });
            
            if (!quest.isAccepted)
            {
                options.Add(new Option { text = "퀘스트 수락", value = 1, });
            }
            else if (quest.questGoals.All(g => g.IsComplete) && !quest.isRewarded)
            {
                options.Add(new Option { text = "보상 받기", value = 1, });
            }
            int selectedValue = UIManager.inputController(options);
            if (quest.IsCompleted && selectedValue == 1)
            {
                quest.GiveReward(quest, player);
                quest.isRewarded = true;
            }
            else if(!quest.IsCompleted && selectedValue == 1)
            {
                quest.isAccepted = true;
                return;
            }
            else
            {
                if (selectedValue == 0)
                {
                    return;
                }
                else
                {
                    UIManager.WriteLine(2, "다시 입력해주세요");
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
                        UIManager.WriteLine(2,$"[퀘스트 진행] {quest.questName} - {goal.questTarget}: {goal.currentCount}/{goal.requiredCount}");
                    }
                }
            }
        }
    }
}
