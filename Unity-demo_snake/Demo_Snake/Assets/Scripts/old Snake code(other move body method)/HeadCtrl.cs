using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeadCtrl : MonoBehaviour
{
	//蛇头的朝向
	private enum HeadDir
	{
		Up,
		Down,
		Left,
		Right,
	}

	//移动速度，米/秒
	public float speed;
	//计时器用来记录移动的时间
	private float _Timer = 0f;
	//蛇头当前的朝向
	private HeadDir _CurrentDir = HeadDir.Up;
	//蛇头接下来的朝向
	private HeadDir _NextDir = HeadDir.Up;
	//游戏是否结束
	private bool _IsOver = false;
	//食物的预设体
	public GameObject foodPrefab;
	//身子的预设体
	public GameObject bodyPrefab;
	//第一节身子的引用
	private Body _FirstBody;
	//最后一节身子的引用
	private Body _LastBody;

	// Use this for initialization
	void Start ()
	{
		//游戏开始时，创建一个食物
		CreateFood ();
	}

	// Update is called once per frame
	void Update ()
	{
		//如果游戏没结束，则开始循环
		if (_IsOver != true) {
			Turn ();
			Move ();
		} 
		//结束后，清空，重来
		else {
			_IsOver = false;
			restart ();
			transform.position = new Vector3 (0f, 0f, 0f);
		}
	}

	private void Move ()
	{
		//将计时器累加时间增量，增量：delta
		_Timer += Time.deltaTime;
		//判断当前帧是否应该移动
		if (_Timer >= (1f / speed)) {
			//使蛇头旋转
			switch (_NextDir) {
			case HeadDir.Up:
				transform.forward = Vector3.forward;
				_CurrentDir = HeadDir.Up;
				break;
			case HeadDir.Down:
				transform.forward = Vector3.back;
				_CurrentDir = HeadDir.Down;
				break;
			case HeadDir.Left:
				transform.forward = Vector3.left;
				_CurrentDir = HeadDir.Left;
				break;
			case HeadDir.Right:
				transform.forward = Vector3.right;
				_CurrentDir = HeadDir.Right;
				break;
			}

			//记录头部移动之前的位置
			Vector3 nextPos = transform.position;
			//向前移动一个单位
			transform.Translate (Vector3.forward);
			//重置计时器
			_Timer = 0f;

			//如果有身子
			if (_FirstBody != null) {
				//让第一节身子移动
				_FirstBody.Move (nextPos);
			}
		}
	}

	private void Turn ()
	{
		bool isTouch = false;
		float angle = 0;
		//如果有触摸
		if (Input.touchCount > 0) {
			//触摸时触发
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				//是否触摸
				isTouch = true;
				//得到触摸位置,以视图位置获取
				Vector2 touchPos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
				//把取值0~1转换为-0.5~0.5
				touchPos.x -= 0.5f;
				touchPos.y -= 0.5f;
				//计算与参考向量（0f,1f）的夹角，没有正负
				angle = Vector2.Angle (new Vector2 (0f, 1f), touchPos);
				//用来计算叉乘，通过z轴值判断angle角度的正负
				Vector3 cross = Vector3.Cross (new Vector2 (0f, 1f), touchPos);
				angle = cross.z > 0f ? -angle : angle;
			}
		}

		//监听用户按键事件
		if (Input.GetKey (KeyCode.W) || (isTouch && (angle >= -45f && angle <= 45f))) {
			//设定蛇头接下来的转向
			_NextDir = HeadDir.Up;
			//检测按键是否有效
			if (_CurrentDir == HeadDir.Down) {
				//如果无效则报错不变
				_NextDir = _CurrentDir;
			}
		}
		//-135和135为或的关系
		if (Input.GetKey (KeyCode.S) || (isTouch && (angle <= -135f || angle >= 135f))) {
			_NextDir = HeadDir.Down;
			if (_CurrentDir == HeadDir.Up) {
				_NextDir = _CurrentDir;
			}
		}
		if (Input.GetKey (KeyCode.A) || (isTouch && (angle <= -45f && angle >= -135f))) {
			_NextDir = HeadDir.Left;
			if (_CurrentDir == HeadDir.Right) {
				_NextDir = _CurrentDir;
			}
		}
		if (Input.GetKey (KeyCode.D) || (isTouch && (angle >= 45f && angle <= 135f))) {
			_NextDir = HeadDir.Right;
			if (_CurrentDir == HeadDir.Left) {
				_NextDir = _CurrentDir;
			}
		}
	}

	//碰撞体检测
	private void OnTriggerEnter (Collider other)
	{
		//如果碰到边界，游戏结束
		if (other.tag.Equals ("Bound") || other.tag.Equals ("Body")) {
			_IsOver = true;
		}
		//如果碰到食物，将之前的食物销毁，身体增长，再创建新的食物
		if (other.tag.Equals ("Food")) {
			Destroy (other.gameObject);
			Grow ();
			CreateFood ();
		}
	}

	//身体增长
	private void Grow ()
	{
		//动态创建身子
		GameObject obj = Instantiate (bodyPrefab, new Vector3 (1000f, 1000f, 1000f), Quaternion.identity) as GameObject;
		//获取身上的Body脚本
		Body b = obj.GetComponent<Body> ();
		//如果后面还没有身子
		if (_FirstBody == null) {
			//当前创建出的身子就是第一节身子
			_FirstBody = b;
		}
		//如果有身子
		if (_LastBody != null) {
			//新创建的身子设置在当前最后一节身子后面
			_LastBody.next = b;
		}
		//更新最后一节身子
		_LastBody = b;
	}

	//创建食物
	public void CreateFood ()
	{
		float x = Random.Range (-9.5f, 9.5f);
		float z = Random.Range (-9.5f, 9.5f);
        Instantiate(foodPrefab, new Vector3(x, 0f, z), Quaternion.identity);
	}

	//重新开始
	private void restart()
	{
        SceneManager.LoadScene(0);
	}
}
