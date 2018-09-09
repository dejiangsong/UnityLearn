using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {

	//下一个身子的引用
	public Body next;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//使当前身子移动
	public void Move(Vector3 pos)
	{
		//记录移动前的位置
		Vector3 nextPos = transform.position;
		//移动当前身子
		transform.position=pos;
		//如果后面还有身子
		if (next != null) {
			//后面身子移动
			next.Move (nextPos);
		}
	}
}
