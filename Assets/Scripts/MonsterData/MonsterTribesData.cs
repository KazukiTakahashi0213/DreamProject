using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTribesData : IMonsterTribesData {
	public MonsterTribesData(int number) {
		ResourcesMonsterTribesData data = ResourcesMonsterTribesDatasLoader.GetInstance().GetMonsterDatas(number);

		monsterNumber_ = number;
		monsterName_ = data.monsterName_;

		tribesHitPoint_ = data.tribesHitPoint_;
		tribesAttack_ = data.tribesAttack_;
		tribesDefense_ = data.tribesDefense_;
		tribesSpecialAttack_ = data.tribesSpecialAttack_;
		tribesSpecialDefense_ = data.tribesSpecialDefense_;
		tribesSpeed_ = data.tribesSpeed_;

		firstElement_ = new ElementTypeState((ElementType)data.firstElement_);
		secondElement_ = new ElementTypeState((ElementType)data.secondElement_);

		frontTex_ = Resources.Load("Graphics/Monster/" + data.texName_ + "/" + data.texName_ + "_Front", typeof(Sprite)) as Sprite;
		backTex_ = Resources.Load("Graphics/Monster/" + data.texName_ + "/" + data.texName_ + "_Back", typeof(Sprite)) as Sprite;
		SDTex_ = Resources.Load("Graphics/Monster/" + data.texName_ + "/" + data.texName_ + "_SD", typeof(Sprite)) as Sprite;
	}

	public int monsterNumber_ { get; }
	public string monsterName_ { get; }

	public int tribesHitPoint_ { get; }
	public int tribesAttack_ { get; }
	public int tribesDefense_ { get; }
	public int tribesSpecialAttack_ { get; }
	public int tribesSpecialDefense_ { get; }
	public int tribesSpeed_ { get; }

	public ElementTypeState firstElement_ { get; }
	public ElementTypeState secondElement_ { get; }

	public Sprite frontTex_ { get; }
	public Sprite backTex_ { get; }
	public Sprite SDTex_ { get; }
}
