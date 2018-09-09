using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Wave[] Waves;                        // 每一波的参数
    public Transform[] StartTransform;           // 出生点位置
    public Transform[] CoreTransform;            // 核心的位置
    [HideInInspector]
    public int EnemyAliveCount = 0;      // 存活的敌人数量


    // Use this for initialization
    void Start() {
        StartCoroutine("SpawnEnemy");           // 开始生成敌人
    }


    // Update is called once per frame
    void Update() {

    }


    IEnumerator SpawnEnemy() {
        foreach (var wave in Waves) {
            for (int i = 0; i < wave.count; i++) {
                GameObject go = GameObject.Instantiate(wave.enemyPrefabs, StartTransform[Random.Range(0, StartTransform.Length)].position, Quaternion.identity);  // 生成敌人
                go.GetComponent<EnemyController>().TargetPosition = CoreTransform[Random.Range(0, CoreTransform.Length)].position;     // 设置敌人的目标位置
                EnemyAliveCount++;
                yield return new WaitForSeconds(wave.rate);
            }
            while (EnemyAliveCount > 0) {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
