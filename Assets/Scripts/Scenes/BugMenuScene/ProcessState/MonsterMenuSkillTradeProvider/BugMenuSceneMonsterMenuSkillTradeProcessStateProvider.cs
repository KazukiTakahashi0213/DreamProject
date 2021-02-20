using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMenuSceneMonsterMenuSkillTradeProcessStateProvider : BBugMenuSceneProcessStateProvider {
	public BugMenuSceneMonsterMenuSkillTradeProcessStateProvider() {
		states_.Add(new BugMenuSceneMonsterMenuSkillTradeProcessNone());
		states_.Add(new BugMenuSceneMonsterMenuSkillTradeProcessSkillSelect());
	}

	public override void init() {

	}
}
