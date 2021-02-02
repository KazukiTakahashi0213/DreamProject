using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand2 : IAttackCommandState {
	public IAttackCommandState DownSelect(BattleManager mgr) {
		return this;
	}
	public IAttackCommandState LeftSelect(BattleManager mgr) {
		return this;
	}
	public IAttackCommandState RightSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess();

		mgr.AttackCommandRightCursorMove();
		mgr.AttackCommandSkillInfoTextSet(3);
		return new AttackCommand3();
	}
	public IAttackCommandState UpSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess();

		mgr.AttackCommandUpCursorMove();
		mgr.AttackCommandSkillInfoTextSet(0);
		return new AttackCommand0();
	}

	public IProcessState Execute(BattleManager mgr) {
		return mgr.nowProcessState().NextProcess();
	}
}
