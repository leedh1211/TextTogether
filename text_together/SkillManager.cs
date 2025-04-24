using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    class SkillManager
    {
        public List<Skill> skills { get; private set; } = new List<Skill>();

        private static SkillManager instance;

        public static SkillManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SkillManager();
                }
                return instance;
            }
        }

        public SkillManager()
        {
            // 스킬 생성 ( 이름, 코스트, 공격력 ,설명)
            skills.Add(new Skill("활퀴기", 0, 1, 1, "태초의 기술"));
            skills.Add(new Skill("물대포", 5, 3, 2, "물속성의 근본"));
            skills.Add(new Skill("화염방사", 8, 5, 4, "화염방사기를 통해..."));
            skills.Add(new Skill("250볼트", 10, 10, 5, "정상적인 전기 기술"));
        }

        public void SelectSkill(List<Monster> monster, Player player, Dungeon dungeon)
        {
            string message = "";
            while (true)
            {
                List<Option> options = new List<Option>();
                Console.Clear();
                Console.WriteLine("현재 스테이지 : {0} \n", dungeon.stage);

                foreach (var monsters in monster)
                {
                    Console.Write($"[Lv. {monsters.level}] {monsters.name}  | ");
                    Console.WriteLine(monsters.health <= 0 ? "Dead" : $"HP : {monsters.health} ");
                }

                Console.WriteLine(message);
                
                int i = 0;
                foreach (var skill in skills)
                {
                    i++;
                    options.Add(new Option
                    {
                        text = $" {skill.Name}  | 데미지 + {skill.Attack} | 코스트 : {skill.Cost} | {skill.Description} \n", value = i,
                    });
                }
                int selectedValue = UIManager.inputController(options);
                    

                switch (selectedValue)
                {
                    case 0: return;
                    default : 
                    if (skills[selectedValue-1].Cost > player.mana)
                    {
                        message = "마나가 부족합니다.";
                        continue;
                    }
                    else
                    {
                        message = "대상을 선택하세요.";
                        DungeonManager.Instance.MonsterSelect(player, dungeon, monster, skills[selectedValue - 1]);
                        return;
                    }
                }

                Console.WriteLine("[플레이어]");
                Console.WriteLine("체력 : {0}", player.health);
                Console.WriteLine("마나 : {0}", player.mana);
                Console.WriteLine("레벨 : {0} \n", player.level);

                if (message != null)
                {
                    Console.WriteLine(message);
                }

                Console.WriteLine("원하시는 행동을 입력해주세요.");

                Console.Write(">>");

                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                    return;
                else if (input >= 1 && input <= skills.Count)
                {
                    if (skills[input - 1].Cost > player.mana)
                    {
                        message = "마나가 부족합니다.";
                        continue;
                    }
                    else
                    {
                        message = "대상을 선택하세요.";
                        DungeonManager.Instance.MonsterSelect(player, dungeon, monster, skills[input - 1]);
                        return;
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }

    
}