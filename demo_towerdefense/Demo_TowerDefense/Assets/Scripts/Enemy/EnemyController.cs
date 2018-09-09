using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Vector3 TargetPosition;
    public float Speed = 10f;

    private NavMeshAgent navAgent;


    // Use this for initialization
    void Start() {
        navAgent = this.GetComponent<NavMeshAgent>();
        //navAgent.destination = TargetPosition.position;
        navAgent.SetDestination(TargetPosition);       // 设置目标地点
        navAgent.stoppingDistance = 1f;                         // 设置停止距离
        navAgent.speed = Speed;                                 // 设置速度
    }


    // Update is called once per frame
    void Update() {
        if (navAgent.enabled && navAgent.remainingDistance < 1) {
            navAgent.isStopped = true;      // 停止导航
            //navAgent.enabled = false;
            GameObject.Find("Game Manager").GetComponent<EnemySpawner>().EnemyAliveCount--;        // 到达目的地，减少敌人存活数量的计数
            Destroy(this.gameObject);       // 销毁此游戏物体
        }
    }
    

    private void dealDie() {
        GameObject.Find("Game Manager").GetComponent<EnemySpawner>().EnemyAliveCount--;        // 被打死了，减少敌人存活数量的计数
    }
}
