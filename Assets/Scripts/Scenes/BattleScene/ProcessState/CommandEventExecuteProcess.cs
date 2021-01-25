using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEventExecuteProcess : IProcessState {
	public IProcessState BackProcess() {
		return this;
	}

	public IProcessState NextProcess() {
		return new CommandSelectProcess();
	}

	public IProcessState Update(BattleManager mgr) {
		if (AllEventManager.GetInstance().EventUpdate()) {
			mgr.GetPlayerStatusInfoParts().ProcessIdleStart();
			mgr.GetPlayerMonsterParts().ProcessIdleStart();
			mgr.ActiveUiCommand();

			return mgr.nowProcessState().NextProcess();
		}

		return this;
	}
}
