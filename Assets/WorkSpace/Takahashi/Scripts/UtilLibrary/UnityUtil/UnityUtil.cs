using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace t13 {

	public class UnityUtil {
		//moveObjectのx軸をmoveFluctで移動させる
		static public void ObjectInFluctUpdatePosX(GameObject moveObject, Time_fluct moveFluct, float endValue, float count, float regulation) {
			Vector3 objectPos = moveObject.transform.position;

			objectPos.x = moveFluct.InFluct(count, objectPos.x, endValue, regulation);
			moveObject.transform.position = objectPos;
		}
		//moveObjectのy軸をmoveFluctで移動させる
		static public void ObjectInFluctUpdatePosY(GameObject moveObject, t13.Time_fluct moveFluct, float endValue, float count, float regulation) {
			Vector3 objectPos = moveObject.transform.position;

			objectPos.y = moveFluct.InFluct(count, objectPos.y, endValue, regulation);
			moveObject.transform.position = objectPos;
		}
		//moveObjectのz軸をmoveFluctで移動させる
		static public void ObjectInFluctUpdatePosZ(GameObject moveObject, t13.Time_fluct moveFluct, float endValue, float count, float regulation) {
			Vector3 objectPos = moveObject.transform.position;

			objectPos.z = moveFluct.InFluct(count, objectPos.z, endValue, regulation);
			moveObject.transform.position = objectPos;
		}

		//moveObjectのx軸をmoveFluctで回転させる
		static public void ObjectInFluctUpdateRotX(GameObject moveObject, t13.Time_fluct moveFluct, float endValue, float count, float regulation) {
			Quaternion objectRot = moveObject.transform.rotation;

			float angleAxis = moveFluct.InFluct(count, objectRot.eulerAngles.x, endValue, regulation);
			moveObject.transform.rotation = Quaternion.AngleAxis(angleAxis, new Vector3(1, 0, 0));
		}
		//moveObjectのy軸をmoveFluctで回転させる
		static public void ObjectInFluctUpdateRotY(GameObject moveObject, t13.Time_fluct moveFluct, float endValue, float count, float regulation) {
			Quaternion objectRot = moveObject.transform.rotation;

			float angleAxis = moveFluct.InFluct(count, objectRot.eulerAngles.y, endValue, regulation);
			moveObject.transform.rotation = Quaternion.AngleAxis(angleAxis, new Vector3(0, 1, 0));
		}
		//moveObjectのz軸をmoveFluctで回転させる
		static public void ObjectInFluctUpdateRotZ(GameObject moveObject, t13.Time_fluct moveFluct, float endValue, float count, float regulation) {
			Quaternion objectRot = moveObject.transform.rotation;

			float angleAxis = moveFluct.InFluct(count, objectRot.eulerAngles.z, endValue, regulation);
			moveObject.transform.rotation = Quaternion.AngleAxis(angleAxis, new Vector3(0, 0, 1));
		}

		//moveObjをmovePosに移動させる
		static public void ObjectPosMove(GameObject moveObj, Vector3 movePos) {
			Vector3 objPos = moveObj.transform.position;
			objPos = movePos;

			moveObj.transform.position = objPos;
		}
		//moveObjをmoveRotに移動させる
		static public void ObjectRotMove(GameObject moveObj, Quaternion moveRot) {
			Quaternion objRot = moveObj.transform.rotation;
			objRot = moveRot;

			moveObj.transform.rotation = objRot;
		}

		//moveObjをmovePos分、移動させる
		static public void ObjectPosAdd(GameObject moveObj, Vector3 movePos) {
			Vector3 objPos = moveObj.transform.position;
			objPos += movePos;

			moveObj.transform.position = objPos;
		}

		//fillImageをfillFluctで拡大縮小させる
		static public void ImageInFluctUpdate(Image fillImage, t13.Time_fluct fillFluct, float endFillAmount, float count, float regulation) {
			float imageFill = fillImage.fillAmount;

			imageFill = fillFluct.InFluct(count, imageFill, endFillAmount, regulation);
			fillImage.fillAmount = imageFill;
		}

		static public Color32 Color32InFluctUpdateAlpha(Color32 color, Time_fluct timeFluct, float endAlpha, float count, float timeRegulation) {
			float result = timeFluct.InFluct(count, color.a, endAlpha, timeRegulation);

			return new Color32(color.r, color.g, color.b, (byte)result);
		}

		static public Color32 ColorForColor32(Color color) {
			return new Color32((byte)(color.r * 255), (byte)(color.g * 255), (byte)(color.b * 255), (byte)(color.a * 255));
		}

		static public void GameQuit() {
			#if UNITY_EDITOR
						UnityEditor.EditorApplication.isPlaying = false;
			#elif UNITY_STANDALONE
			      UnityEngine.Application.Quit();
			#endif
		}
	}

}
