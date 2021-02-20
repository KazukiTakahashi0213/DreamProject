using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMenuSceneNormalProcessStateProvider : BBugMenuSceneProcessStateProvider {
	public BugMenuSceneNormalProcessStateProvider() {
		states_.Add(new BugMenuSceneNormalProcessNone());
		states_.Add(new BugMenuSceneNormalProcessSkillSelect());
	}

	public override void init() {

	}
}
