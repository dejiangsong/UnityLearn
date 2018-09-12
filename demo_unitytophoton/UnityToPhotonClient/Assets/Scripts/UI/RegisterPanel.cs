using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour {

    public InputField usernameIF;
    public InputField passwordIF;
    public Text MsgText;

    private RegisterRequest registerRequest;


    private void Start() {
        registerRequest = GetComponent<RegisterRequest>();
    }

    public void OnRegisterButtion() {
        registerRequest.Username = usernameIF.text;
        registerRequest.Password = passwordIF.text;
        registerRequest.DefaultRequest();
    }

    public void OnRegisterResponseUI(ReturnCode returnCode) {
        if(returnCode == ReturnCode.Success) {
            Debug.Log("注册成功");
            MsgText.text = "注册成功";
        } else {
            Debug.Log("注册失败！！！");
            MsgText.text = "账号已存在！！！";
        }
    }
}
