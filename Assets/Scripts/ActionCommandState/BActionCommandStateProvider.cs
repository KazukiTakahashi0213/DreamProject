using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BActionCommandStateProvider {
	public int state_ = 0;

	protected List<BActionCommandState> processStates_ = new List<BActionCommandState>();

	public int UpSelect() { return processStates_[state_].UpSelect(); }
	public int DownSelect() { return processStates_[state_].DownSelect(); }
	public int RightSelect() { return processStates_[state_].RightSelect(); }
	public int LeftSelect() { return processStates_[state_].LeftSelect(); }

	public int EnterSelect() { return processStates_[state_].EnterSelect(); }
}
