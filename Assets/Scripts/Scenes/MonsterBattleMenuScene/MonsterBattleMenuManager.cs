using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBattleMenuManager : MonoBehaviour, ISceneManager {
	public void SceneStart() {
		AllSceneManager sceneMgr = AllSceneManager.GetInstance();
		AllEventManager eventMgr = AllEventManager.GetInstance();

		//依存性注入
		nowCommandState_ = new MonsterBattleMenuCommandState(MonsterBattleMenuCommand.MonsterSelect);

		selectMonsterNumber_ = 0;

		//BulletPartsの初期化
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(0).gameObject, new Vector3(0.9f, 2.5f, 5));
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(1).gameObject, new Vector3(0.9f, 2.5f, 5));
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(2).gameObject, new Vector3(0.9f, 1.0f, 5));
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(3).gameObject, new Vector3(0.9f, -0.5f, 5));
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(4).gameObject, new Vector3(0.9f, -0.5f, 5));

		//MagazinePartsの初期化
		t13.UnityUtil.ObjectRotMove(magazineParts_.gameObject, Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));

		for(int i = 0;i < magazineParts_.GetMonsterSDsPartsCount(); ++i) {
			t13.UnityUtil.ObjectRotMove(magazineParts_.GetMonsterSDsParts(i).gameObject, Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));
		}

		//StatusInfosPartsの色の変更
		for (int i = 0;i < bulletParts_.GetEventStatusInfosPartsSize() / 2 + (bulletParts_.GetEventStatusInfosPartsSize() % 2); ++i) {
			if (i == 0) {
				bulletParts_.GetEventStatusInfosParts(i).ProcessStateAllColorUpdateExecute(0, t13.TimeFluctProcess.Liner, new Color32(0, 0, 0, 0));
				bulletParts_.GetEventStatusInfosParts(bulletParts_.GetEventStatusInfosPartsSize() - 1 - i).ProcessStateAllColorUpdateExecute(0, t13.TimeFluctProcess.Liner, new Color32(0, 0, 0, 0));
			}
			else {
				bulletParts_.GetEventStatusInfosParts(i).ProcessStateColorUpdateExecute(0, t13.TimeFluctProcess.Liner, new Color32(0, 0, 0, (byte)(255 / ((i % 2) + 1))));
				bulletParts_.GetEventStatusInfosParts(bulletParts_.GetEventStatusInfosPartsSize() - 1 - i).ProcessStateColorUpdateExecute(0, t13.TimeFluctProcess.Liner, new Color32(0, 0, 0, (byte)(255 / ((i % 2) + 1))));
			}
		}

		//StatusInfosPartsのモンスター情報の変更
		for (int i = 0; i < PlayerBattleData.GetInstance().GetMonsterDatasLength() / 2; ++i) {
			if (i == 0) {
				bulletParts_.GetEventStatusInfosParts(bulletParts_.GetEventStatusInfosPartsSize() / 2).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas(i));
			}
			else {
				bulletParts_.GetEventStatusInfosParts(i + bulletParts_.GetEventStatusInfosPartsSize() / 2).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas(i));
				bulletParts_.GetEventStatusInfosParts(-i + bulletParts_.GetEventStatusInfosPartsSize() / 2).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas(PlayerBattleData.GetInstance().GetMonsterDatasLength() - i));
			}
		}

		//MagazinePartsのSDの画像の変更
		for(int i = 0;i < magazineParts_.GetMonsterSDsPartsCount(); ++i) {
			magazineParts_.GetMonsterSDsParts(i).GetMonsterSDEventSpriteRenderer().GetSpriteRenderer().sprite = PlayerBattleData.GetInstance().GetMonsterDatas(i).tribesData_.SDTex_;
		}

		//フェードイン
		eventMgr.EventSpriteRendererSet(
			sceneMgr.GetPublicFrontScreen().GetEventScreenSprite()
			, null
			, new Color(sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.r, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.g, sceneMgr.GetPublicFrontScreen().GetEventScreenSprite().GetSpriteRenderer().color.b, 0)
			);
		eventMgr.EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.ChangeColor);
		eventMgr.AllUpdateEventExecute(0.4f);

		//操作の変更
		eventMgr.InputProviderChangeEventSet(new KeyBoardNormalInputProvider());
	}

	public void SceneUpdate() {
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();

		AllEventManager.GetInstance().EventUpdate();

		if (allSceneMgr.inputProvider_.UpSelect()) {
			nowCommandState_.state_ = nowCommandState_.UpSelect(this);
		}
		else if (allSceneMgr.inputProvider_.DownSelect()) {
			nowCommandState_.state_ = nowCommandState_.DownSelect(this);
		}
		else if (allSceneMgr.inputProvider_.RightSelect()) {
			nowCommandState_.state_ = nowCommandState_.RightSelect(this);
		}
		else if (allSceneMgr.inputProvider_.LeftSelect()) {
			nowCommandState_.state_ = nowCommandState_.LeftSelect(this);
		}
		else if (allSceneMgr.inputProvider_.SelectEnter()) {
			nowCommandState_.state_ = nowCommandState_.SelectEnter(this);
		}
		else if (allSceneMgr.inputProvider_.SelectBack()) {
			nowCommandState_.state_ = nowCommandState_.SelectBack(this);
		}
		else if (allSceneMgr.inputProvider_.SelectNovelWindowActive()) {
			nowCommandState_.state_ = nowCommandState_.SelectNovelWindowActive(this);
		}
	}

	public void SceneEnd() {
		magazineParts_.GetEventGameObject().addEulerVec3_ = new Vector3(0, 0, 0);
	}

	public GameObject GetGameObject() { return gameObject; }

	[SerializeField] private MagazineParts magazineParts_ = null;
	[SerializeField] private BulletParts bulletParts_ = null;

	public MagazineParts GetMagazineParts() { return magazineParts_; }
	public BulletParts GetBulletParts() { return bulletParts_; }

	//ステート
	private MonsterBattleMenuCommandState nowCommandState_ = new MonsterBattleMenuCommandState(MonsterBattleMenuCommand.MonsterSelect);

	public int selectMonsterNumber_ = 0;
}
