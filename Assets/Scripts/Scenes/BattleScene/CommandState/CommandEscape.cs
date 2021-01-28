﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEscape : ICommandState {
	public ICommandState DownSelect(BattleManager mgr) {
		return this;
	}
	public ICommandState LeftSelect(BattleManager mgr) {
		mgr.CommandLeftCursorMove();
		return new CommandMonsterTrade();
	}
	public ICommandState RightSelect(BattleManager mgr) {
		return this;
	}
	public ICommandState UpSelect(BattleManager mgr) {
		mgr.CommandUpCursorMove();
		return new CommandItem();
	}

	public IProcessState Execute(BattleManager mgr) {
		//文字列の設定
		AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "いまは　にげられない！");
		AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute(mgr.GetEventContextUpdateTime());
		//イベントの最後
		AllEventManager.GetInstance().EventFinishSet();

		return mgr.nowProcessState();
	}
}
