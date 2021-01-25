using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandItem : ICommandState {
	public ICommandState DownSelect(BattleManager mgr) {
		mgr.CommandDownCursorMove();
		return new CommandEscape();
	}
	public ICommandState LeftSelect(BattleManager mgr) {
		mgr.CommandLeftCursorMove();
		return new CommandAttack();
	}
	public ICommandState RightSelect(BattleManager mgr) {
		return this;
	}
	public ICommandState UpSelect(BattleManager mgr) {
		return this;
	}

	public IProcessState Execute(BattleManager mgr) {
		//文字列の設定
		AllEventManager.GetInstance().EventTextSet(mgr.GetNovelWindowParts().GetEventText(), "いまは　つかえない！");
		AllEventManager.GetInstance().EventTextsUpdateExecute(0.4f);
		//イベントの最後
		AllEventManager.GetInstance().EventFinishSet();

		return mgr.nowProcessState();
	}
}
