using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBattleMenuScene : MonoBehaviour, ISceneManager {
	public void SceneStart() {
		//依存性注入
		inputProvider_ = new KeyBoardNormalInputProvider();
		nowCommandState_ = new MonsterBattleMenuCommandState(MonsterBattleMenuCommand.MonsterSelect);

		selectMonsterNumber_ = 0;

		//BulletPartsの初期化
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(0).gameObject, new Vector3(0.9f, 3.5f, 5));
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(1).gameObject, new Vector3(0.9f, 3.5f, 5));
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(2).gameObject, new Vector3(0.9f, 2.0f, 5));
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(3).gameObject, new Vector3(0.9f, 0.5f, 5));
		t13.UnityUtil.ObjectPosMove(bulletParts_.GetEventStatusInfosParts(4).gameObject, new Vector3(0.9f, 0.5f, 5));

		//MagazinePartsの初期化
		t13.UnityUtil.ObjectRotMove(magazineParts_.gameObject, Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));

		t13.UnityUtil.ObjectRotMove(magazineParts_.GetMonsterSDsParts(0).gameObject, Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));
		t13.UnityUtil.ObjectRotMove(magazineParts_.GetMonsterSDsParts(1).gameObject, Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));
		t13.UnityUtil.ObjectRotMove(magazineParts_.GetMonsterSDsParts(2).gameObject, Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));

		//StatusInfosPartsの色の変更
		for (int i = 0;i < bulletParts_.GetEventStatusInfosPartsSize() / 2 + (bulletParts_.GetEventStatusInfosPartsSize() % 2); ++i) {
			if (i == 0) {
				bulletParts_.GetEventStatusInfosParts(i).ProcessStateColorUpdateExecute(0, t13.TimeFluctProcess.Liner, new Color32(0, 0, 0, 0));
				bulletParts_.GetEventStatusInfosParts(bulletParts_.GetEventStatusInfosPartsSize() - 1 - i).ProcessStateColorUpdateExecute(0, t13.TimeFluctProcess.Liner, new Color32(0, 0, 0, 0));
			}
			else {
				bulletParts_.GetEventStatusInfosParts(i).ProcessStateColorUpdateExecute(0, t13.TimeFluctProcess.Liner, new Color32(0, 0, 0, (byte)(255 / ((i % 2) + 1))));
				bulletParts_.GetEventStatusInfosParts(bulletParts_.GetEventStatusInfosPartsSize() - 1 - i).ProcessStateColorUpdateExecute(0, t13.TimeFluctProcess.Liner, new Color32(0, 0, 0, (byte)(255 / ((i % 2) + 1))));
			}
		}

		//StatusInfosPartsのモンスター情報の変更
		for (int i = 0; i < PlayerBattleData.GetInstance().GetMonsterDatasLength(); ++i) {
			{
				if (i == 0) {
					bulletParts_.GetEventStatusInfosParts(2).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas(i));
				}
				else {
					bulletParts_.GetEventStatusInfosParts(i + 2).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas((i % 2) + 1));
					bulletParts_.GetEventStatusInfosParts(-i + 2).MonsterStatusInfoSet(PlayerBattleData.GetInstance().GetMonsterDatas(((i + 1) % 2) + 1));
				}
			}
		}

		//MagazinePartsのSDの画像の変更
		for(int i = 0;i < magazineParts_.GetMonsterSDsPartsCount(); ++i) {
			magazineParts_.GetMonsterSDsParts(i).GetMonsterSDSpriteRenderer().sprite = PlayerBattleData.GetInstance().GetMonsterDatas(i).tribesData_.SDTex_;
		}
	}

	public void SceneUpdate() {
		if (AllEventManager.GetInstance().EventUpdate()) {
			inputProvider_ = new KeyBoardNormalInputProvider();
		}

		if (inputProvider_.UpSelect()) {
			nowCommandState_.state_ = nowCommandState_.UpSelect(this);
		}
		else if (inputProvider_.DownSelect()) {
			nowCommandState_.state_ = nowCommandState_.DownSelect(this);
		}
		else if (inputProvider_.RightSelect()) {
			nowCommandState_.state_ = nowCommandState_.RightSelect(this);
		}
		else if (inputProvider_.LeftSelect()) {
			nowCommandState_.state_ = nowCommandState_.LeftSelect(this);
		}
		else if (inputProvider_.SelectEnter()) {
			nowCommandState_.state_ = nowCommandState_.SelectEnter(this);
		}
		else if (inputProvider_.SelectBack()) {
			nowCommandState_.state_ = nowCommandState_.SelectBack(this);
		}
		else if (inputProvider_.SelectNovelWindowActive()) {
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
	public IInputProvider inputProvider_ = new KeyBoardNormalInputProvider();
	private MonsterBattleMenuCommandState nowCommandState_ = new MonsterBattleMenuCommandState(MonsterBattleMenuCommand.MonsterSelect);

	public int selectMonsterNumber_ = 0;
}
