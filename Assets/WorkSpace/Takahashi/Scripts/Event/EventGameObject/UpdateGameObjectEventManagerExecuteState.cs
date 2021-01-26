using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpdateGameObjectEventManagerExecute {
	None
	, PosMove
	, PosRot
	, Max
}

public class UpdateGameObjectEventManagerExecuteState {
	public UpdateGameObjectEventManagerExecuteState(UpdateGameObjectEventManagerExecute setState) {
		state_ = setState;
	}

	public UpdateGameObjectEventManagerExecute state_;

	//None
	static private void NoneExecute(UpdateGameObjectEventManagerExecuteState mine, UpdateGameObjectEventManager updateGameObjectEventManager, float timeRegulation, t13.TimeFluctProcess timeFluctProcess) {

	}

	//PosMove
	static private void PosMoveExecute(UpdateGameObjectEventManagerExecuteState mine, UpdateGameObjectEventManager updateGameObjectEventManager, float timeRegulation, t13.TimeFluctProcess timeFluctProcess) {
		for (int i = 0; i < updateGameObjectEventManager.GetExecuteUpdateGameObjectsCount(); ++i) {
			updateGameObjectEventManager.GetExecuteUpdateGameObjects(i).ProcessStatePosMoveExecute(
				timeRegulation,
				updateGameObjectEventManager.GetExecuteEndVec3s(i),
				timeFluctProcess
				);
		}
	}

	//RotMove
	static private void RotMoveExecute(UpdateGameObjectEventManagerExecuteState mine, UpdateGameObjectEventManager updateGameObjectEventManager, float timeRegulation, t13.TimeFluctProcess timeFluctProcess) {
		for (int i = 0; i < updateGameObjectEventManager.GetExecuteUpdateGameObjectsCount(); ++i) {
			updateGameObjectEventManager.GetExecuteUpdateGameObjects(i).ProcessStateRotMoveExecute(
				timeRegulation,
				updateGameObjectEventManager.GetExecuteEndVec3s(i),
				timeFluctProcess
				);
		}
	}

	private delegate void ExecuteFunc(UpdateGameObjectEventManagerExecuteState mine, UpdateGameObjectEventManager updateGameObjectEventManager, float timeRegulation, t13.TimeFluctProcess timeFluctProcess);

	private ExecuteFunc[] executeFuncs_ = new ExecuteFunc[(int)UpdateGameObjectProcess.Max] {
		NoneExecute
		, PosMoveExecute
		, RotMoveExecute
	};
	public void Execute(UpdateGameObjectEventManager updateGameObjectEventManager, float timeRegulation, t13.TimeFluctProcess timeFluctProcess) { executeFuncs_[(int)state_](this, updateGameObjectEventManager, timeRegulation, timeFluctProcess); }
}
