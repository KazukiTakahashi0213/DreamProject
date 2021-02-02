using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand1 : IAttackCommandState {
	public IAttackCommandState DownSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess();

		mgr.AttackCommandDownCursorMove();
		mgr.AttackCommandSkillInfoTextSet(3);
		return new AttackCommand3();
	}
	public IAttackCommandState LeftSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess();

		mgr.AttackCommandLeftCursorMove();
		mgr.AttackCommandSkillInfoTextSet(0);
		return new AttackCommand0();
	}
	public IAttackCommandState RightSelect(BattleManager mgr) {
		return this;
	}
	public IAttackCommandState UpSelect(BattleManager mgr) {
		return this;
	}

	public IProcessState Execute(BattleManager mgr) {
		return mgr.nowProcessState().NextProcess();
	}
}
