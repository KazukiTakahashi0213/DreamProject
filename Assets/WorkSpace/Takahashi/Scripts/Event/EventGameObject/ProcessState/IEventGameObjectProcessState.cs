using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventGameObjectProcessState {
	IEventGameObjectProcessState Update(UpdateGameObject eventObject);
}
