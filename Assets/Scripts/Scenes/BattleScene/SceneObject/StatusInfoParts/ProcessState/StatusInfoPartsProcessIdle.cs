using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfoPartsProcessIdle : IStatusInfoPartsProcessState {
	public IStatusInfoPartsProcessState Update(StatusInfoParts statusInfoParts) {
		if(statusInfoParts.GetTimeCounter().measure(Time.deltaTime, statusInfoParts.GetIdleTimeRegulation())) {
			/*
			t13.UnityUtil.ObjectLinearUpdatePosY(
				statusInfoParts.GetEventGameObject().GetGameObject(),
				statusInfoParts.GetTimeFluct(),
				statusInfoParts.GetEventGameObject().GetGameObject().transform.position.y + statusInfoParts.GetProcessIdleState().addPos_,
				statusInfoParts.GetIdleTimeRegulation(),
				statusInfoParts.GetIdleTimeRegulation()
				);
			*/
			Vector3 vec3 = new Vector3(
				statusInfoParts.GetEventGameObject().GetGameObject().transform.position.x, 
				statusInfoParts.GetEventGameObject().GetGameObject().transform.position.y + statusInfoParts.GetProcessIdleState().addPos_, 
				statusInfoParts.GetEventGameObject().GetGameObject().transform.position.z
				);
			t13.UnityUtil.ObjectPosMove(statusInfoParts.GetEventGameObject().GetGameObject(), vec3);

			statusInfoParts.SetProcessIdleState(statusInfoParts.GetProcessIdleState().Next());
		}
		else {
			/*
			t13.UnityUtil.ObjectLinearUpdatePosY(
				statusInfoParts.GetEventGameObject().GetGameObject(),
				statusInfoParts.GetTimeFluct(),
				statusInfoParts.GetEventGameObject().GetGameObject().transform.position.y + statusInfoParts.GetProcessIdleState().addPos_,
				statusInfoParts.GetTimeCounter().count(),
				statusInfoParts.GetIdleTimeRegulation()
				);
			*/
		}

		return this;
	}
}
