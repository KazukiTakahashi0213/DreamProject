using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleData {
	public void monsterAdd(IMonsterData addMonster) {
		if (haveMonsterSize_ == MONSTER_MAX_SIZE) return;

		monsterDatas_[haveMonsterSize_] = addMonster;
		haveMonsterSize_ += 1;
	}

	public IMonsterData GetMonsterDatas(int num) { return monsterDatas_[num]; }
	public int GetMonsterDatasLength() { return monsterDatas_.Length; }
	public int GetHaveMonsterSize() { return haveMonsterSize_; }

	//手持ちのモンスターのデータ
	private const int MONSTER_MAX_SIZE = 3;
	private int haveMonsterSize_ = 0;
	private IMonsterData[] monsterDatas_ = new IMonsterData[MONSTER_MAX_SIZE] {
		new MonsterData(new MonsterTribesData(0), 0, 50)
		, new MonsterData(new MonsterTribesData(0), 0, 50)
		, new MonsterData(new MonsterTribesData(0), 0, 50)
	};

	public bool changeMonsterActive_ = false;
	//交換する手持ちの番号
	public int changeMonsterNumber_ = 0;
	//交換処理
	public void MonsterChangeEventSet(BattleManager manager) {
		if(changeMonsterNumber_ > 0) {
			IMonsterData md = monsterDatas_[changeMonsterNumber_];

			AllEventManager.GetInstance().EventTextSet(manager.GetNovelWindowParts().GetEventText(), monsterDatas_[0].uniqueName_ + "\n"
				+ "もどれ！");
			AllEventManager.GetInstance().EventTextsUpdateExecute(manager.GetEventContextUpdateTime());

			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			AllEventManager.GetInstance().EventGameObjectSet(manager.GetPlayerMonsterParts().GetEventGameObject());
			AllEventManager.GetInstance().EventGameObjectsActiveSetExecute(false);

			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			AllEventManager.GetInstance().EventGameObjectSet(manager.GetPlayerStatusInfoParts().GetEventGameObject(), 13.5f);
			AllEventManager.GetInstance().EventGameObjectsPosMoveXExecute(0.2f);

			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			AllEventManager.GetInstance().EventTextSet(manager.GetNovelWindowParts().GetEventText(), "ゆけ！　" + md.uniqueName_ + "！");
			AllEventManager.GetInstance().EventTextsUpdateExecute(manager.GetEventContextUpdateTime());

			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			{
				//画像の設定
				List<Sprite> sprites = new List<Sprite>();
				sprites.Add(md.tribesData_.backTex_);
				//manager.GetPlayerMonsterParts().GetMonsterSprite().sprite = md.tribesData_.backTex_;
				AllEventManager.GetInstance().EventSpriteRendererSet(manager.GetPlayerMonsterParts().GetEventMonsterSprite(), sprites);
				AllEventManager.GetInstance().EventSpriteRenderersSetSpriteExecute();
			}
			//名前とレベルをTextに反映
			string monsterViewName = t13.Utility.StringFullSpaceBackTamp(md.uniqueName_, 6);
			AllEventManager.GetInstance().EventTextSet(manager.GetPlayerStatusInfoParts().GetBaseParts().GetInfoEventText(), monsterViewName + "　　Lｖ" + t13.Utility.HarfSizeForFullSize(md.level_.ToString()));
			AllEventManager.GetInstance().EventTextsUpdateExecute();

			//HPをTextに反映
			//HPゲージの調整
			float hpGaugeFillAmount = t13.Utility.ValueForPercentage(md.RealHitPoint(), md.nowHitPoint_, 1);
			AllEventManager.GetInstance().EventHpGaugeSet(manager.GetPlayerStatusInfoParts().GetFrameParts().GetEventHpGaugeParts(), hpGaugeFillAmount, md);
			AllEventManager.GetInstance().EventHpGaugesUpdateExecute();

			//技をTextに反映
			for (int i = 0; i < 4; ++i) {
				AllEventManager.GetInstance().EventTextSet(manager.GetNovelWindowParts().GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i), "　" + t13.Utility.StringFullSpaceBackTamp(md.GetSkillDatas(i).skillNname_, 7));
			}
			AllEventManager.GetInstance().EventTextsUpdateExecute();

			//文字の色の変更
			for (int i = 0; i < 4; ++i) {
				if (EnemyBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarChecker(md.GetSkillDatas(i).elementType_) > 1.0f) {
					manager.GetNovelWindowParts().GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i).GetText().color = new Color32(207, 52, 112, 255);
				}
				else if (EnemyBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarChecker(md.GetSkillDatas(i).elementType_) < 1.0f
					&& EnemyBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarChecker(md.GetSkillDatas(i).elementType_) > 0) {
					manager.GetNovelWindowParts().GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i).GetText().color = new Color32(52, 130, 207, 255);
				}
				else if (EnemyBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarChecker(md.GetSkillDatas(i).elementType_) < 0.1f) {
					manager.GetNovelWindowParts().GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i).GetText().color = new Color32(195, 195, 195, 255);
				}
				else {
					manager.GetNovelWindowParts().GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i).GetText().color = new Color32(50, 50, 50, 255);
				}
			}

			IMonsterData temp = monsterDatas_[0];
			monsterDatas_[0] = monsterDatas_[changeMonsterNumber_];
			monsterDatas_[changeMonsterNumber_] = temp;

			AllEventManager.GetInstance().EventGameObjectSet(manager.GetPlayerMonsterParts().GetEventGameObject());
			AllEventManager.GetInstance().EventGameObjectsActiveSetExecute(true);

			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			AllEventManager.GetInstance().EventGameObjectSet(manager.GetPlayerStatusInfoParts().GetEventGameObject(), 4.0f);
			AllEventManager.GetInstance().EventGameObjectsPosMoveXExecute(0.2f);
		}

		AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

		AllEventManager.GetInstance().EventGameObjectSet(manager.GetCursorParts().GetEventGameObject());
		AllEventManager.GetInstance().EventGameObjectSet(manager.GetNovelWindowParts().GetCommandParts().GetEventGameObject());
		AllEventManager.GetInstance().EventGameObjectsActiveSetExecute(true);

		AllEventManager.GetInstance().EventTextSet(manager.GetNovelWindowParts().GetEventText(), monsterDatas_[0].uniqueName_ + "は　どうする？");
		AllEventManager.GetInstance().EventTextsUpdateExecute();

		AllEventManager.GetInstance().EventFinishSet();

		manager.ActiveUiCommand();
		manager.InactiveUiCommand();

		manager.SetInputProvider(new KeyBoardInactiveInputProvider());

		changeMonsterActive_ = false;
	}

	//シングルトン
	private PlayerBattleData() { }

	static private PlayerBattleData instance_ = null;
	static public PlayerBattleData GetInstance() {
		if (instance_ != null) return instance_;

		instance_ = new PlayerBattleData();
		return instance_;
	}
}
