using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMenuSceneProcessNone : BBugMenuSceneProcessState {
	public override BugMenuSceneProcess Update(BugMenuManager bugMenuManager) {
		return BugMenuSceneProcess.None;
	}
}
