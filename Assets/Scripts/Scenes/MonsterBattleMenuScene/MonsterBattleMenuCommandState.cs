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
	static private MonsterBattleMenuCommand NoneUpSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneDownSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneRightSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneLeftSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneSelectEnter(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneSelectBack(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand NoneSelectNovelWindowActive(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}

	//MonsterSelect
	static private MonsterBattleMenuCommand MonsterSelectUpSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		manager.GetMagazineParts().UpRollMagazineParts();
		AllEventManager.GetInstance().EventFinishSet();
		manager.GetBulletParts().UpRollStatusInfoParts(manager.selectMonsterNumber_);

		manager.GetBulletParts().GetEventStatusInfosParts(manager.GetBulletParts().GetEventStatusInfosPartsSize() - 1).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas(manager.selectMonsterNumber_));

		manager.selectMonsterNumber_ -= 1;

		manager.selectMonsterNumber_ = System.Math.Abs((manager.selectMonsterNumber_ + 3) % 3);

		manager.inputProvider_ = new KeyBoardInactiveInputProvider();

		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectDownSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		manager.GetMagazineParts().DownRollMagazineParts();
		AllEventManager.GetInstance().EventFinishSet();
		manager.GetBulletParts().DownRollStatusInfoParts(manager.selectMonsterNumber_);

		manager.GetBulletParts().GetEventStatusInfosParts(0).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas(manager.selectMonsterNumber_));

		manager.selectMonsterNumber_ += 1;

		manager.selectMonsterNumber_ %= 3;

		manager.inputProvider_ = new KeyBoardInactiveInputProvider();

		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectRightSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectLeftSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectSelectEnter(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		if (PlayerBattleData.GetInstance().GetMonsterDatas(manager.selectMonsterNumber_).battleActive_) {
			PlayerBattleData.GetInstance().changeMonsterNumber_ = manager.selectMonsterNumber_;
			PlayerBattleData.GetInstance().changeMonsterActive_ = true;

			AllSceneManager.GetInstance().SceneChange(SceneState.Battle, SceneChangeMode.Continue);
		}

		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectSelectBack(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		if (PlayerBattleData.GetInstance().GetMonsterDatas(0).battleActive_) {
			PlayerBattleData.GetInstance().changeMonsterNumber_ = 0;
			PlayerBattleData.GetInstance().changeMonsterActive_ = true;

			AllSceneManager.GetInstance().SceneChange(SceneState.Battle, SceneChangeMode.Continue);
		}

		return mine.state_;
	}
	static private MonsterBattleMenuCommand MonsterSelectSelectNovelWindowActive(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}

	//ActionSelect
	static private MonsterBattleMenuCommand ActionSelectUpSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectDownSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectRightSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectLeftSelect(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectSelectEnter(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectSelectBack(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}
	static private MonsterBattleMenuCommand ActionSelectSelectNovelWindowActive(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager) {
		return mine.state_;
	}

	private delegate MonsterBattleMenuCommand SelectFunc(MonsterBattleMenuCommandState mine, MonsterBattleMenuScene manager);

	private SelectFunc[] upSelects_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneUpSelect,
		MonsterSelectUpSelect,
		ActionSelectUpSelect
	};
	public MonsterBattleMenuCommand UpSelect(MonsterBattleMenuScene manager) { return upSelects_[(int)state_](this, manager); }

	private SelectFunc[] downSelects_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneDownSelect,
		MonsterSelectDownSelect,
		ActionSelectDownSelect
	};
	public MonsterBattleMenuCommand DownSelect(MonsterBattleMenuScene manager) { return downSelects_[(int)state_](this, manager); }

	private SelectFunc[] rightSelects_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneRightSelect,
		MonsterSelectRightSelect,
		ActionSelectRightSelect
	};
	public MonsterBattleMenuCommand RightSelect(MonsterBattleMenuScene manager) { return rightSelects_[(int)state_](this, manager); }

	private SelectFunc[] leftSelects_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneLeftSelect,
		MonsterSelectLeftSelect,
		ActionSelectLeftSelect
	};
	public MonsterBattleMenuCommand LeftSelect(MonsterBattleMenuScene manager) { return leftSelects_[(int)state_](this, manager); }

	private SelectFunc[] selectEnters_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneSelectEnter,
		MonsterSelectSelectEnter,
		ActionSelectSelectEnter
	};
	public MonsterBattleMenuCommand SelectEnter(MonsterBattleMenuScene manager) { return selectEnters_[(int)state_](this, manager); }

	private SelectFunc[] selectBacks_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneSelectBack,
		MonsterSelectSelectBack,
		ActionSelectSelectBack
	};
	public MonsterBattleMenuCommand SelectBack(MonsterBattleMenuScene manager) { return selectBacks_[(int)state_](this, manager); }

	private SelectFunc[] selectNovelWindowActives_ = new SelectFunc[(int)MonsterBattleMenuCommand.Max] {
		NoneSelectNovelWindowActive,
		MonsterSelectSelectNovelWindowActive,
		ActionSelectSelectNovelWindowActive
	};
	public MonsterBattleMenuCommand SelectNovelWindowActive(MonsterBattleMenuScene manager) { return selectNovelWindowActives_[(int)state_](this, manager); }
}
