using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterBattleMenuCommand {
	None
	, MonsterSelect
	, ActionSelect
	, Max
}

public class MonsterBattleMenuCommandState {
	public MonsterBattleMenuCommandState(MonsterBattleMenuCommand setState) {
		state_ = setState;
	}

	public MonsterBattleMenuCommand state_;

	//None
	static private MonsterBattleMenuCommand NoneUpSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneDownSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneRightSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneLeftSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneSelectEnter(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneSelectBack(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneSelectNovelWindowActive(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}

	//MonsterSelect
	static private MonsterBattleMenuCommand MonsterSelectUpSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();
		AllEventManager eventMgr = AllEventManager.GetInstance();

		manager.GetMagazineParts().UpRollMagazineParts();

		//操作の変更
		eventMgr.InputProviderChangeEventSet(new KeyBoardNormalInputProvider());

		manager.GetBulletParts().UpRollStatusInfoParts(manager.selectMonsterNumber_);

		manager.selectMonsterNumber_ += 1;
		manager.selectMonsterNumber_ %= PlayerBattleData.GetInstance().GetMonsterDatasLength();
		
		int referMonsterNumber = (manager.selectMonsterNumber_ + 2) % PlayerBattleData.GetInstance().GetMonsterDatasLength();

		manager.GetBulletParts().GetEventStatusInfosParts(manager.GetBulletParts().GetEventStatusInfosPartsSize() - 1).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas(referMonsterNumber));

		sceneMgr.inputProvider_ = new InactiveInputProvider();

		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectDownSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();
		AllEventManager eventMgr = AllEventManager.GetInstance();

		manager.GetMagazineParts().DownRollMagazineParts();

		//操作の変更
		eventMgr.InputProviderChangeEventSet(new KeyBoardNormalInputProvider());

		manager.GetBulletParts().DownRollStatusInfoParts(manager.selectMonsterNumber_);

		manager.selectMonsterNumber_ -= 1;
		manager.selectMonsterNumber_ = System.Math.Abs((manager.selectMonsterNumber_ + PlayerBattleData.GetInstance().GetMonsterDatasLength()) % PlayerBattleData.GetInstance().GetMonsterDatasLength());

		int referMonsterNumber = System.Math.Abs(((manager.selectMonsterNumber_ - 2) + PlayerBattleData.GetInstance().GetMonsterDatasLength()) % PlayerBattleData.GetInstance().GetMonsterDatasLength());

		manager.GetBulletParts().GetEventStatusInfosParts(0).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas(referMonsterNumber));

		sceneMgr.inputProvider_ = new InactiveInputProvider();

		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectRightSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectLeftSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectSelectEnter(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();
		AllEventManager eventMgr = AllEventManager.GetInstance();

		if (PlayerBattleData.GetInstance().GetMonsterDatas(manager.selectMonsterNumber_).battleActive_
			&& PlayerBattleData.GetInstance().GetMonsterDatas(manager.selectMonsterNumber_).tribesData_.monsterNumber_ != 0) {
			sceneMgr.inputProvider_ = new InactiveInputProvider();

			PlayerBattleData.GetInstance().changeMonsterNumber_ = manager.selectMonsterNumber_;
			PlayerBattleData.GetInstance().changeMonsterActive_ = true;

			//フェードアウト
			eventMgr.EventSpriteRendererSet(
				sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
				, null
				, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 255)
				);
			eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
			eventMgr.AllUpdateEventExecute(0.4f);

			//シーンの切り替え
			eventMgr.SceneChangeEventSet(SceneState.Battle, SceneChangeMode.Continue);
		}

		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectSelectBack(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();
		AllEventManager eventMgr = AllEventManager.GetInstance();

		if (PlayerBattleData.GetInstance().GetMonsterDatas(0).battleActive_) {
			sceneMgr.inputProvider_ = new InactiveInputProvider();

			PlayerBattleData.GetInstance().changeMonsterNumber_ = 0;
			PlayerBattleData.GetInstance().changeMonsterActive_ = true;

			//フェードアウト
			eventMgr.EventSpriteRendererSet(
				sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
				, null
				, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 255)
				);
			eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
			eventMgr.AllUpdateEventExecute(0.4f);

			//シーンの切り替え
			eventMgr.SceneChangeEventSet(SceneState.Battle, SceneChangeMode.Continue);
		}

		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectSelectNovelWindowActive(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}

	//ActionSelect
	static private MonsterBattleMenuCommand ActionSelectUpSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectDownSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectRightSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectLeftSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectSelectEnter(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectSelectBack(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectSelectNovelWindowActive(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager) {
		return mine.state_;
	}

	private delegate MonsterBattleMenuCommand SelectFunc(MonsterBattleMenuCommandState mine, MonsterBattleMenuManager manager);

	private SelectFunc[] upSelects_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneUpSelect,
		MonsterSelectUpSelect,
		ActionSelectUpSelect
	};
	public MonsterBattleMenuCommand UpSelect(MonsterBattleMenuManager manager) { return upSelects_[(int)state_](this, manager); }

	private SelectFunc[] downSelects_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneDownSelect,
		MonsterSelectDownSelect,
		ActionSelectDownSelect
	};
	public MonsterBattleMenuCommand DownSelect(MonsterBattleMenuManager manager) { return downSelects_[(int)state_](this, manager); }

	private SelectFunc[] rightSelects_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneRightSelect,
		MonsterSelectRightSelect,
		ActionSelectRightSelect
	};
	public MonsterBattleMenuCommand RightSelect(MonsterBattleMenuManager manager) { return rightSelects_[(int)state_](this, manager); }

	private SelectFunc[] leftSelects_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneLeftSelect,
		MonsterSelectLeftSelect,
		ActionSelectLeftSelect
	};
	public MonsterBattleMenuCommand LeftSelect(MonsterBattleMenuManager manager) { return leftSelects_[(int)state_](this, manager); }

	private SelectFunc[] selectEnters_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneSelectEnter,
		MonsterSelectSelectEnter,
		ActionSelectSelectEnter
	};
	public MonsterBattleMenuCommand SelectEnter(MonsterBattleMenuManager manager) { return selectEnters_[(int)state_](this, manager); }

	private SelectFunc[] selectBacks_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneSelectBack,
		MonsterSelectSelectBack,
		ActionSelectSelectBack
	};
	public MonsterBattleMenuCommand SelectBack(MonsterBattleMenuManager manager) { return selectBacks_[(int)state_](this, manager); }

	private SelectFunc[] selectNovelWindowActives_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneSelectNovelWindowActive,
		MonsterSelectSelectNovelWindowActive,
		ActionSelectSelectNovelWindowActive
	};
	public MonsterBattleMenuCommand SelectNovelWindowActive(MonsterBattleMenuManager manager) { return selectNovelWindowActives_[(int)state_](this, manager); }
}
