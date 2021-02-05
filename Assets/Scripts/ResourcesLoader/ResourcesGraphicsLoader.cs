using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesGraphicsLoader {
	private List<string> filePaths_ = new List<string>();
	private List<Sprite> datas_ = new List<Sprite>();

	public Sprite GetGraphics(string filePath) {
		//２度目の読み込みだったら
		for (int i = 0; i < filePaths_.Count; ++i) {
			if (filePaths_[i] == filePath) {
				return datas_[i];
			}
		}

		Sprite loadData = Resources.Load("Graphics/" + filePath, typeof(Sprite)) as Sprite;

		filePaths_.Add(filePath);
		datas_.Add(loadData);

		return loadData;
	}

	//シングルトン
	private ResourcesGraphicsLoader() { }

	static private ResourcesGraphicsLoader instance_ = null;
	static public ResourcesGraphicsLoader GetInstance() {
		if (instance_ != null) return instance_;

		instance_ = new ResourcesGraphicsLoader();
		return instance_;
	}
	static public void ReleaseInstance() { instance_ = null; }
}
