using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, ISceneManager {
	[SerializeField] Camera mainCamera_ = null;
	//[SerializeField] PlayerMoveMap playerMoveMap_ = null;

	public void SceneStart() {
		//フロントスクリーンをカメラの子にする
		AllSceneManager.GetInstance().GetPublicFrontScreen().gameObject.transform.SetParent(mainCamera_.transform);
	}

	public void SceneUpdate() {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();

		if (AllEventManager.GetInstance().EventUpdate()) {
			allSceneMgr.inputProvider_ = new KeyBoardNormalInputProvider();
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
