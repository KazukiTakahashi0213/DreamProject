using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrainerData {
	public void SetTrainerData(string job, string name) {
		job_ = job;
		name_ = name;
	}

	//get
	public string job() { return job_; }
	public string name() { return name_; }

	private string job_;
	private string name_;

	//シングルトン
	private EnemyTrainerData() { }

	static private EnemyTrainerData instance_ = null;
	static public EnemyTrainerData getInstance() {
		if (instance_ != null) return instance_;

		instance_ = new EnemyTrainerData();
		return instance_;
	}
}
