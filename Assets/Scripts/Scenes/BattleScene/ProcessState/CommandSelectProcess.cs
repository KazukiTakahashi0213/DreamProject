using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSelectProcess : IProcessState {
	public IProcessState BackProcess() {
		return this;
	}

	public IProcessState NextProcess() {
		return new AttackCommandSelectProcess();
	}

	public IProcessState Update(BattleManager mgr) {
		if (PlayerBattleData.GetInstance().changeMonsterActive_) {
			PlayerBattleData.GetInstance().MonsterChangeEventSet(mgr);
		}

		if (AllEventManager.GetInstance().EventUpdate()) {
			mgr.SetInputProvider(new KeyBoardNormalInputProvider());
		}

		if (mgr.GetInputProvider().UpSelect()) {
			mgr.nowCommandState_ = mgr.nowCommandState_.UpSelect(mgr);
		}
		else if (mgr.GetInputProvider().DownSelect()) {
			mgr.nowCommandState_ = mgr.nowCommandState_.DownSelect(mgr);
		}
		else if (mgr.GetInputProvider().RightSelect()) {
			mgr.nowCommandState_ = mgr.nowCommandState_.RightSelect(mgr);
		}
		else if (mgr.GetInputProvider().LeftSelect()) {
			mgr.nowCommandState_ = mgr.nowCommandState_.LeftSelect(mgr);
		}
		else if (mgr.GetInputProvider().SelectEnter()) {
			return mgr.nowCommandState_.Execute(mgr);
		}
		else if (mgr.GetInputProvider().SelectNovelWindowActive()) {
			mgr.GetNovelWindowPartsActiveState().state_ = mgr.GetNovelWindowPartsActiveState().Next(mgr);
		}

		return this;
	}
}
