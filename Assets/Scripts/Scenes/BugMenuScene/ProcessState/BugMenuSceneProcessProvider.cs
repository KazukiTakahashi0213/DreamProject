using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BugMenuSceneProcess {
    None
    , SkillSelect
    , Max
}

public class BugMenuSceneProcessProvider {
	public BugMenuSceneProcessProvider() {
		processStates_.Add(new BugMenuSceneProcessNone());
		processStates_.Add(new BugMenuSceneProcessSkillSelect());
	}

	public BugMenuSceneProcess state_ = BugMenuSceneProcess.None;

	private List<BBugMenuSceneProcessState> processStates_ = new List<BBugMenuSceneProcessState>();

	public BugMenuSceneProcess Update(BugMenuManager bugMenuManager) { return processStates_[(int)state_].Update(bugMenuManager); }
}
