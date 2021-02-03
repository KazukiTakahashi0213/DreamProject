using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagazineParts : MonoBehaviour {
	[SerializeField] private SpriteRenderer magazineSpriteRenderer_ = null;
	[SerializeField] private List<MonsterSDParts> monsterSDsParts_ = null;
	[SerializeField] private UpdateGameObject eventGameObject_ = null;
	[SerializeField] private int holeNumber_ = 3;

	public SpriteRenderer GetMagazineSpriteRenderer() { return magazineSpriteRenderer_; }
	public MonsterSDParts GetMonsterSDsParts(int number) { return monsterSDsParts_[number]; }
	public int GetMonsterSDsPartsCount() { return monsterSDsParts_.Count; }
	public UpdateGameObject GetEventGameObject() { return eventGameObject_; }

	private const float UPDATE_TIME_REGULATION = 0.3f;

	public void UpRollMagazineParts() {
		AllEventManager.GetInstance().UpdateGameObjectSet(eventGameObject_, new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (360.0f / holeNumber_)));

		for(int i = 0;i < monsterSDsParts_.Count; ++i) {
			AllEventManager.GetInstance().UpdateGameObjectSet(monsterSDsParts_[i].GetEventGameObject(), new Vector3(monsterSDsParts_[i].transform.rotation.eulerAngles.x, monsterSDsParts_[i].transform.rotation.eulerAngles.y, monsterSDsParts_[i].transform.rotation.eulerAngles.z + 0.0f));
		}

		AllEventManager.GetInstance().UpdateGameObjectUpdateExecuteSet(UpdateGameObjectEventManagerExecute.RotMove);
		AllEventManager.GetInstance().AllUpdateEventExecute(UPDATE_TIME_REGULATION);
	}
	public void DownRollMagazineParts() {
		AllEventManager.GetInstance().UpdateGameObjectSet(eventGameObject_, new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + -(360.0f / holeNumber_)));

		for (int i = 0; i < monsterSDsParts_.Count; ++i) {
			AllEventManager.GetInstance().UpdateGameObjectSet(monsterSDsParts_[i].GetEventGameObject(), new Vector3(monsterSDsParts_[i].transform.rotation.eulerAngles.x, monsterSDsParts_[i].transform.rotation.eulerAngles.y, monsterSDsParts_[i].transform.rotation.eulerAngles.z + 0.0f));
		}

		AllEventManager.GetInstance().UpdateGameObjectUpdateExecuteSet(UpdateGameObjectEventManagerExecute.RotMove);
		AllEventManager.GetInstance().AllUpdateEventExecute(UPDATE_TIME_REGULATION);
	}
}
