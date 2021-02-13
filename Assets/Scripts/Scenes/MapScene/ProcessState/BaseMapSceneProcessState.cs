using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMapSceneProcessState {
	public virtual MapSceneProcess Update(MapManager mapManager) { return mapManager.GetProcessProvider().state_; }
}
