using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

/// <summary>
/// Sample示例
/// </summary>
public class LoginRequest : RequestBase {

    [HideInInspector]
    public string Username;
    [HideInInspector]
    public string Password;


    public LoginRequest() {
        OpCode = OperationCode.Login;
    }

    public override void Start() {
        base.Start();
    }

    public override void DefaultRequest() {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Username, Username);
        data.Add((byte)ParameterCode.Password, Password);

        PhotonEngine.Peer.OpCustom((byte)OpCode, data, true);       // 发起请求
    }

    public override void OnOperationResponse(OperationResponse operationResponse) {
        Debug.Log((ReturnCode)operationResponse.ReturnCode);
        gameObject.GetComponent<LoginPanel>().SendMessage("OnLoginResponseUI", (ReturnCode)operationResponse.ReturnCode);
    }
}
