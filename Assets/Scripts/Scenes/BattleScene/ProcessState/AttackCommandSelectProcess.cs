using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommandSelectProcess : IProcessState {
	public IProcessState BackProcess() {
		return new CommandSelectProcess();
	}

	public IProcessState NextProcess() {
		return new EnemyCommandSelectProcess();
	}

	public IProcessState Update(BattleManager mgr) {
		AllEventManager.GetInstance().EventUpdate();

		if (mgr.GetInputProvider().UpSelect()) {
			mgr.nowAttackCommandState_ = mgr.nowAttackCommandState_.UpSelect(mgr);
		}
		else if (mgr.GetInputProvider().DownSelect()) {
			mgr.nowAttackCommandState_ = mgr.nowAttackCommandState_.DownSelect(mgr);
		}
		else if (mgr.GetInputProvider().RightSelect()) {
			mgr.nowAttackCommandState_ = mgr.nowAttackCommandState_.RightSelect(mgr);
		}
		else if (mgr.GetInputProvider().LeftSelect()) {
			mgr.nowAttackCommandState_ = mgr.nowAttackCommandState_.LeftSelect(mgr);
		}
		else if (mgr.GetInputProvider().SelectEnter()) {
			ISkillData playerSkillData = PlayerBattleData.GetInstance().GetMonsterDatas(0).GetSkillDatas(mgr.playerSelectSkillNumber_);

			if (playerSkillData.nowPlayPoint_ > 0) {
				mgr.GetPlayerStatusInfoParts().ProcessIdleEnd();
				mgr.GetPlayerMonsterParts().ProcessIdleEnd();

				//ppの消費
				playerSkillData.nowPlayPoint_ -= 1;

				return mgr.nowAttackCommandState_.Execute(mgr);
			}
		}
		else if (mgr.GetInputProvider().SelectBack()) {
			mgr.ChangeUiCommand();

			return mgr.nowProcessState().BackProcess();
		}
		else if (mgr.GetInputProvider().SelectNovelWindowActive()) {
			mgr.GetNovelWindowPartsActiveState().state_ = mgr.GetNovelWindowPartsActiveState().Next(mgr);
		}

		return this;
	}
}
