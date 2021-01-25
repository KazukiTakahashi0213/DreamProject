using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand3 : IAttackCommandState {
	public IAttackCommandState DownSelect(BattleManager mgr) {
		return this;
	}
	public IAttackCommandState LeftSelect(BattleManager mgr) {
		mgr.AttackCommandLeftCursorMove();
		mgr.AttackCommandSkillInfoTextSet(2);
		return new AttackCommand2();
	}
	public IAttackCommandState RightSelect(BattleManager mgr) {
		return this;
	}
	public IAttackCommandState UpSelect(BattleManager mgr) {
		mgr.AttackCommandUpCursorMove();
		mgr.AttackCommandSkillInfoTextSet(1);
		return new AttackCommand1();
	}

	public IProcessState Execute(BattleManager mgr) {
		return mgr.nowProcessState().NextProcess();
	}
}
