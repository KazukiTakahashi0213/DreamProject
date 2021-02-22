using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneProcessEventExecute : BMapSceneProcessState {
	public override MapSceneProcess Update(MapManager mapManager) {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		AllEventManager allEventMgr = AllEventManager.GetInstance();
		PlayerTrainerData playerData = PlayerTrainerData.GetInstance();

		//プレイヤーが動いていなかったら
		if (!mapManager.GetPlayerMoveMap().GetMapMoveActive()) {
			if (AllEventManager.GetInstance().EventUpdate()) {
				allSceneMgr.inputProvider_ = new KeyBoardNormalInputProvider();

				mapManager.GetPlayerMoveMap().is_move = true;

				return mapManager.eventBackProcess_;
			}
		}

		if (allSceneMgr.inputProvider_.UpSelect()) {
		}
		else if (allSceneMgr.inputProvider_.DownSelect()) {
		}
		else if (allSceneMgr.inputProvider_.RightSelect()) {
		}
		else if (allSceneMgr.inputProvider_.LeftSelect()) {
		}
		else if (allSceneMgr.inputProvider_.SelectEnter()) {
			allEventMgr.EventTriggerNext();
		}
		else if (allSceneMgr.inputProvider_.SelectBack()) {
		}
		else if (allSceneMgr.inputProvider_.SelectNovelWindowActive()) {
		}
		else if (allSceneMgr.inputProvider_.SelectMenu()) {
		}

		return mapManager.GetProcessProvider().state_;
	}
}
