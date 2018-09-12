using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncPositionRequest : RequestBase {

    [HideInInspector]
    public Vector3 pos;
    [Range(0,10)]
    public float syncPosRate = 0.2f;        // 同步函数执行的速率
    [Range(0,10)]
    public float syncPosDis = 0.1f;         // 移动距离发送同步请求的阈值
    
    private Vector3 lastPosition = Vector3.zero;        // 最后的位置


    public override void Start() {
        base.Start();
        InvokeRepeating("syncPosition", 3, syncPosRate);        // 同步位置（3秒后开始，每syncPosRate秒一次）
    }

    public SyncPositionRequest() {
        OpCode = OperationCode.SyncPosition;
    }

    public override void DefaultRequest() {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.X, pos.x);
        data.Add((byte)ParameterCode.Y, pos.y);
        data.Add((byte)ParameterCode.Z, pos.z);
        PhotonEngine.Peer.OpCustom((byte)OpCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse) {
        throw new System.NotImplementedException();
    }

    #region 同步请求
    /// <summary>
    /// 同步自身位置
    /// </summary>
    void syncPosition() {
        if (Vector3.Distance(transform.position, lastPosition) > syncPosDis) {
            lastPosition = transform.position;
            pos = transform.position;
            DefaultRequest();       // 发起同步请求
        }
    }
    #endregion
}
