using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSeedTutorialDocter : MonoBehaviour {
	//EntryPoint
	void Start() {
		EventMoveMap eventMoveMap_ = GetComponent<EventMoveMap>();

		eventMoveMap_.GetEventSetFuncs().Add(BattleStart);
		eventMoveMap_.GetEventSetFuncs().Add(BattleVictory);
		eventMoveMap_.GetEventSetFuncs().Add(BattleLose);
		eventMoveMap_.GetEventSetFuncs().Add(BattleAfter);
	}

	private static void BattleStart(EventMoveMap eventMoveMap, MapManager mapManager) {
		PlayerBattleData.ReleaseInstance();
		EnemyBattleData.ReleaseInstance();

		AllEventManager allEventMgr = AllEventManager.GetInstance();
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		PlayerTrainerData playerTrainerData = PlayerTrainerData.GetInstance();
		EnemyTrainerData enemyTrainerData = EnemyTrainerData.GetInstance();
		PlayerBattleData playerBattleData = PlayerBattleData.GetInstance();
		EnemyBattleData enemyBattleData = EnemyBattleData.GetInstance();

		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().blinkTimeRegulation_ = 0.5f;
		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().GetBlinkState().state_ = UpdateSpriteRendererProcessBlink.In;

		//エネミーのモンスター設定
		{
			//データの生成
			IMonsterData md = new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.Bauporisu), 0, 50);
			//技の取得
			md.SkillAdd(new SkillData(SkillDataNumber.Taiatari));
			md.SkillAdd(new SkillData(SkillDataNumber.Hiitosutanpu));
			md.SkillAdd(new SkillData(SkillDataNumber.Mizushuriken));
			md.SkillAdd(new SkillData(SkillDataNumber.Uddohanmaa));
			//エネミーの手持ちに追加
			enemyBattleData.monsterAdd(md);
		}
		//{
		//	//データの生成
		//	IMonsterData md = new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.Maikon), 0, 50);
		//	//技の取得
		//	md.SkillAdd(new SkillData(SkillDataNumber.Taiatari));
		//	md.SkillAdd(new SkillData(SkillDataNumber.Hiitosutanpu));
		//	md.SkillAdd(new SkillData(SkillDataNumber.Mizushuriken));
		//	md.SkillAdd(new SkillData(SkillDataNumber.Uddohanmaa));
		//	//エネミーの手持ちに追加
		//	enemyBattleData.monsterAdd(md);
		//}

		//エネミーの設定
		enemyTrainerData.SetTrainerData("はかせ", "ヴィクター", ResourcesGraphicsLoader.GetInstance().GetGraphics("Enemy/Victer0"));

		{
			//データの生成
			IMonsterData md = new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.Maikon), 0, 50);
			//技の取得
			md.SkillAdd(new SkillData(SkillDataNumber.Taiatari));
			md.SkillAdd(new SkillData(SkillDataNumber.Hiitosutanpu));
			md.SkillAdd(new SkillData(SkillDataNumber.Mizushuriken));
			md.SkillAdd(new SkillData(SkillDataNumber.Uddohanmaa));
			//プレイヤーの手持ちに追加
			playerTrainerData.MonsterAdd(md);
		}
		{
			//データの生成
			IMonsterData md = new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.Bauporisu), 0, 50);
			//技の取得
			md.SkillAdd(new SkillData(SkillDataNumber.Uddohanmaa));
			md.SkillAdd(new SkillData(SkillDataNumber.Mizushuriken));
			md.SkillAdd(new SkillData(SkillDataNumber.Hiitosutanpu));
			md.SkillAdd(new SkillData(SkillDataNumber.Taiatari));
			//プレイヤーの手持ちに追加
			playerTrainerData.MonsterAdd(md);
		}
		{
			//データの生成
			IMonsterData md = new MonsterData(new MonsterTribesData(MonsterTribesDataNumber.Furiruma), 0, 50);
			//技の取得
			md.SkillAdd(new SkillData(SkillDataNumber.Uddohanmaa));
			md.SkillAdd(new SkillData(SkillDataNumber.Mizushuriken));
			md.SkillAdd(new SkillData(SkillDataNumber.Hiitosutanpu));
			md.SkillAdd(new SkillData(SkillDataNumber.Taiatari));
			//プレイヤーの手持ちに追加
			playerTrainerData.MonsterAdd(md);
		}

		////ノベル処理
		//eventMoveMap.NovelEvent(mapManager.GetNovelWindowParts(), "TutorialDocter/BattleStart1");
		//
		////ウェイト
		//allEventMgr.EventWaitSet(allSceneMgr.GetEventWaitTime());
		//
		////移動処理
		//eventMoveMap.ObjectMovePosYEvent(mapManager.GetPlayerMoveMap(), 3, 1.0f);
		//
		////ウェイト
		//allEventMgr.EventWaitSet(allSceneMgr.GetEventWaitTime());
		//
		////ノベル処理
		//eventMoveMap.NovelEvent(mapManager.GetNovelWindowParts(), "TutorialDocter/BattleStart2");
		//
		////ウェイト
		//allEventMgr.EventWaitSet(allSceneMgr.GetEventWaitTime() * 2.0f);
		//
		////ノベル処理
		//eventMoveMap.NovelEvent(mapManager.GetNovelWindowParts(), "TutorialDocter/BattleStart3");
		//
		////ウェイト
		//allEventMgr.EventWaitSet(allSceneMgr.GetEventWaitTime() * 2.0f);

		//ノベル処理
		eventMoveMap.NovelEvent(mapManager.GetNovelWindowParts(), "TutorialDocter/BattleStart4");

		//戦闘の処理
		eventMoveMap.BattleEvent();
	}
	private static void BattleVictory(EventMoveMap eventMoveMap, MapManager mapManager) {
		AllEventManager allEventMgr = AllEventManager.GetInstance();
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		PlayerTrainerData playerTrainerData = PlayerTrainerData.GetInstance();
		EnemyTrainerData enemyTrainerData = EnemyTrainerData.GetInstance();
		PlayerBattleData playerBattleData = PlayerBattleData.GetInstance();
		EnemyBattleData enemyBattleData = EnemyBattleData.GetInstance();

		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().blinkTimeRegulation_ = 0.5f;
		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().GetBlinkState().state_ = UpdateSpriteRendererProcessBlink.In;

		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Taiatari));
		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Hiitosutanpu));
		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Mizushuriken));
		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Uddohanmaa));
		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Hiitosutanpu));

		//バトル後のイベントへ
		eventMoveMap.executeEventNum_ = 4;

		//階層の移動の解放
		mapManager.GetFloorObjects().GetEventMoveMaps(1).executeEventNum_ = 1;
		mapManager.GetFloorObjects().GetEventMoveMaps(2).executeEventNum_ = 1;

		//ノベル処理
		eventMoveMap.NovelEvent(mapManager.GetNovelWindowParts(), "TutorialDocter/BattleVictory1");

		//イベントの最後
		allEventMgr.EventFinishSet();
	}
	private static void BattleLose(EventMoveMap eventMoveMap, MapManager mapManager) {
		AllEventManager allEventMgr = AllEventManager.GetInstance();
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		PlayerTrainerData playerTrainerData = PlayerTrainerData.GetInstance();
		EnemyTrainerData enemyTrainerData = EnemyTrainerData.GetInstance();
		PlayerBattleData playerBattleData = PlayerBattleData.GetInstance();
		EnemyBattleData enemyBattleData = EnemyBattleData.GetInstance();

		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().blinkTimeRegulation_ = 0.5f;
		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().GetBlinkState().state_ = UpdateSpriteRendererProcessBlink.In;

		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Taiatari));
		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Hiitosutanpu));
		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Mizushuriken));
		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Uddohanmaa));
		playerTrainerData.SkillAdd(new SkillData(SkillDataNumber.Hiitosutanpu));

		//バトル後のイベントへ
		eventMoveMap.executeEventNum_ = 4;

		//階層の移動の解放
		mapManager.GetFloorObjects().GetEventMoveMaps(1).executeEventNum_ = 1;
		mapManager.GetFloorObjects().GetEventMoveMaps(2).executeEventNum_ = 1;

		//ノベル処理
		eventMoveMap.NovelEvent(mapManager.GetNovelWindowParts(), "TutorialDocter/BattleLose1");

		//イベントの最後
		allEventMgr.EventFinishSet();
	}
	private static void BattleAfter(EventMoveMap eventMoveMap, MapManager mapManager) {
		AllEventManager allEventMgr = AllEventManager.GetInstance();
		AllSceneManager allSceneMgr = AllSceneManager.GetInstance();
		PlayerTrainerData playerTrainerData = PlayerTrainerData.GetInstance();
		EnemyTrainerData enemyTrainerData = EnemyTrainerData.GetInstance();
		PlayerBattleData playerBattleData = PlayerBattleData.GetInstance();
		EnemyBattleData enemyBattleData = EnemyBattleData.GetInstance();

		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().blinkTimeRegulation_ = 0.5f;
		mapManager.GetNovelWindowParts().GetNovelBlinkIconParts().GetNovelBlinkIconEventSprite().GetBlinkState().state_ = UpdateSpriteRendererProcessBlink.In;

		//ノベル処理
		eventMoveMap.NovelEvent(mapManager.GetNovelWindowParts(), "TutorialDocter/BattleAfter1");

		//イベントの最後
		allEventMgr.EventFinishSet();
	}
}
