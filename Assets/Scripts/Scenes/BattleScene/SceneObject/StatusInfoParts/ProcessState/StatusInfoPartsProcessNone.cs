using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfoPartsProcessNone : IStatusInfoPartsProcessState {
	public IStatusInfoPartsProcessState Update(StatusInfoParts statusInfoParts) {
		return this;
	}
}
