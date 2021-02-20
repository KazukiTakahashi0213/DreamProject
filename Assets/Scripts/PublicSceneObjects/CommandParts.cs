using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandParts : MonoBehaviour {
	[SerializeField] private SpriteRenderer commandWindowSprite_ = null;
	[SerializeField] private List<Text> commandWindowTexts_ = null;
	[SerializeField] private UpdateGameObject eventGameObject_ = null;
	[SerializeField] private CursorParts cursorParts_ = null;

	public SpriteRenderer GetCommandWindowSprite() { return commandWindowSprite_; }
	public Text GetCommandWindowTexts(int value) { return commandWindowTexts_[value]; }
	public int GetCommandWindowTextsCount() { return commandWindowTexts_.Count; }
	public UpdateGameObject GetEventGameObject() { return eventGameObject_; }
	public CursorParts GetCursorParts() { return cursorParts_; }

	private int selectNumber_ = 0;
	public int GetSelectNumber() { return selectNumber_; }
	public void CommandSelect(int addSelectNumber, Vector3 addCursorPos) {
		selectNumber_ += addSelectNumber;

		//カーソルの移動
		t13.UnityUtil.ObjectPosAdd(cursorParts_.gameObject, addCursorPos);
	}
	public void SelectReset(Vector3 startCursorPos) {
		selectNumber_ = 0;

		cursorParts_.transform.localPosition = startCursorPos;
	}
}
