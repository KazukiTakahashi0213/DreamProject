using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour, ISceneManager {
	public void SceneStart() {
		AllSceneManager.GetInstance().inputProvider_ = new KeyBoardNormalTriggerInputProvider();
	}

	public void SceneUpdate() {
		AllEventManager.GetInstance().EventUpdate();

		if (AllSceneManager.GetInstance().inputProvider_.SelectEnter()) {
			AllEventManager eventMgr = AllEventManager.GetInstance();
			AllSceneManager sceneMgr = AllSceneManager.GetInstance();

			AllSceneManager.GetInstance().inputProvider_ = new KeyBoardInactiveInputProvider();

			//フェードアウト
			eventMgr.EventSpriteRendererSet(
				sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
				, null
				, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 255)
				) ;
			eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
			eventMgr.AllUpdateEventExecute(0.4f);

			//シーンの切り替え
			eventMgr.SceneChangeEventSet(SceneState.SaveDataSelect, SceneChangeMode.Change);

			//フェードイン
			eventMgr.EventSpriteRendererSet(
				sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
				, null
				, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 0)
				);
			eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
			eventMgr.AllUpdateEventExecute(0.4f);

			//イベントの最後
			eventMgr.EventFinishSet();
		}
	}

	public void SceneEnd() {

	}

	public void OnSaveScene() { }

	public GameObject GetGameObject() { return gameObject; }
}
