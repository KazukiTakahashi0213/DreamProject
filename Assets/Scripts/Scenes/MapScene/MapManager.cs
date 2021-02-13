using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, ISceneManager {
	MapSceneProcessProvider processProvider_ = new MapSceneProcessProvider();

	[SerializeField] private Camera mainCamera_ = null;
	[SerializeField] private PlayerMoveMap playerMoveMap_ = null;
	[SerializeField] private NovelWindowParts novelWindowParts_ = null;
	[SerializeField] private List<EventMoveMap> sceneObjectMoveMaps_ = new List<EventMoveMap>();
	[SerializeField] private CommandParts commandParts_ = null;

	public MapSceneProcessProvider GetProcessProvider() { return processProvider_; }

	public NovelWindowParts GetNovelWindowParts() { return novelWindowParts_; }
	public PlayerMoveMap GetPlayerMoveMap() { return playerMoveMap_; }
	public List<EventMoveMap> GetSceneObjectMoveMaps() { return sceneObjectMoveMaps_; }
	public CommandParts GetCommandParts() { return commandParts_; }

	public void SceneStart() {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		AllEventManager allEventMgr = AllEventManager.GetInstance();

		//依存性注入
		processProvider_.state_ = MapSceneProcess.PlayerMove;

		//フロントスクリーンをカメラの子にする
		AllSceneManager.GetInstance().GetPublicFrontScreen().gameObject.transform.SetParent(mainCamera_.transform);

		//フェードイン
		allEventMgr.EventSpriteRendererSet(
			allSceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
			, null
			, new Color(allSceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, allSceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, allSceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 0)
			);
		allEventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
		allEventMgr.AllUpdateEventExecute(0.4f);

		//イベントの最後
		//制御の変更
		allEventMgr.InputProviderChangeEventSet(new KeyBoardNormalInputProvider());
	}

	public void SceneUpdate() {
		processProvider_.state_ = processProvider_.Update(this);
	}

	public void SceneEnd() {
		//フロントスクリーンの親の除外
		AllSceneManager.GetInstance().GetPublicFrontScreen().gameObject.transform.parent = null;

		//フロントスクリーンを初期位置へ移動
		AllSceneManager.GetInstance().GetPublicFrontScreen().gameObject.transform.position = new Vector3(0, 0, -9.5f);
	}

	public GameObject GetGameObject() { return gameObject; }
}
