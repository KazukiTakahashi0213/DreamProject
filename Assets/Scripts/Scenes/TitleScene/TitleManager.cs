using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour, ISceneManager {

	IInputProvider input = new KeyBoardNormalInputProvider();

	public void SceneStart() {

	}

	public void SceneUpdate() {
		if (input.SelectEnter()) {
			AllSceneManager.GetInstance().SceneChange(SceneState.SaveDataSelect, SceneChangeMode.Change);
		}
	}

	public void SceneEnd() {

	}

	public void OnSaveScene() { }

	public GameObject GetGameObject() { return gameObject; }
}
