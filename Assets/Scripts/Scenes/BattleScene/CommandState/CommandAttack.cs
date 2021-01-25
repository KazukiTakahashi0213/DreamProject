using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAttack : ICommandState {
	public ICommandState DownSelect(BattleManager mgr) {
		mgr.CommandDownCursorMove();
		return new CommandMonsterTrade();
	}
	public ICommandState LeftSelect(BattleManager mgr) {
		return this;
	}
	public ICommandState RightSelect(BattleManager mgr) {
		mgr.CommandRightCursorMove();
		return new CommandItem();
	}
	public ICommandState UpSelect(BattleManager mgr) {
		return this;
	}

	public IProcessState Execute(BattleManager mgr) {
		mgr.ChangeUiAttackCommand();

		return mgr.nowProcessState().NextProcess();
	}
}
