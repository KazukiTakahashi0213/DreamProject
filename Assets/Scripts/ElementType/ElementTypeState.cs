using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType {
	None
	, Normal
	, Fire
	, Water
	, Tree
	, Max
}

public class ElementTypeState {
	public ElementTypeState(ElementType setState) {
		state_ = setState;
	}

	public ElementType state_;

	//None

	//Normal

	//Fire

	//Water

	//Tree

	private string[] names = new string[(int)ElementType.Max] {
		"None",
		"ノーマル",
		"ほのお",
		"みず",
		"くさ",
	};
	public string GetName() { return names[(int)state_]; }
}
