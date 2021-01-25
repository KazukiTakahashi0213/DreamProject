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

	public void UpRollMagazineParts() {
		AllEventManager.GetInstance().EventGameObjectSet(eventGameObject_, transform.rotation.eulerAngles.z + (360.0f / holeNumber_));

		for(int i = 0;i < monsterSDsParts_.Count; ++i) {
			AllEventManager.GetInstance().EventGameObjectSet(monsterSDsParts_[i].GetEventGameObject(), monsterSDsParts_[i].transform.rotation.eulerAngles.z + 0.0f);
		}

		AllEventManager.GetInstance().EventGameObjectsRotMoveExecute(0.5f);
	}
	public void DownRollMagazineParts() {
		AllEventManager.GetInstance().EventGameObjectSet(eventGameObject_, transform.rotation.eulerAngles.z + -(360.0f / holeNumber_));

		for (int i = 0; i < monsterSDsParts_.Count; ++i) {
			AllEventManager.GetInstance().EventGameObjectSet(monsterSDsParts_[i].GetEventGameObject(), monsterSDsParts_[i].transform.rotation.eulerAngles.z + 360.0f);
		}

		AllEventManager.GetInstance().EventGameObjectsRotMoveExecute(0.5f);
	}
}
