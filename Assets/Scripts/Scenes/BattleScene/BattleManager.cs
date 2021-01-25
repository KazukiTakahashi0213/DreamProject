using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour, ISceneManager {
	//EntryPoint
	//Init
	public void SceneStart() {
		//依存性注入
		inputProvider_ = new KeyBoardNormalInputProvider();

		//初期Stateを設定
		nowProcessState_ = new OpeningProcess();
		nowCommandState_ = new CommandAttack();
		nowAttackCommandState_ = new AttackCommand0();

		//BGMの再生
		AllSceneManager.GetInstance().GetPublicSystemAudioParts().GetAudioSource().clip = Resources.Load("Sounds/BGM/BattleScene/Dreamers_Academy_Battle") as AudioClip;
		AllSceneManager.GetInstance().GetPublicSystemAudioParts().GetAudioSource().Play();

		//エネミーモンスターの読み込み
		{
			//外部でする処理
			//データの生成
			IMonsterData md = new MonsterData(new MonsterTribesData(1), 0, 50);
			//技の取得
			md.SkillAdd(new SkillData(1));
			md.SkillAdd(new SkillData(2));
			md.SkillAdd(new SkillData(3));
			md.SkillAdd(new SkillData(4));
			//エネミーの手持ちに追加
			EnemyBattleData.GetInstance().monsterAdd(md);

			//画像の設定
			enemyMonsterParts_.GetMonsterSprite().sprite = md.tribesData_.frontTex_;

			//名前とレベルをTextに反映
			string monsterViewName = t13.Utility.StringFullSpaceBackTamp(md.uniqueName_, 6);
			enemyStatusInfoParts_.GetBaseParts().GetInfoEventText().GetText().text = monsterViewName + "　　Lｖ" + t13.Utility.HarfSizeForFullSize(md.level_.ToString());

			//HPゲージの調整
			enemyStatusInfoParts_.GetFrameParts().GetHpGaugeParts().GetGauge().fillAmount = t13.Utility.ValueForPercentage(md.RealHitPoint(), md.nowHitPoint_, 1);
		}

		//プレイヤーモンスターの読み込み
		{
			//外部でする処理
			//データの生成
			IMonsterData md = new MonsterData(new MonsterTribesData(2), 0, 50);
			//技の取得
			md.SkillAdd(new SkillData(1));
			md.SkillAdd(new SkillData(2));
			md.SkillAdd(new SkillData(3));
			md.SkillAdd(new SkillData(4));
			//プレイヤーの手持ちに追加
			PlayerBattleData.GetInstance().monsterAdd(md);

			//データの生成
			IMonsterData md2 = new MonsterData(new MonsterTribesData(1), 0, 50);
			//技の取得
			md2.SkillAdd(new SkillData(4));
			md2.SkillAdd(new SkillData(3));
			md2.SkillAdd(new SkillData(2));
			md2.SkillAdd(new SkillData(1));
			//プレイヤーの手持ちに追加
			PlayerBattleData.GetInstance().monsterAdd(md2);

			//画像の設定
			playerMonsterParts_.GetMonsterSprite().sprite = md.tribesData_.backTex_;

			//名前とレベルをTextに反映
			string monsterViewName = t13.Utility.StringFullSpaceBackTamp(md.uniqueName_, 6);
			playerStatusInfoParts_.GetBaseParts().GetInfoEventText().GetText().text = monsterViewName + "　　Lｖ" + t13.Utility.HarfSizeForFullSize(md.level_.ToString());

			//HPをTextに反映
			playerStatusInfoParts_.GetFrameParts().GetHpGaugeParts().GetInfoText().text = t13.Utility.HarfSizeForFullSize(md.nowHitPoint_.ToString()) + "／" + t13.Utility.HarfSizeForFullSize(md.RealHitPoint().ToString());

			//HPゲージの調整
			playerStatusInfoParts_.GetFrameParts().GetHpGaugeParts().GetGauge().fillAmount = t13.Utility.ValueForPercentage(md.RealHitPoint(), md.nowHitPoint_, 1);

			//技をTextに反映
			for (int i = 0; i < 4; ++i) {
				novelWindowParts_.GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i).GetText().text = "　" + t13.Utility.StringFullSpaceBackTamp(md.GetSkillDatas(i).skillNname_, 7);
			}
			//文字の色の変更
			for(int i = 0;i < 4; ++i) {
				if (EnemyBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarChecker(md.GetSkillDatas(i).elementType_) > 1.0f) {
					novelWindowParts_.GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i).GetText().color = new Color32(207, 52, 112, 255);
				}
				else if (EnemyBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarChecker(md.GetSkillDatas(i).elementType_) < 1.0f
					&& EnemyBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarChecker(md.GetSkillDatas(i).elementType_) > 0) {
					novelWindowParts_.GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i).GetText().color = new Color32(52, 130, 207, 255);
				}
				else if (EnemyBattleData.GetInstance().GetMonsterDatas(0).ElementSimillarChecker(md.GetSkillDatas(i).elementType_) < 0.1f) {
					novelWindowParts_.GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i).GetText().color = new Color32(195, 195, 195, 255);
				}
				else {
					novelWindowParts_.GetAttackCommandParts().GetSkillParts().GetSkillEventTexts(i).GetText().color = new Color32(50, 50, 50, 255);
				}
			}
		}

		//外部でする処理
		//エネミーのトレーナーデータの設定
		EnemyTrainerData.getInstance().SetTrainerData("デバッガー", "テスト");

		//イベントのセット
		OpeningEventSet();
	}
	//MainLoop
	public void SceneUpdate() {
		nowProcessState_ = nowProcessState_.Update(this);
	}

	public void SceneEnd() {

	}

	public GameObject GetGameObject() { return gameObject; }

	[SerializeField] private CursorParts cursorParts_ = null;
	[SerializeField] private NovelWindowParts novelWindowParts_ = null;
	[SerializeField] private MonsterParts playerMonsterParts_ = null;
	[SerializeField] private MonsterParts enemyMonsterParts_ = null;
	[SerializeField] private PlayerParts playerParts_ = null;
	[SerializeField] private EnemyParts enemyParts_ = null;
	[SerializeField] private StatusInfoParts playerStatusInfoParts_ = null;
	[SerializeField] private StatusInfoParts enemyStatusInfoParts_ = null;
	[SerializeField] private EffectParts playerEffectParts_ = null;
	[SerializeField] private EffectParts enemyEffectParts_ = null;
	[SerializeField] private AudioParts playerAudioParts_ = null;
	[SerializeField] private AudioParts enemyAudioParts_ = null;

	public HpGaugeParts GetEnemyMonsterHpGauge() { return enemyStatusInfoParts_.GetFrameParts().GetHpGaugeParts(); }
	public HpGaugeParts GetPlayerMonsterHpGauge() { return playerStatusInfoParts_.GetFrameParts().GetHpGaugeParts(); }
	public MonsterParts GetEnemyMonsterParts() { return enemyMonsterParts_; }
	public MonsterParts GetPlayerMonsterParts() { return playerMonsterParts_; }
	public StatusInfoParts GetPlayerStatusInfoParts() { return playerStatusInfoParts_; }
	public StatusInfoParts GetEnemyStatusInfoParts() { return enemyStatusInfoParts_; }
	public EffectParts GetPlayerEffectParts() { return playerEffectParts_; }
	public EffectParts GetEnemyEffectParts() { return enemyEffectParts_; }
	public NovelWindowParts GetNovelWindowParts() { return novelWindowParts_; }
	public CursorParts GetCursorParts() { return cursorParts_; }
	public AudioParts GetPlayerAudioParts() { return playerAudioParts_; }
	public AudioParts GetEnemyAudioParts() { return enemyAudioParts_; }

	public void AttackCommandSkillInfoTextSet(int number) {
		IMonsterData md = PlayerBattleData.GetInstance().GetMonsterDatas(0);

		string playPointContext = t13.Utility.HarfSizeForFullSize(md.GetSkillDatas(number).nowPlayPoint_.ToString()) + "／" + t13.Utility.HarfSizeForFullSize(md.GetSkillDatas(number).playPoint_.ToString());

		novelWindowParts_.GetAttackCommandParts().GetSkillInfoParts().GetCommandWindowText().text =
			"PP　　　　" + playPointContext + "\n"
			+ "わざタイプ／" + md.GetSkillDatas(number).elementType_.GetName();
	}

	public void ChangeUiAttackCommand() {
		novelWindowParts_.GetCommandParts().gameObject.SetActive(false);

		novelWindowParts_.GetAttackCommandParts().gameObject.SetActive(true);

		t13.UnityUtil.ObjectPosMove(cursorParts_.gameObject, new Vector3(-8.4f, -3.25f, -4));

		AttackCommandSkillInfoTextSet(0);
		playerSelectSkillNumber_ = 0;

		nowAttackCommandState_ = new AttackCommand0();
	}
	public void ChangeUiCommand() {
		novelWindowParts_.GetAttackCommandParts().gameObject.SetActive(false);

		novelWindowParts_.GetCommandParts().gameObject.SetActive(true);

		t13.UnityUtil.ObjectPosMove(cursorParts_.gameObject, new Vector3(1.7f, -3.25f, -4));

		nowCommandState_ = new CommandAttack();

		string playerFirstMonsterName = PlayerBattleData.GetInstance().GetMonsterDatas(0).tribesData_.monsterName_;
		string context_ = playerFirstMonsterName + "は　どうする？";
		novelWindowParts_.GetNovelWindowText().text = context_;
	}
	public void InactiveUiAttackCommand() {
		novelWindowParts_.GetNovelWindowText().text = "　";

		novelWindowParts_.GetAttackCommandParts().gameObject.SetActive(false);
		cursorParts_.gameObject.SetActive(false);
	}
	public void InactiveUiCommand() {
		novelWindowParts_.GetNovelWindowText().text = "　";

		novelWindowParts_.GetCommandParts().gameObject.SetActive(false);
		cursorParts_.gameObject.SetActive(false);
	}
	public void ActiveUiCommand() {
		novelWindowParts_.GetCommandParts().gameObject.SetActive(true);
		cursorParts_.gameObject.SetActive(true);

		t13.UnityUtil.ObjectPosMove(cursorParts_.gameObject, new Vector3(1.7f, -3.25f, -4));

		nowCommandState_ = new CommandAttack();

		string playerFirstMonsterName = PlayerBattleData.GetInstance().GetMonsterDatas(0).tribesData_.monsterName_;
		string context_ = playerFirstMonsterName + "は　どうする？";
		novelWindowParts_.GetNovelWindowText().text = context_;
	}

	//仲介クラス
	private IInputProvider inputProvider_;
	public IInputProvider GetInputProvider() { return inputProvider_; }
	public void SetInputProvider(IInputProvider inputProvider) { inputProvider_ = inputProvider; }

	//ステート
	private IProcessState nowProcessState_;
	//get
	public IProcessState nowProcessState() { return nowProcessState_; }
	public ICommandState nowCommandState_ { get; set; }
	private NovelWindowPartsActiveState novelWindowPartsActiveState_ = new NovelWindowPartsActiveState(NovelWindowPartsActive.Active);
	public NovelWindowPartsActiveState GetNovelWindowPartsActiveState() { return novelWindowPartsActiveState_; }

	public void CommandUpCursorMove() {
		t13.UnityUtil.ObjectPosAdd(cursorParts_.gameObject, new Vector3(0, 0.8f, 0));
	}
	public void CommandDownCursorMove() {
		t13.UnityUtil.ObjectPosAdd(cursorParts_.gameObject, new Vector3(0, -0.8f, 0));
	}
	public void CommandRightCursorMove() {
		t13.UnityUtil.ObjectPosAdd(cursorParts_.gameObject, new Vector3(4.1f, 0, 0));
	}
	public void CommandLeftCursorMove() {
		t13.UnityUtil.ObjectPosAdd(cursorParts_.gameObject, new Vector3(-4.1f, 0, 0));
	}

	public IAttackCommandState nowAttackCommandState_ { get; set; }
	public void AttackCommandUpCursorMove() {
		t13.UnityUtil.ObjectPosAdd(cursorParts_.gameObject, new Vector3(0, 0.8f, 0));
		playerSelectSkillNumber_ -= 2;
	}
	public void AttackCommandDownCursorMove() {
		t13.UnityUtil.ObjectPosAdd(cursorParts_.gameObject, new Vector3(0, -0.8f, 0));
		playerSelectSkillNumber_ += 2;
	}
	public void AttackCommandRightCursorMove() {
		t13.UnityUtil.ObjectPosAdd(cursorParts_.gameObject, new Vector3(5.6f, 0, 0));
		playerSelectSkillNumber_ += 1;
	}
	public void AttackCommandLeftCursorMove() {
		t13.UnityUtil.ObjectPosAdd(cursorParts_.gameObject, new Vector3(-5.6f, 0, 0));
		playerSelectSkillNumber_ -= 1;
	}
	public int playerSelectSkillNumber_ { get; set; }
	public int enemySelectSkillNumber_ { get; set; }

	private const float EventContextUpdateTime_ = 0.4f;
	private const float EventWaitTime_ = 0.8f;
	public float GetEventContextUpdateTime() { return EventContextUpdateTime_; }
	public float GetEventWaitTime() { return EventWaitTime_; }

	void OpeningEventSet() {
		AllEventManager.GetInstance().EventGameObjectSet(enemyMonsterParts_.GetEventGameObject());
		AllEventManager.GetInstance().EventGameObjectSet(playerMonsterParts_.GetEventGameObject());
		AllEventManager.GetInstance().EventGameObjectsActiveSetExecute(false);

		//プレイヤーとエネミーの入場
		AllEventManager.GetInstance().EventGameObjectSet(enemyParts_.GetEventGameObject(), 3.5f);
		AllEventManager.GetInstance().EventGameObjectSet(playerParts_.GetEventGameObject(), -4.5f);
		AllEventManager.GetInstance().EventGameObjectsPosMoveXExecute(2.0f);
		{
			//文字列の設定
			EnemyTrainerData enemyTrainerData = EnemyTrainerData.getInstance();
			string context = enemyTrainerData.job() + "の　" + enemyTrainerData.name() + "が\nしょうぶを　しかけてきた！";

			AllEventManager.GetInstance().EventTextSet(novelWindowParts_.GetEventText(), context);
			AllEventManager.GetInstance().EventTextsUpdateExecute(EventContextUpdateTime_);
		}
		//Enterの押下待ち
		AllEventManager.GetInstance().EventWaitEnterSelectSet();
		{
			//文字列の設定
			string enemyFirstMonsterName = EnemyBattleData.GetInstance().GetMonsterDatas(0).tribesData_.monsterName_;
			EnemyTrainerData enemyTrainerData = EnemyTrainerData.getInstance();
			string context = enemyTrainerData.job() + "の　" + enemyTrainerData.name() + "は\n" + enemyFirstMonsterName + "を　くりだした！";

			AllEventManager.GetInstance().EventTextSet(novelWindowParts_.GetEventText(), context);
			AllEventManager.GetInstance().EventTextsUpdateExecute(EventContextUpdateTime_);
		}
		//エネミーの退場
		AllEventManager.GetInstance().EventGameObjectSet(enemyParts_.GetEventGameObject(), 3.5f + 9.5f);
		AllEventManager.GetInstance().EventGameObjectsPosMoveXExecute(1.0f);
		//エネミーモンスターの登場
		AllEventManager.GetInstance().EventGameObjectSet(enemyMonsterParts_.GetEventGameObject());
		AllEventManager.GetInstance().EventGameObjectsActiveSetExecute(true);
		//エネミーモンスターのインフォメーションの入場
		AllEventManager.GetInstance().EventGameObjectSet(enemyStatusInfoParts_.GetEventGameObject(), -3.5f);
		AllEventManager.GetInstance().EventGameObjectsPosMoveXExecute(0.4f);
		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(EventWaitTime_);
		{
			//文字列の設定
			string playerFirstMonsterName = PlayerBattleData.GetInstance().GetMonsterDatas(0).tribesData_.monsterName_;
			string context = "ゆけっ！　" + playerFirstMonsterName + "！";

			AllEventManager.GetInstance().EventTextSet(novelWindowParts_.GetEventText(), context);
			AllEventManager.GetInstance().EventTextsUpdateExecute(EventContextUpdateTime_);
		}
		//プレイヤーの退場
		AllEventManager.GetInstance().EventGameObjectSet(playerParts_.GetEventGameObject(), -4.5f - 9.5f);
		AllEventManager.GetInstance().EventGameObjectsPosMoveXExecute(1.5f);
		//プレイヤーモンスターの登場
		AllEventManager.GetInstance().EventGameObjectSet(playerMonsterParts_.GetEventGameObject());
		AllEventManager.GetInstance().EventGameObjectsActiveSetExecute(true);
		//プレイヤーモンスターのインフォメーションの入場
		AllEventManager.GetInstance().EventGameObjectSet(playerStatusInfoParts_.GetEventGameObject(), 4.0f);
		AllEventManager.GetInstance().EventGameObjectsPosMoveXExecute(0.4f);
		//ウェイト
		AllEventManager.GetInstance().EventWaitSet(EventWaitTime_);
		{
			//文字列の設定
			string playerFirstMonsterName = PlayerBattleData.GetInstance().GetMonsterDatas(0).tribesData_.monsterName_;
			string context = playerFirstMonsterName + "は　どうする？";

			AllEventManager.GetInstance().EventTextSet(novelWindowParts_.GetEventText(), context);
			AllEventManager.GetInstance().EventTextsUpdateExecute();
		}
		//コマンドの選択肢とカーソルの出現
		AllEventManager.GetInstance().EventGameObjectSet(novelWindowParts_.GetCommandParts().GetEventGameObject());
		AllEventManager.GetInstance().EventGameObjectSet(cursorParts_.GetEventGameObject());
		AllEventManager.GetInstance().EventGameObjectsActiveSetExecute(true);
		//イベントの最後
		AllEventManager.GetInstance().EventFinishSet();
	}
}
