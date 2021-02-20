using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterBattleMenuMonsterActionCommandExecute {
    None
    , Trade
    , Skill
    , Back
    , Max
}

public class MonsterBattleMenuMonsterActionCommandExecuteStateProvider {
	public MonsterBattleMenuMonsterActionCommandExecuteStateProvider(MonsterBattleMenuMonsterActionCommandExecute setState = MonsterBattleMenuMonsterActionCommandExecute.None) {
		states_.Add(new MonsterBattleMenuMonsterActionCommandExecuteNone());
		states_.Add(new MonsterBattleMenuMonsterActionCommandExecuteTrade());
		states_.Add(new MonsterBattleMenuMonsterActionCommandExecuteSkill());
		states_.Add(new MonsterBattleMenuMonsterActionCommandExecuteBack());

		state_ = setState;
	}

	public MonsterBattleMenuMonsterActionCommandExecute state_;

	private List<BMonsterBattleMenuMonsterActionCommandExecuteState> states_ = new List<BMonsterBattleMenuMonsterActionCommandExecuteState>();

	public void Execute(MonsterMenuManager monsterMenuManager) { states_[(int)state_].Execute(monsterMenuManager); }
}
