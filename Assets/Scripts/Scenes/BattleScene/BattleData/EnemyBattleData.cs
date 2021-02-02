using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleData {
	public void monsterAdd(IMonsterData addMonster) {
		if (haveMonsterSize_ == MONSTER_MAX_SIZE) return;

		monsterDatas_[haveMonsterSize_] = addMonster;
		haveMonsterSize_ += 1;
		battleActiveMonsterSize_ += 1;
	}

	//get
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

	//戦えるモンスターの数
	private int battleActiveMonsterSize_ = 0;

	//交換するか否かのフラグ
	public bool changeMonsterActive_ = false;
	//交換する手持ちの番号
	public int changeMonsterNumber_ = 0;

	//共通のdp
	public int dreamPoint_ = 0;
	//パワーアップするか否かのフラグ
	public bool dreamSyncronize_ = false;

	//倒れた時の処理
	public void MonsterDownEventSet(BattleManager manager) {
		battleActiveMonsterSize_ -= 1;

		dreamPoint_ += 45;

		//戦闘のモンスターをダウンさせる
		monsterDatas_[0].battleActive_ = false;

		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(1.0f);

		//エネミーの画像の非表示
		AllEventManager.GetInstance().UpdateGameObjectSet(manager.GetEnemyMonsterParts().GetEventGameObject());
		AllEventManager.GetInstance().UpdateGameObjectsActiveSetExecute(false);

		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

		//エネミーのステータスインフォの退場
		AllEventManager.GetInstance().UpdateGameObjectSet(manager.GetEnemyStatusInfoParts().GetEventGameObject(), new Vector3(-13.5f, manager.GetEnemyStatusInfoParts().transform.position.y, manager.GetEnemyStatusInfoParts().transform.position.z));
		AllEventManager.GetInstance().UpdateGameObjectUpdateExecuteSet(UpdateGameObjectEventManagerExecute.PosMove);
		AllEventManager.GetInstance().AllUpdateEventExecute(0.2f);

		//文字列の処理
		AllEventManager.GetInstance().EventTextSet(manager.GetNovelWindowParts().GetEventText(), "あいての　" + monsterDatas_[0].uniqueName_ + "は　たおれた！");
		AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute(manager.GetEventContextUpdateTime());

		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

		if(battleActiveMonsterSize_ == 0) {
			//文字列の処理
			AllEventManager.GetInstance().EventTextSet(
				manager.GetNovelWindowParts().GetEventText()
				, EnemyTrainerData.GetInstance().job() + "の　" + EnemyTrainerData.GetInstance().name() + "\n"
				+ "との　しょうぶに　かった！");
			AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
			AllEventManager.GetInstance().AllUpdateEventExecute(manager.GetEventContextUpdateTime());

			//Enterキー
			AllEventManager.GetInstance().EventWaitEnterSelectSet();

			//エネミーの入場
			AllEventManager.GetInstance().UpdateGameObjectSet(manager.GetEnemyParts().GetEventGameObject(), new Vector3(3.5f, manager.GetEnemyParts().transform.position.y, manager.GetEnemyParts().transform.position.z));
			AllEventManager.GetInstance().UpdateGameObjectUpdateExecuteSet(UpdateGameObjectEventManagerExecute.PosMove);
			AllEventManager.GetInstance().AllUpdateEventExecute(0.8f);

			//Enterキー
			AllEventManager.GetInstance().EventWaitEnterSelectSet();

			AllEventManager.GetInstance().EventFinishSet();

			return;
		}

		//タイプ相性の測定
		int[] typeSimillarResult = new int[3] { 0, 0, 0 };
		int[] monsterNumbers = new int[3] { 0, 1, 2 };


		for (int i = 0; i < monsterDatas_.Length; ++i) {
			//戦えたら
			if(monsterDatas_[i].battleActive_
				&& monsterDatas_[i].tribesData_.monsterNumber_ != 0) {
				{
					int simillarResult = PlayerBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarCheckerForValue(monsterDatas_[i].tribesData_.firstElement_);

					if (simillarResult == 0) typeSimillarResult[i] += 3;
					else if (simillarResult == 1) typeSimillarResult[i] += 1;
					else if (simillarResult == 2) typeSimillarResult[i] += 0;
					else if (simillarResult == 3) typeSimillarResult[i] += 2;
				}
				{
					int simillarResult = PlayerBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarCheckerForValue(monsterDatas_[i].tribesData_.secondElement_);

					if (simillarResult == 0) typeSimillarResult[i] += 3;
					else if (simillarResult == 1) typeSimillarResult[i] += 1;
					else if (simillarResult == 2) typeSimillarResult[i] += 0;
					else if (simillarResult == 3) typeSimillarResult[i] += 2;
				}
			}
		}

		t13.Utility.SimpleHiSort2Index(typeSimillarResult, monsterNumbers);

		//モンスターデータの入れ替え
		IMonsterData temp = monsterDatas_[0];
		monsterDatas_[0] = monsterDatas_[monsterNumbers[0]];
		monsterDatas_[monsterNumbers[0]] = temp;

		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(1.0f);

		//文字列の処理
		AllEventManager.GetInstance().EventTextSet(
			manager.GetNovelWindowParts().GetEventText(), EnemyTrainerData.GetInstance().name() + "は\n" 
			+ monsterDatas_[0].uniqueName_ + "を　くりだした！"
			);
		AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute(manager.GetEventContextUpdateTime());

		//画像の設定
		List<Sprite> sprites = new List<Sprite>();
		sprites.Add(monsterDatas_[0].tribesData_.frontTex_);
		AllEventManager.GetInstance().EventSpriteRendererSet(manager.GetEnemyMonsterParts().GetEventMonsterSprite(), sprites, new Color32());
		AllEventManager.GetInstance().EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.SpriteSet);
		AllEventManager.GetInstance().AllUpdateEventExecute(manager.GetEventContextUpdateTime());

		//名前とレベルをTextに反映
		string monsterViewName = t13.Utility.StringFullSpaceBackTamp(monsterDatas_[0].uniqueName_, 6);
		AllEventManager.GetInstance().EventTextSet(manager.GetEnemyStatusInfoParts().GetBaseParts().GetInfoEventText(), monsterViewName + "　　Lｖ" + t13.Utility.HarfSizeForFullSize(monsterDatas_[0].level_.ToString()));
		AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute(manager.GetEventContextUpdateTime());

		//HPをTextに反映
		//HPゲージの調整
		float hpGaugeFillAmount = t13.Utility.ValueForPercentage(monsterDatas_[0].RealHitPoint(), monsterDatas_[0].nowHitPoint_, 1);
		AllEventManager.GetInstance().HpGaugePartsSet(manager.GetEnemyStatusInfoParts().GetFrameParts().GetHpGaugeParts(), hpGaugeFillAmount, monsterDatas_[0]);
		AllEventManager.GetInstance().HpGaugePartsUpdateExecuteSet(HpGaugePartsEventManagerExecute.GaugeUpdate);
		AllEventManager.GetInstance().AllUpdateEventExecute();

		//状態異常の反映
		monsterDatas_[0].battleData_.AbnormalSetStatusInfoParts(manager.GetEnemyStatusInfoParts());

		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

		//エネミーの表示
		AllEventManager.GetInstance().UpdateGameObjectSet(manager.GetEnemyMonsterParts().GetEventGameObject());
		AllEventManager.GetInstance().UpdateGameObjectsActiveSetExecute(true);

		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

		//エネミーのステータスインフォの入場
		AllEventManager.GetInstance().UpdateGameObjectSet(manager.GetEnemyStatusInfoParts().GetEventGameObject(), new Vector3(-3.5f, manager.GetEnemyStatusInfoParts().transform.position.y, manager.GetEnemyStatusInfoParts().transform.position.z));
		AllEventManager.GetInstance().UpdateGameObjectUpdateExecuteSet(UpdateGameObjectEventManagerExecute.PosMove);
		AllEventManager.GetInstance().AllUpdateEventExecute(0.2f);

		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());
	}

	//交換処理
	public void MonsterChangeEventSet(BattleManager manager) {
		//モンスターの変更が行われていたら
		if (changeMonsterNumber_ > 0) {
			IMonsterData md = monsterDatas_[changeMonsterNumber_];

			AllEventManager.GetInstance().EventTextSet(manager.GetNovelWindowParts().GetEventText(), EnemyTrainerData.GetInstance().name() + "は\n"
				+ monsterDatas_[0].uniqueName_ + "を　ひっこめた！");
			AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
			AllEventManager.GetInstance().AllUpdateEventExecute(manager.GetEventContextUpdateTime());

			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			AllEventManager.GetInstance().UpdateGameObjectSet(manager.GetEnemyMonsterParts().GetEventGameObject());
			AllEventManager.GetInstance().UpdateGameObjectsActiveSetExecute(false);

			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			AllEventManager.GetInstance().UpdateGameObjectSet(manager.GetEnemyStatusInfoParts().GetEventGameObject(), new Vector3(-13.5f, manager.GetEnemyStatusInfoParts().transform.position.y, manager.GetEnemyStatusInfoParts().transform.position.z));
			AllEventManager.GetInstance().UpdateGameObjectUpdateExecuteSet(UpdateGameObjectEventManagerExecute.PosMove);
			AllEventManager.GetInstance().AllUpdateEventExecute(0.2f);

			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			//モンスターデータの入れ替え
			IMonsterData temp = monsterDatas_[0];
			monsterDatas_[0] = monsterDatas_[changeMonsterNumber_];
			monsterDatas_[changeMonsterNumber_] = temp;

			AllEventManager.GetInstance().EventTextSet(manager.GetNovelWindowParts().GetEventText(), EnemyTrainerData.GetInstance().name() + "は\n"
				+ md.uniqueName_ + "を　くりだした！");
			AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
			AllEventManager.GetInstance().AllUpdateEventExecute(manager.GetEventContextUpdateTime());

			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			//画像の設定
			List<Sprite> sprites = new List<Sprite>();
			sprites.Add(monsterDatas_[0].tribesData_.frontTex_);
			AllEventManager.GetInstance().EventSpriteRendererSet(manager.GetEnemyMonsterParts().GetEventMonsterSprite(), sprites, new Color32());
			AllEventManager.GetInstance().EventSpriteRenderersUpdateExecuteSet(EventSpriteRendererEventManagerExecute.SpriteSet);
			AllEventManager.GetInstance().AllUpdateEventExecute(manager.GetEventContextUpdateTime());

			//名前とレベルをTextに反映
			string monsterViewName = t13.Utility.StringFullSpaceBackTamp(monsterDatas_[0].uniqueName_, 6);
			AllEventManager.GetInstance().EventTextSet(manager.GetEnemyStatusInfoParts().GetBaseParts().GetInfoEventText(), monsterViewName + "　　Lｖ" + t13.Utility.HarfSizeForFullSize(monsterDatas_[0].level_.ToString()));
			AllEventManager.GetInstance().EventTextsUpdateExecuteSet(EventTextEventManagerExecute.CharaUpdate);
			AllEventManager.GetInstance().AllUpdateEventExecute(manager.GetEventContextUpdateTime());

			//HPをTextに反映
			//HPゲージの調整
			float hpGaugeFillAmount = t13.Utility.ValueForPercentage(monsterDatas_[0].RealHitPoint(), monsterDatas_[0].nowHitPoint_, 1);
			AllEventManager.GetInstance().HpGaugePartsSet(manager.GetEnemyStatusInfoParts().GetFrameParts().GetHpGaugeParts(), hpGaugeFillAmount, monsterDatas_[0]);
			AllEventManager.GetInstance().HpGaugePartsUpdateExecuteSet(HpGaugePartsEventManagerExecute.GaugeUpdate);
			AllEventManager.GetInstance().AllUpdateEventExecute();

			//状態異常の反映
			md.battleData_.AbnormalSetStatusInfoParts(manager.GetEnemyStatusInfoParts());

			//エネミーの表示
			AllEventManager.GetInstance().UpdateGameObjectSet(manager.GetEnemyMonsterParts().GetEventGameObject());
			AllEventManager.GetInstance().UpdateGameObjectsActiveSetExecute(true);

			//ウェイト
			AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

			//エネミーのステータスインフォの入場
			AllEventManager.GetInstance().UpdateGameObjectSet(manager.GetEnemyStatusInfoParts().GetEventGameObject(), new Vector3(-3.5f, manager.GetEnemyStatusInfoParts().transform.position.y, manager.GetEnemyStatusInfoParts().transform.position.z));
			AllEventManager.GetInstance().UpdateGameObjectUpdateExecuteSet(UpdateGameObjectEventManagerExecute.PosMove);
			AllEventManager.GetInstance().AllUpdateEventExecute(0.2f);
		}

		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(manager.GetEventWaitTime());

		changeMonsterNumber_ = 0;
	}

	//シングルトン
	private EnemyBattleData() { }

	static private EnemyBattleData instance_ = null;
	static public EnemyBattleData GetInstance() {
		if (instance_ != null) return instance_;

		instance_ = new EnemyBattleData();
		return instance_;
	}
}
