using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFood : MonoBehaviour {

    public GameObject Food;

    private float width;
    private float height;

    // Use this for initialization
    void Start() {
        getWidthAndHeight();
        Invoke("createFood", 0.5f);
    }


    // Update is called once per frame
    void Update() {
        
    }


    void createFood() {
        Instantiate(Food, new Vector2(Random.Range(-width, width), Random.Range(-height, height)), Quaternion.identity);
    }



    #region 公共函数
    void getWidthAndHeight() {
        Vector3 screenToWorldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        width = screenToWorldSize.x;
        height = screenToWorldSize.y;
    }
    #endregion
}
