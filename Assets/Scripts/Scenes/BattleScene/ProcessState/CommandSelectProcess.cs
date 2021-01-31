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
		if (PlayerBattleData.GetInstance().changeMonsterActive_ == true) {
			if (PlayerBattleData.GetInstance().changeMonsterNumber_ > 0) {
				mgr.GetPlayerStatusInfoParts().ProcessIdleEnd();
				mgr.GetPlayerMonsterParts().ProcessIdleEnd();

				//イベントの最後
				AllEventManager.GetInstance().EventFinishSet();

				return new EnemyCommandSelectProcess();
			}
			else {
				mgr.SetInputProvider(new KeyBoardInactiveInputProvider());

				AllEventManager.GetInstance().EventWaitSet(mgr.GetEventWaitTime());

				AllEventManager.GetInstance().UpdateGameObjectSet(mgr.GetCursorParts().GetEventGameObject());
				AllEventManager.GetInstance().UpdateGameObjectSet(mgr.GetNovelWindowParts().GetCommandParts().GetEventGameObject());
				AllEventManager.GetInstance().UpdateGameObjectsActiveSetExecute(true);

				//dpが100以上だったら
				if (PlayerBattleData.GetInstance().dreamPoint_ >= 100) {
					AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText()
						, "ゆめたちが　\n"
						+ "きょうめいしている・・・");
					AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
					AllEventManager.GetInstance().AllUpdateEventExecute();
				}
				else {
					AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), PlayerBattleData.GetInstance().GetMonsterDatas(0).uniqueName_ + "は　どうする？");
					AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
					AllEventManager.GetInstance().AllUpdateEventExecute();
				}

				AllEventManager.GetInstance().EventStatusInfoPartsSet(mgr.GetPlayerStatusInfoParts(), new Color32(0, 0, 0, 0));
				AllEventManager.GetInstance().StatusInfoPartsUpdateExecuteSet(StatusInfoPartsEventManagerExecute.IdleMoveStart);
				AllEventManager.GetInstance().AllUpdateEventExecute();

				AllEventManager.GetInstance().EventFinishSet();

				PlayerBattleData.GetInstance().changeMonsterActive_ = false;
			}
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
