using System;
using System.Collections.Generic;
using System.Text;

namespace textRPG
{
    class SkillManager
    {
        public List<Skill> skills { get; private set; } = new List<Skill>();
       
         public SkillManager()
        {
            // 스킬 생성 ( 이름, 코스트, 공격력 ,설명)
            skills.Add(new Skill("활퀴기", 0, 1, 1, "태초의 기술"));
            skills.Add(new Skill("물대포", 5, 3, 2, "물속성의 근본"));
            skills.Add(new Skill("화염방사", 8, 5, 4, "화염방사기를 통해..."));
            skills.Add(new Skill("250볼트", 10, 10, 5, "정상적인 전기 기술"));
        }
    }
}