﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace t13 {

	public class Utility {
		const float PI = 3.14159265358979f;
		// ラジアンからデグリーへの変換
		static public float ToDegree(float radian) {
			return (radian / PI * 180.0f);
		}

		// デグリーからラジアンへの変換
		static public float ToRadian(float degree) {
			return (PI / 180.0f * degree);
		}

		//maxValueの値とnowValueの値の割合を出し、resultValueに掛けて、返り値で返す
		static public float ValueForPercentage(float maxValue, float nowValue, float resultValue) {
			return resultValue * (nowValue / maxValue);
		}

		//originalContextをcountとregulationの値で分割し、返り値で返す。
		static public string ContextUpdate(string originalContext, float count, float regulation) {
			if(regulation <= 0) return originalContext.Substring(0, originalContext.Length);

			float subLength = originalContext.Length * (count / regulation);

			if (subLength >= originalContext.Length) {
				return originalContext.Substring(0, originalContext.Length);
			}

			return originalContext.Substring(0, (int)subLength);
		}

		//半角の前に半角スペースを入れ、なんちゃて全角を作り、返り値で返す
		static public string HarfSizeForFullSize(string harfSizeStr) {
			string retString = "";

			for (int i = 0; i < harfSizeStr.Length; ++i) {
				retString += " " + harfSizeStr.Substring(i, 1);
			}

			return retString;
		}

		//tampStrの文字数がtampNumより小さければ、残りを全角スペースで埋めて、返り値で返す
		static public string StringFullSpaceBackTamp(string tampStr, int tampNum) {
			string retStr = tampStr;

			for (int i = 0; i < tampNum - tampStr.Length; ++i) {
				retStr += "　";
			}

			return retStr;
		}
		//tampStrの文字数がtampNumより小さければ、前から全角スペースで埋めて、返り値で返す
		static public string StringFullSpaceFrontTamp(string tampStr, int tampNum) {
			string retStr = tampStr;

			for (int i = 0; i < tampNum - tampStr.Length; ++i) {
				retStr = "　" + retStr;
			}

			return retStr;
		}
		//tampStrの文字数がtampNumより小さければ、残りを半角スペースで埋めて、返り値で返す
		static public string StringHarfSpaceBackTamp(string tampStr, int tampNum) {
			string retStr = tampStr;

			for (int i = 0; i < tampNum - tampStr.Length; ++i) {
				retStr += " ";
			}

			return retStr;
		}
		//tampStrの文字数がtampNumより小さければ、前から半角スペースで埋めて、返り値で返す
		static public string StringHarfSpaceFrontTamp(string tampStr, int tampNum) {
			string retStr = tampStr;

			for (int i = 0; i < tampNum - tampStr.Length; ++i) {
				retStr = " " + retStr;
			}

			return retStr;
		}
	}

}