﻿using System;

namespace t13 {

	public class Time_fluct {
		//t:count
		//b:startPos_archive_
		//c:range_
		//d:regulation_time

		private float range_ = 0;
		private float startPos_archive_ = 0;
		private bool firstFluct_ = true;

		public const float PI = 3.14159265358979f;

		Time_fluctProcessState processState_ = new Time_fluctProcessState(Time_fluctProcess.Liner);

		public float GetRange() { return range_; }
		public float GetStartPos_Archive() { return startPos_archive_; }
		public Time_fluctProcessState GetProcessState() { return processState_; }

		public float InFluct(float count, float startPos, float endPos, float regulation_time) {
			if (firstFluct_) {
				range_ = endPos - startPos;
				startPos_archive_ = startPos;

				firstFluct_ = false;
			}

			if (count >= regulation_time) {
				firstFluct_ = true;
				return range_ + startPos_archive_;
			}

			return processState_.InFluct(this, count, startPos, endPos, regulation_time);
		}
		public float OutFluct(float count, float startPos, float endPos, float regulation_time) {
			if (firstFluct_) {
				range_ = endPos - startPos;
				startPos_archive_ = startPos;

				firstFluct_ = false;
			}

			if (count >= regulation_time) {
				firstFluct_ = true;
				return range_ + startPos_archive_;
			}

			return processState_.OutFluct(this, count, startPos, endPos, regulation_time);
		}
		public float InOutFluct(float count, float startPos, float endPos, float regulation_time) {
			if (firstFluct_) {
				range_ = endPos - startPos;
				startPos_archive_ = startPos;

				firstFluct_ = false;
			}

			if (count >= regulation_time) {
				firstFluct_ = true;
				return range_ + startPos_archive_;
			}

			return processState_.InOutFluct(this, count, startPos, endPos, regulation_time);
		}
	}

}
