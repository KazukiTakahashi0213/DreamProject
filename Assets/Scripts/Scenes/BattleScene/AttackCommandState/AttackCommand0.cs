using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand0 : IAttackCommandState {
	public IAttackCommandState DownSelect(BattleManager mgr) {
		mgr.AttackCommandDownCursorMove();
		mgr.AttackCommandSkillInfoTextSet(2);
		return new AttackCommand2();
	}
	public IAttackCommandState LeftSelect(BattleManager mgr) {
		return this;
	}
	public IAttackCommandState RightSelect(BattleManager mgr) {
		mgr.AttackCommandRightCursorMove();
		mgr.AttackCommandSkillInfoTextSet(1);
		return new AttackCommand1();
	}
	public IAttackCommandState UpSelect(BattleManager mgr) {
		return this;
	}

	public IProcessState Execute(BattleManager mgr) {
		return mgr.nowProcessState().NextProcess();
	}
}
