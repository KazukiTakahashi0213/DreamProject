using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapSceneMenuSelectCommandSelect {
	None
	, Monster
	, Skill
	, Report
	, Back
	, Max
}

public class MapSceneMenuSelectCommandSelectProvider {
	public MapSceneMenuSelectCommandSelectProvider() {
		baseMapSceneMenuSelectCommandSelectStates_.Add(new MapSceneMenuSelectCommandSelectMonster());
		baseMapSceneMenuSelectCommandSelectStates_.Add(new MapSceneMenuSelectCommandSelectSkill());
		baseMapSceneMenuSelectCommandSelectStates_.Add(new MapSceneMenuSelectCommandSelectReport());
		baseMapSceneMenuSelectCommandSelectStates_.Add(new MapSceneMenuSelectCommandSelectBack());
	}

	public MapSceneMenuSelectCommandSelect state_ = MapSceneMenuSelectCommandSelect.None;

	private List<BaseMapSceneMenuSelectCommandSelectState> baseMapSceneMenuSelectCommandSelectStates_ = new List<BaseMapSceneMenuSelectCommandSelectState>();

	public void SelectEnter(MapManager mapManager) { baseMapSceneMenuSelectCommandSelectStates_[(int)state_].SelectEnter(mapManager); }
}
