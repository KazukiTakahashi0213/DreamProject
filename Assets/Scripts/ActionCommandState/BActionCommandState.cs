using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BActionCommandState {
	public virtual int UpSelect() { return 0; }
	public virtual int DownSelect() { return 0; }
	public virtual int RightSelect() { return 0; }
	public virtual int LeftSelect() { return 0; }

	public virtual int EnterSelect() { return 0; }
}
