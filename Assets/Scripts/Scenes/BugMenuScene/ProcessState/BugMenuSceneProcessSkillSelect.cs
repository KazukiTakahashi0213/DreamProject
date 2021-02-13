using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMenuSceneProcessSkillSelect : BBugMenuSceneProcessState {
	//選択肢制御
	private int selectNum_ = 0;

	public override BugMenuSceneProcess Update(BugMenuManager bugMenuManager) {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		AllEventManager allEventMgr = AllEventManager.GetInstance();
		PlayerTrainerData playerTrainerData = PlayerTrainerData.GetInstance();

		allEventMgr.EventUpdate();

		if (allSceneMgr.inputProvider_.UpSelect()) {
			if (selectNum_ > 0) {
				t13.UnityUtil.ObjectPosAdd(bugMenuManager.GetCommandParts().GetCursorParts().gameObject, new Vector3(0, 1.0f, 0));

				--selectNum_;

				//技の情報の反映
				string playPointContext = t13.Utility.HarfSizeForFullSize(playerTrainerData.GetSkillDatas(selectNum_).nowPlayPoint_.ToString()) + "／" + t13.Utility.HarfSizeForFullSize(playerTrainerData.GetSkillDatas(selectNum_).playPoint_.ToString());

				bugMenuManager.GetInfoFrameParts().GetTexts(0).text =
					"PP　　　　" + playPointContext + "\n"
					+ "わざタイプ／" + playerTrainerData.GetSkillDatas(selectNum_).elementType_.GetName();

				bugMenuManager.GetInfoFrameParts().GetTexts(1).text = playerTrainerData.GetSkillDatas(selectNum_).effectInfo_;
			}
		}
		else if (allSceneMgr.inputProvider_.DownSelect()) {
			if (selectNum_ < playerTrainerData.GetSkillDatasCount() - 1) {
				t13.UnityUtil.ObjectPosAdd(bugMenuManager.GetCommandParts().GetCursorParts().gameObject, new Vector3(0, -1.0f, 0));

				++selectNum_;

				//技の情報の反映
				string playPointContext = t13.Utility.HarfSizeForFullSize(playerTrainerData.GetSkillDatas(selectNum_).nowPlayPoint_.ToString()) + "／" + t13.Utility.HarfSizeForFullSize(playerTrainerData.GetSkillDatas(selectNum_).playPoint_.ToString());

				bugMenuManager.GetInfoFrameParts().GetTexts(0).text =
					"PP　　　　" + playPointContext + "\n"
					+ "わざタイプ／" + playerTrainerData.GetSkillDatas(selectNum_).elementType_.GetName();

				bugMenuManager.GetInfoFrameParts().GetTexts(1).text = playerTrainerData.GetSkillDatas(selectNum_).effectInfo_;
			}
		}
		else if (allSceneMgr.inputProvider_.RightSelect()) {
		}
		else if (allSceneMgr.inputProvider_.LeftSelect()) {
		}
		else if (allSceneMgr.inputProvider_.SelectEnter()) {
		}
		else if (allSceneMgr.inputProvider_.SelectBack()) {
		}
		else if (allSceneMgr.inputProvider_.SelectNovelWindowActive()) {
		}
		else if (allSceneMgr.inputProvider_.SelectMenu()) {
		}

		return bugMenuManager.GetProcessProvider().state_;
	}
}
