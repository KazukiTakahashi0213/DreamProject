using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HumanMoveMapCharacter {
	None
	, TutorialDocter
	, Max
}

public class HumanMoveMapCharacterState {
	public HumanMoveMapCharacterState(HumanMoveMapCharacter setState) {
		state_ = setState;
	}

	public HumanMoveMapCharacter state_;

	//None
	private static void NoneStartEventSet(HumanMoveMapCharacterState mine, MapManager mapManager) {

	}
	private static void NoneWinEndEventSet(HumanMoveMapCharacterState mine, MapManager mapManager) {

	}
	private static void NoneLoseEndEventSet(HumanMoveMapCharacterState mine, MapManager mapManager) {

	}

	//TutorialDocter
	private static void TutorialDocterStartEventSet(HumanMoveMapCharacterState mine, MapManager mapManager) {

	}
	private static void TutorialDocterWinEndEventSet(HumanMoveMapCharacterState mine, MapManager mapManager) {

	}
	private static void TutorialDocterLoseEndEventSet(HumanMoveMapCharacterState mine, MapManager mapManager) {

	}

	private string[] jobs_ = new string[(int)HumanMoveMapCharacter.Max] {
		"ーー"
		, "はかせ"
	};
	public string GetJob() { return jobs_[(int)state_]; }

	private string[] names_ = new string[(int)HumanMoveMapCharacter.Max] {
		"ーー"
		, "はかせ"
	};
	public string GetName() { return names_[(int)state_]; }
}
