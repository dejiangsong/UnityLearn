
using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventBase : MonoBehaviour {

    [HideInInspector]
    public EventCode EventCode;

    public abstract void OnEvent(EventData eventData);

    public virtual void Start() {
        PhotonEngine.Instance.AddEvent(this);
    }

    public void OnDestroy() {
        PhotonEngine.Instance.RemoveEvent(this);
    }
}
