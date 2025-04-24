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
                UIManager.Clear(1);
                UIManager.Clear(2);
                UIManager.Clear(3);
                UIManager.WriteLine(2,$"현재 스테이지1 : {dungeon.stage}" );

                foreach (var monsters in monster)
                {
                    String monsterinfoText = DungeonManager.getMonsterInfoText(monsters);
                    UIManager.WriteLine(2,monsterinfoText);
                }

                UIManager.WriteLine(2,message);
                
                int i = 0;
                foreach (var skill in skills)
                {
                    i++;
                    options.Add(new Option
                    {
                        text = $" {skill.Name}  | 데미지 + {skill.Attack} | 코스트 : {skill.Cost} | {skill.Description} ", value = i,
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

                UIManager.WriteLine(2,"[플레이어]");
                UIManager.WriteLine(2,$"체력 : {player.health}" );
                UIManager.WriteLine(2,$"마나 : {player.mana}" );
                UIManager.WriteLine(2,$"레벨 : {player.level} " );

                if (message != null)
                {
                    UIManager.WriteLine(2,message);
                }

                UIManager.WriteLine(2,"원하시는 행동을 입력해주세요.");

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