﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneState {
	Title,
	SaveDataSelect,
	Map,
	BugMenu,
	MonsterMenu,
	MonsterBattleMenu,
	MonsterMenuInfo,
	MonsterParameterCustom,
	Battle,
	Max
}

public class AllSceneManager : MonoBehaviour {
	//EntryPoint
	public AllSceneManager() {
		instance_ = this;
	}

	//init
	void Start() {
		//各シーンを生成し、非表示にする
		for (int i = 0; i < (int)SceneState.Max; ++i) {
			GameObject load = Resources.Load("Prefabs/Scenes/" + sceneStateString[i]) as GameObject;
			load = Instantiate(load, new Vector3(0, 0, 0), Quaternion.identity);

			sceneState[i] = load.GetComponent<ISceneManager>();
			load.SetActive(false);
		}

		//現在のシーンを表示にし、ISceneManagerを取得する
		sceneState[(int)nowSceneState_].GetGameObject().SetActive(true);

		//現在のシーンの開始処理
		sceneState[(int)nowSceneState_].SceneStart();
	}
	//MainLoop
	void Update() {
		//Escapeキーの振る舞い
		if (Input.GetKeyDown(KeyCode.Escape)) t13.UnityUtil.GameQuit();

		//現在のシーンの実装処理
		sceneState[(int)nowSceneState_].SceneUpdate();

		//SceneChangeが呼ばれていたら
		if(SceneActive_ == false) {
			//シーンの切り替え処理
			sceneChangeModeState.ChangeExecute(sceneState[(int)nowSceneState_], sceneState[(int)nextSceneState_]);

			//シーンのステートの変更
			nowSceneState_ = nextSceneState_;

			SceneActive_ = true;
		}
	}

	[SerializeField] AudioParts publicSystemAudioParts_ = null;
	
	[SerializeField] private SceneState nowSceneState_ = SceneState.Battle;
	public AudioParts GetPublicSystemAudioParts() { return publicSystemAudioParts_; }

	private string[] sceneStateString = new string[(int)SceneState.Max] {
		"TitleScene",
		"SaveDataSelectScene",
		"MapScene",
		"BugMenuScene",
		"MonsterMenuScene",
		"MonsterBattleMenuScene",
		"MonsterMenuInfoScene",
		"MonsterParameterCustomScene",
		"BattleScene",
	};

	private ISceneManager[] sceneState = new ISceneManager[(int)SceneState.Max];

	private SceneChangeModeState sceneChangeModeState = new SceneChangeModeState(SceneChangeMode.Change);
	private bool SceneActive_ = true;
	private SceneState nextSceneState_;

	public void SceneChange(SceneState nextScene, SceneChangeMode sceneChangeMode) {
		SceneActive_ = false;

		nextSceneState_ = nextScene;

		sceneChangeModeState.state_ = sceneChangeMode;
	}

	static private AllSceneManager instance_;
	static public AllSceneManager GetInstance() { return instance_; }
}
