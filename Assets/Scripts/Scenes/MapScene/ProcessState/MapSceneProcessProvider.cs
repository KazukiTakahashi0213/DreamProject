using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapSceneProcess {
	None
	, PlayerMove
	, MenuSelect
	, Max
}

public class MapSceneProcessProvider {
	public MapSceneProcessProvider() {
		baseMapSceneProcessStates_.Add(new MapSceneProcessNone());
		baseMapSceneProcessStates_.Add(new MapSceneProcessPlayerMove());
		baseMapSceneProcessStates_.Add(new MapSceneProcessMenuSelect());
	}

	public MapSceneProcess state_ = MapSceneProcess.None;

	private List<BaseMapSceneProcessState> baseMapSceneProcessStates_ = new List<BaseMapSceneProcessState>();

	public MapSceneProcess Update(MapManager mapManager) { return baseMapSceneProcessStates_[(int)state_].Update(mapManager); }
}
