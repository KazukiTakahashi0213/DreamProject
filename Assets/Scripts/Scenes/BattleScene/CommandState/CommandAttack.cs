using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAttack : ICommandState {
	public ICommandState DownSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess();

		mgr.CommandDownCursorMove();
		return new CommandMonsterTrade();
	}
	public ICommandState LeftSelect(BattleManager mgr) {
		return this;
	}
	public ICommandState RightSelect(BattleManager mgr) {
		//どくのダメージ処理
		mgr.PoisonDamageProcess();

		mgr.CommandRightCursorMove();
		return new CommandDream();
	}
	public ICommandState UpSelect(BattleManager mgr) {
		return this;
	}

	public IProcessState Execute(BattleManager mgr) {
		//こんらんの処理の初期化
		mgr.ConfusionProcessStart();

		mgr.ChangeUiAttackCommand();

		return mgr.nowProcessState().NextProcess();
	}
}
