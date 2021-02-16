using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterBattleMenuMonsterActionCommand {
	None
	, Trade
	, Skill
}

public class MonsterBattleMenuMonsterActionCommandStateProvider : BActionCommandStateProvider {
	public MonsterBattleMenuMonsterActionCommandStateProvider() {
		processStates_.Add(new MonsterBattleMenuMonsterActionCommandStateNone());
		processStates_.Add(new MonsterBattleMenuMonsterActionCommandStateTrade());
		processStates_.Add(new MonsterBattleMenuMonsterActionCommandStateSkill());
	}
}
