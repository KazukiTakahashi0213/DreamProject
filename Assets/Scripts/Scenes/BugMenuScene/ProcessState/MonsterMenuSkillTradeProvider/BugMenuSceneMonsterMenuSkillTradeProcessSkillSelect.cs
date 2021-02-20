using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMenuSceneMonsterMenuSkillTradeProcessSkillSelect : BBugMenuSceneProcessState {
	//選択肢制御
	private int skillSelectNum_ = 0;
	private int cursorSelectNum_ = 0;

	public override BugMenuSceneProcess Update(BugMenuManager bugMenuManager) {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		AllEventManager allEventMgr = AllEventManager.GetInstance();
		PlayerTrainerData playerTrainerData = PlayerTrainerData.GetInstance();

		allEventMgr.EventUpdate();

		if (allSceneMgr.inputProvider_.UpSelect()) {
			//表示する技がまだあったら
			if (skillSelectNum_ > 0) {
				--skillSelectNum_;

				//一番上からスクロールさせようとしたら
				if (cursorSelectNum_ == 0) {
					//技の名前を更新する
					int startNum = skillSelectNum_ % (bugMenuManager.GetCommandParts().GetCommandWindowTextsCount() - 1);
					for (int i = startNum; i < bugMenuManager.GetCommandParts().GetCommandWindowTextsCount(); ++i) {
						bugMenuManager.GetCommandParts().GetCommandWindowTexts(i - startNum).text = playerTrainerData.GetSkillDatas(i).skillName_;
					}

					//upCursorの非表示
					if (skillSelectNum_ == 0) {
						bugMenuManager.GetUpCursor().gameObject.SetActive(false);
					}
					//downCursorの表示
					else {
						bugMenuManager.GetDownCursor().gameObject.SetActive(true);
					}
				}

				//技の情報の反映
				bugMenuManager.GetInfoFrameParts().SkillInfoReflect(playerTrainerData.GetSkillDatas(skillSelectNum_));
			}
			//カーソルが上に動かせたら
			if (cursorSelectNum_ > 0) {
				--cursorSelectNum_;

				t13.UnityUtil.ObjectPosAdd(bugMenuManager.GetCommandParts().GetCursorParts().gameObject, new Vector3(0, 1.0f, 0));
			}
		}
		else if (allSceneMgr.inputProvider_.DownSelect()) {
			//表示する技がまだあったら
			if (skillSelectNum_ < playerTrainerData.GetHaveSkillSize() - 1) {
				++skillSelectNum_;

				//一番下からスクロールさせようとしたら
				if (cursorSelectNum_ == bugMenuManager.GetCommandParts().GetCommandWindowTextsCount() - 1) {
					//技の名前を更新する
					int startNum = skillSelectNum_ % (bugMenuManager.GetCommandParts().GetCommandWindowTextsCount() - 1);
					for (int i = startNum; i < skillSelectNum_ + 1; ++i) {
						bugMenuManager.GetCommandParts().GetCommandWindowTexts(i - startNum).text = playerTrainerData.GetSkillDatas(i).skillName_;
					}

					//downCursorの非表示
					if (skillSelectNum_ == playerTrainerData.GetSkillDatasCount() - 1) {
						bugMenuManager.GetDownCursor().gameObject.SetActive(false);
					}
					//upCursorの非表示
					else {
						bugMenuManager.GetUpCursor().gameObject.SetActive(true);
					}
				}

				//技の情報の反映
				bugMenuManager.GetInfoFrameParts().SkillInfoReflect(playerTrainerData.GetSkillDatas(skillSelectNum_));
			}
			//カーソルが下に動かせたら
			if (cursorSelectNum_ < playerTrainerData.GetHaveSkillSize() - 1) {
				++cursorSelectNum_;

				t13.UnityUtil.ObjectPosAdd(bugMenuManager.GetCommandParts().GetCursorParts().gameObject, new Vector3(0, -1.0f, 0));
			}
		}
		else if (allSceneMgr.inputProvider_.RightSelect()) {
		}
		else if (allSceneMgr.inputProvider_.LeftSelect()) {
		}
		else if (allSceneMgr.inputProvider_.SelectEnter()) {
			AllEventManager eventMgr = AllEventManager.GetInstance();
			AllSceneManager sceneMgr = AllSceneManager.GetInstance();

			MonsterMenuManager.skillTradeActive_ = true;
			MonsterMenuManager.skillTradeSkillData_ = new SkillData(playerTrainerData.GetSkillDatas(skillSelectNum_).skillNumber_);

			sceneMgr.inputProvider_ = new InactiveInputProvider();

			//フェードアウト
			eventMgr.EventSpriteRendererSet(
				sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
				, null
				, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 255)
				);
			eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
			eventMgr.AllUpdateEventExecute(0.4f);

			//シーンの切り替え
			MonsterMenuManager.SetProcessStateProvider(new MonsterMenuSceneNormalProcessStateProvider());
			eventMgr.SceneChangeEventSet(SceneState.MonsterMenu, SceneChangeMode.Continue);
		}
		else if (allSceneMgr.inputProvider_.SelectBack()) {
			AllEventManager eventMgr = AllEventManager.GetInstance();
			AllSceneManager sceneMgr = AllSceneManager.GetInstance();

			MonsterMenuManager.skillTradeActive_ = true;
			MonsterMenuManager.skillTradeSkillData_ = null;

			sceneMgr.inputProvider_ = new InactiveInputProvider();

			//フェードアウト
			eventMgr.EventSpriteRendererSet(
				sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
				, null
				, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 255)
				);
			eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
			eventMgr.AllUpdateEventExecute(0.4f);

			//シーンの切り替え
			MonsterMenuManager.SetProcessStateProvider(new MonsterMenuSceneNormalProcessStateProvider());
			eventMgr.SceneChangeEventSet(SceneState.MonsterMenu, SceneChangeMode.Continue);
		}
		else if (allSceneMgr.inputProvider_.SelectNovelWindowActive()) {
		}
		else if (allSceneMgr.inputProvider_.SelectMenu()) {
		}

		return bugMenuManager.GetProcessProvider().state_;
	}
}
