using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour {

    public InputField usernameIF;
    public InputField passwordIF;
    public Text MsgText;

    private LoginRequest loginRequest;


    private void Start() {
        loginRequest = GetComponent<LoginRequest>();
    }

    public void OnLoginButtion() {
        loginRequest.Username = usernameIF.text;
        loginRequest.Password = passwordIF.text;
        loginRequest.DefaultRequest();
    }

    public void OnLoginResponseUI(ReturnCode returnCode) {
        if(returnCode == ReturnCode.ExistUser) {
            Debug.Log("账户已登录！！！");
            MsgText.text = "账户已登录！！！";
        }
        else if (returnCode == ReturnCode.Success) {
            PlayerPrefs.SetString("username", usernameIF.text);
            SceneManager.LoadScene("002Game");
        } else {
            Debug.Log("密码或账户错误！！！");
            MsgText.text = "密码或账户错误！！！";
        }
    }
}
