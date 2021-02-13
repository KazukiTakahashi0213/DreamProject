using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMenuManager : MonoBehaviour, ISceneManager {
	public void SceneStart() {
		AllEventManager eventMgr = AllEventManager.GetInstance();
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();

		//フェードイン
		eventMgr.EventSpriteRendererSet(
			sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
			, null
			, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 0)
			);
		eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
		eventMgr.AllUpdateEventExecute(0.4f);

		//イベントの最後
		//操作の変更
		eventMgr.InputProviderChangeEventSet(new KeyBoardNormalTriggerInputProvider());
	}

	public void SceneUpdate() {
		AllEventManager.GetInstance().EventUpdate();
	}

	public void SceneEnd() {

	}

	public GameObject GetGameObject() { return gameObject; }
}
