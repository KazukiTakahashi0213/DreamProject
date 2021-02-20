using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMenuSceneNormalProcessSkillSelect : BBugMenuSceneProcessState {
	//選択肢制御
	private int skillSelectNum_ = 0;

	public override BugMenuSceneProcess Update(BugMenuManager bugMenuManager) {
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();
		AllEventManager eventMgr = AllEventManager.GetInstance();
		PlayerTrainerData playerData = PlayerTrainerData.GetInstance();

		eventMgr.EventUpdate();

		if (sceneMgr.inputProvider_.UpSelect()) {
			//表示する技がまだあったら
			if (skillSelectNum_ > 0) {
				--skillSelectNum_;

				//一番上からスクロールさせようとしたら
				if (bugMenuManager.GetCommandParts().GetSelectNumber() == 0) {
					//技の名前を更新する
					int startNum = skillSelectNum_ % (bugMenuManager.GetCommandParts().GetCommandWindowTextsCount() - 1);
					for (int i = startNum; i < bugMenuManager.GetCommandParts().GetCommandWindowTextsCount(); ++i) {
						bugMenuManager.GetCommandParts().GetCommandWindowTexts(i - startNum).text = playerData.GetSkillDatas(i).skillName_;
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
				bugMenuManager.GetInfoFrameParts().SkillInfoReflect(playerData.GetSkillDatas(skillSelectNum_));
			}
			//カーソルが上に動かせたら
			if (bugMenuManager.GetCommandParts().GetSelectNumber() > 0) {
				bugMenuManager.GetCommandParts().CommandSelect(-1, new Vector3(0, 1.0f, 0));
			}
		}
		else if (sceneMgr.inputProvider_.DownSelect()) {
			//表示する技がまだあったら
			if (skillSelectNum_ < playerData.GetHaveSkillSize()-1) {
				++skillSelectNum_;

				//一番下からスクロールさせようとしたら
				if(bugMenuManager.GetCommandParts().GetSelectNumber() == bugMenuManager.GetCommandParts().GetCommandWindowTextsCount()-1) {
					//技の名前を更新する
					int startNum = skillSelectNum_ % (bugMenuManager.GetCommandParts().GetCommandWindowTextsCount()-1);
					for (int i = startNum; i < skillSelectNum_+1; ++i) {
						bugMenuManager.GetCommandParts().GetCommandWindowTexts(i - startNum).text = playerData.GetSkillDatas(i).skillName_;
					}

					//downCursorの非表示
					if(skillSelectNum_ == playerData.GetSkillDatasCount() - 1) {
						bugMenuManager.GetDownCursor().gameObject.SetActive(false);
					}
					//upCursorの非表示
					else {
						bugMenuManager.GetUpCursor().gameObject.SetActive(true);
					}
				}

				//技の情報の反映
				bugMenuManager.GetInfoFrameParts().SkillInfoReflect(playerData.GetSkillDatas(skillSelectNum_));
			}
			//カーソルが下に動かせたら
			if (bugMenuManager.GetCommandParts().GetSelectNumber() < bugMenuManager.GetCommandParts().GetCommandWindowTextsCount()-1) {
				bugMenuManager.GetCommandParts().CommandSelect(1, new Vector3(0, -1.0f, 0));
			}
		}
		else if (sceneMgr.inputProvider_.RightSelect()) {
		}
		else if (sceneMgr.inputProvider_.LeftSelect()) {
		}
		else if (sceneMgr.inputProvider_.SelectEnter()) {
		}
		else if (sceneMgr.inputProvider_.SelectBack()) {
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
			eventMgr.SceneChangeEventSet(SceneState.Map, SceneChangeMode.Change);
		}
		else if (sceneMgr.inputProvider_.SelectNovelWindowActive()) {
		}
		else if (sceneMgr.inputProvider_.SelectMenu()) {
		}

		return bugMenuManager.GetProcessProvider().state_;
	}
}
