using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataSelectManager : MonoBehaviour, ISceneManager {

	[SerializeField] Text _start_text = null;
	[SerializeField] Text _continue_text = null;
	[SerializeField] GameObject _move_cursor = null;

	IInputProvider input = new KeyBoardNormalInputProvider();

	enum SELECT_STATUS {START,CONTINUE, }

	SELECT_STATUS _select_num = SELECT_STATUS.START;

	public void SceneStart() {
		
	}

	public void SceneUpdate() {
		if (input.UpSelect())
		{
			_select_num = SELECT_STATUS.START;
			_move_cursor.transform.position = _start_text.transform.position;
		}

		if (input.DownSelect())
		{
			_select_num = SELECT_STATUS.CONTINUE;
			_move_cursor.transform.position = _continue_text.transform.position;
		}

		if (input.SelectEnter()) {
			if (_select_num == SELECT_STATUS.START)
			{
				Debug.Log("はじめから");
			}
			if (_select_num == SELECT_STATUS.CONTINUE) {
				Debug.Log("つづきから");
			}
		}
	}

	public void SceneEnd() {

	}

	public GameObject GetGameObject() { return gameObject; }
}
