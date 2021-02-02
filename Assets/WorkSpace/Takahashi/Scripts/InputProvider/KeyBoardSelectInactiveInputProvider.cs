using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardSelectInactiveInputProvider : IInputProvider {
	public bool UpSelect() {
		return false;
	}
	public bool DownSelect() {
		return false;
	}
	public bool RightSelect() {
		return false;
	}
	public bool LeftSelect() {
		return false;
	}
	public bool SelectEnter() {
		return Input.GetKeyDown(KeyCode.Z);
	}
	public bool SelectBack() {
		return Input.GetKeyDown(KeyCode.X);
	}
	public bool SelectNovelWindowActive() {
		return Input.GetKeyDown(KeyCode.W);
	}
}
