using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMenuManager : MonoBehaviour, ISceneManager {
	private BugMenuSceneProcessProvider processProvider_ = new BugMenuSceneProcessProvider();
	public BugMenuSceneProcessProvider GetProcessProvider() { return processProvider_; }

	//シーン上のオブジェクト
	[SerializeField] CommandParts commandParts_ = null;
	[SerializeField] InfoFrameParts infoFrameParts_ = null;
	public CommandParts GetCommandParts() { return commandParts_; }
	public InfoFrameParts GetInfoFrameParts() { return infoFrameParts_; }

	public void SceneStart() {
		AllEventManager eventMgr = AllEventManager.GetInstance();
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();
		PlayerTrainerData playerTrainerData = PlayerTrainerData.GetInstance();

		//依存性注入
		processProvider_.state_ = BugMenuSceneProcess.SkillSelect;

		//初期化
		for (int i = 0; i < commandParts_.GetCommandWindowTextsCount(); ++i) {
			commandParts_.GetCommandWindowTexts(i).text = "ーー";
		}

		//技の名前の反映
		for (int i = 0;i < playerTrainerData.GetSkillDatasCount(); ++i) {
			commandParts_.GetCommandWindowTexts(i).text = playerTrainerData.GetSkillDatas(i).skillName_;
		}

		//技の情報の反映
		string playPointContext = t13.Utility.HarfSizeForFullSize(playerTrainerData.GetSkillDatas(0).nowPlayPoint_.ToString()) + "／" + t13.Utility.HarfSizeForFullSize(playerTrainerData.GetSkillDatas(0).playPoint_.ToString());

		infoFrameParts_.GetTexts(0).text =
			"PP　　　　" + playPointContext + "\n"
			+ "わざタイプ／" + playerTrainerData.GetSkillDatas(0).elementType_.GetName();

		infoFrameParts_.GetTexts(1).text = playerTrainerData.GetSkillDatas(0).effectInfo_;


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
		processProvider_.state_ = processProvider_.Update(this);
	}

	public void SceneEnd() {

	}

	public GameObject GetGameObject() { return gameObject; }
}
