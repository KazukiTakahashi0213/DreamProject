using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoFrameParts : MonoBehaviour{
	[SerializeField] private SpriteRenderer infoFramePartsSprite_ = null;
	public SpriteRenderer GetInfoFramePartsSprite() { return infoFramePartsSprite_; }

	[SerializeField] private List<Text> texts_ = new List<Text>();
	public Text GetTexts(int value) { return texts_[value]; }
	public int GetTextsCount() { return texts_.Count; }
}
