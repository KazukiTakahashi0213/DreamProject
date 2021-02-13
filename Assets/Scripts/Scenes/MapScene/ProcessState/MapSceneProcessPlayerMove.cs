using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneProcessPlayerMove : BaseMapSceneProcessState {
	public override MapSceneProcess Update(MapManager mapManager) {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		AllEventManager allEventMgr = AllEventManager.GetInstance();

		AllEventManager.GetInstance().EventUpdate();

		if (mapManager.GetPlayerMoveMap().GetEntryZone()._collision_object
			&& !mapManager.GetPlayerMoveMap().GetMapMoveActive()) {
			EventMoveMap eventObject = mapManager.GetPlayerMoveMap().GetEntryZone()._collision_object;

			if (eventObject.GetTriggerState().EventTrigger(mapManager.GetPlayerMoveMap().GetEntryZone())) {
				eventObject.GetEventSetFuncs()[eventObject.executeEventNum_](eventObject, mapManager);

				eventObject.eventActive_ = false;
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
			mapManager.GetPlayerMoveMap().is_move = false;
			mapManager.GetCommandParts().gameObject.SetActive(true);

			//操作の変更
			allSceneMgr.inputProvider_ = new KeyBoardNormalTriggerInputProvider();

			return MapSceneProcess.MenuSelect;
		}

		return mapManager.GetProcessProvider().state_;
	}
}
