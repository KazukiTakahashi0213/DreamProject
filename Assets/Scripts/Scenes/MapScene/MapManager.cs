using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, ISceneManager {
	[SerializeField] Camera mainCamera_ = null;
	[SerializeField] PlayerMoveMap playerMoveMap_ = null;
	[SerializeField] NovelWindowParts novelWindowParts_ = null;

	public NovelWindowParts GetNovelWindowParts() { return novelWindowParts_; }

	public void SceneStart() {
		//フロントスクリーンをカメラの子にする
		AllSceneManager.GetInstance().GetPublicFrontScreen().gameObject.transform.SetParent(mainCamera_.transform);

		//ノベルウィンドウをカメラの子にする
		novelWindowParts_.gameObject.transform.SetParent(mainCamera_.transform);
	}

	public void SceneUpdate() {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		AllEventManager allEventMgr = AllEventManager.GetInstance();

		if (AllEventManager.GetInstance().EventUpdate()) {
			allSceneMgr.inputProvider_ = new KeyBoardNormalInputProvider();
		}

		if (playerMoveMap_.GetEntryZone()._collision_object) {
			EventMoveMap eventObject = playerMoveMap_.GetEntryZone()._collision_object;

			if (eventObject.GetTriggerState().EventTrigger(playerMoveMap_.GetEntryZone())) {
				eventObject.GetEventSetFuncs()[eventObject.executeEventNum_](eventObject, this);

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
		else if (allSceneMgr.inputProvider_.SelectNovelWindowActive()) {
		}
	}

	public void SceneEnd() {
		//フロントスクリーンの親の除外
		AllSceneManager.GetInstance().GetPublicFrontScreen().gameObject.transform.parent = null;

		//フロントスクリーンを初期位置へ移動
		AllSceneManager.GetInstance().GetPublicFrontScreen().gameObject.transform.position = new Vector3(0, 0, -9.5f);
	}

	public GameObject GetGameObject() { return gameObject; }
}
