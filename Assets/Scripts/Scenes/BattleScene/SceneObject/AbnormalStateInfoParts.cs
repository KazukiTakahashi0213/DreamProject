using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbnormalStateInfoParts : MonoBehaviour {
	[SerializeField] private SpriteRenderer baseSprite_ = null;
	[SerializeField] private Text infoText_ = null;

	public SpriteRenderer GetBaseSprite() { return baseSprite_; }
	public Text GetInfoText() { return infoText_; }
}
