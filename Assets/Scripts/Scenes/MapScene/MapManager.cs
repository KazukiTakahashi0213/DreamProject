using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, ISceneManager {
	MapSceneProcessProvider processProvider_ = new MapSceneProcessProvider();
	public MapSceneProcess eventBackProcess_ = MapSceneProcess.None;

	private bool firstStart_ = true;

	[SerializeField] private MapData mapData_ = null;
	[SerializeField] private Camera mainCamera_ = null;
	[SerializeField] private PlayerMoveMap playerMoveMap_ = null;
	[SerializeField] private NovelWindowParts novelWindowParts_ = null;
	[SerializeField] private CommandParts commandParts_ = null;
	[SerializeField] private List<FloorObjectsParts> floorObjects_ = null;

	public MapSceneProcessProvider GetProcessProvider() { return processProvider_; }

	public MapData GetMapData() { return mapData_; }
	public Camera GetMainCamera() { return mainCamera_; }
	public NovelWindowParts GetNovelWindowParts() { return novelWindowParts_; }
	public PlayerMoveMap GetPlayerMoveMap() { return playerMoveMap_; }
	public CommandParts GetCommandParts() { return commandParts_; }
	public FloorObjectsParts GetFloorObjects() { return floorObjects_[PlayerTrainerData.GetInstance().nowMapFloor_]; }

	public void SceneStart() {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		AllEventManager allEventMgr = AllEventManager.GetInstance();
		PlayerTrainerData playerData = PlayerTrainerData.GetInstance();

		//初めての処理だったら
		if (firstStart_) {
			firstStart_ = false;

			//オブジェクトの配置
			for (int i = 0; i < floorObjects_.Count; ++i) {
				floorObjects_[i].gameObject.SetActive(true);
			}

			//依存性注入
			processProvider_.state_ = MapSceneProcess.PlayerMove;

			//ウェイト
			allEventMgr.EventWaitSet(0.5f);

			//シーンの切り替え
			allEventMgr.SceneChangeEventSet(SceneState.Map, SceneChangeMode.Change);
		}
		else {
			//依存性注入
			processProvider_.state_ = MapSceneProcess.PlayerMove;

			//主人公の移動の変更
			playerMoveMap_.is_move = true;

			//BGMの再生
			AllSceneManager.GetInstance().GetPublicAudioParts().GetAudioSource().clip = ResourcesSoundsLoader.GetInstance().GetSounds("BGM/MapScene/Dreamers_Map");
			AllSceneManager.GetInstance().GetPublicAudioParts().GetAudioSource().Play();

			//オブジェクトの配置
			for (int i = 0; i < floorObjects_.Count; ++i) {
				floorObjects_[i].gameObject.SetActive(false);
			}
			floorObjects_[playerData.nowMapFloor_].gameObject.SetActive(true);

			//オブジェクトの座標の初期化
			for (int i = 0; i < floorObjects_[PlayerTrainerData.GetInstance().nowMapFloor_].GetEventMoveMapsCount(); ++i) {
				floorObjects_[PlayerTrainerData.GetInstance().nowMapFloor_].GetEventMoveMaps(i).ResetNowPos();
			}
			playerMoveMap_.ResetNowPos();

			//マップデータに反映
			mapData_.MapDataReset();

			//フェードイン
			allEventMgr.EventSpriteRendererSet(
				allSceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
				, null
				, new Color(allSceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, allSceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, allSceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 0)
				);
			allEventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
			allEventMgr.AllUpdateEventExecute(0.4f);

			//イベントの最後
			//制御の変更
			allEventMgr.InputProviderChangeEventSet(new KeyBoardNormalInputProvider());
		}
	}

	public void SceneUpdate() {
		processProvider_.state_ = processProvider_.Update(this);
	}

	public void SceneEnd() {

	}

	public GameObject GetGameObject() { return gameObject; }
}
