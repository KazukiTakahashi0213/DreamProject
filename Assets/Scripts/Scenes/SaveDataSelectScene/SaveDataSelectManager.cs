using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataSelectManager : MonoBehaviour, ISceneManager {

	[SerializeField] Text _start_text = null;
	[SerializeField] Text _continue_text = null;
	[SerializeField] GameObject _move_cursor = null;

	enum SELECT_STATUS {START,CONTINUE, }

	SELECT_STATUS _select_num = SELECT_STATUS.START;

	public void SceneStart() {
		AllSceneManager.GetInstance().inputProvider_ = new KeyBoardInactiveInputProvider();
	}

	public void SceneUpdate() {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();

		if (AllEventManager.GetInstance().EventUpdate()) {
			allSceneMgr.inputProvider_ = new KeyBoardNormalTriggerInputProvider();
		}

		if (allSceneMgr.inputProvider_.UpSelect())
		{
			_select_num = SELECT_STATUS.START;
			_move_cursor.transform.position = _start_text.transform.position;
		}

		if (allSceneMgr.inputProvider_.DownSelect())
		{
			_select_num = SELECT_STATUS.CONTINUE;
			_move_cursor.transform.position = _continue_text.transform.position;
		}

		if (allSceneMgr.inputProvider_.SelectEnter()) {
			if (_select_num == SELECT_STATUS.START)
			{
				Debug.Log("はじめから");

				AllEventManager eventMgr = AllEventManager.GetInstance();
				AllSceneManager sceneMgr = AllSceneManager.GetInstance();

				allSceneMgr.inputProvider_ = new KeyBoardInactiveInputProvider();

				//フェードアウト
				eventMgr.EventSpriteRendererSet(
					sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
					, null
					, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 255)
					);
				eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
				eventMgr.AllUpdateEventExecute(0.4f);

				//シーンの切り替え
				eventMgr.SceneChangeEventSet(SceneState.Map, SceneChangeMode.Change);

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
			if (_select_num == SELECT_STATUS.CONTINUE) {
				Debug.Log("つづきから");
			}
		}
	}

	public void SceneEnd() {

	}

	public GameObject GetGameObject() { return gameObject; }
}
