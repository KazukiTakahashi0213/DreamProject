using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneProcessMenuSelect : BaseMapSceneProcessState {
	//選択肢制御
	private int selectNum_ = 0;
	private MapSceneMenuSelectCommandSelectProvider commandSelectProvider_ = new MapSceneMenuSelectCommandSelectProvider();
	public MapSceneMenuSelectCommandSelectProvider GetCommandSelectProvider() { return commandSelectProvider_; }

	public override MapSceneProcess Update(MapManager mapManager) {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		AllEventManager allEventMgr = AllEventManager.GetInstance();

		AllEventManager.GetInstance().EventUpdate();

		if (allSceneMgr.inputProvider_.UpSelect()) {
			if (selectNum_ > 0) {
				t13.UnityUtil.ObjectPosAdd(mapManager.GetCommandParts().GetCursorParts().gameObject, new Vector3(0, 0.55f, 0));

				--selectNum_;

				commandSelectProvider_.state_ = (MapSceneMenuSelectCommandSelect)selectNum_;
			}
		}
		else if (allSceneMgr.inputProvider_.DownSelect()) {
			if (selectNum_ < mapManager.GetCommandParts().GetCommandWindowTextsCount()) {
				t13.UnityUtil.ObjectPosAdd(mapManager.GetCommandParts().GetCursorParts().gameObject, new Vector3(0, -0.55f, 0));

				++selectNum_;

				commandSelectProvider_.state_ = (MapSceneMenuSelectCommandSelect)selectNum_;
			}
		}
		else if (allSceneMgr.inputProvider_.RightSelect()) {
		}
		else if (allSceneMgr.inputProvider_.LeftSelect()) {
		}
		else if (allSceneMgr.inputProvider_.SelectEnter()) {
			//allEventMgr.EventTriggerNext();
			commandSelectProvider_.SelectEnter(mapManager);
		}
		else if (allSceneMgr.inputProvider_.SelectBack()) {
			mapManager.GetPlayerMoveMap().is_move = true;
			mapManager.GetCommandParts().gameObject.SetActive(false);

			//操作の変更
			allSceneMgr.inputProvider_ = new KeyBoardNormalInputProvider();

			return MapSceneProcess.PlayerMove;
		}
		else if (allSceneMgr.inputProvider_.SelectNovelWindowActive()) {
		}
		else if (allSceneMgr.inputProvider_.SelectMenu()) {
			mapManager.GetPlayerMoveMap().is_move = true;
			mapManager.GetCommandParts().gameObject.SetActive(false);

			//操作の変更
			allSceneMgr.inputProvider_ = new KeyBoardNormalInputProvider();

			return MapSceneProcess.PlayerMove;
		}

		return mapManager.GetProcessProvider().state_;
	}
}
