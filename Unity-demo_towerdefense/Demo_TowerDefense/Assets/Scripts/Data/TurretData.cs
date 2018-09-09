using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData {

    public GameObject TurretPrefab;
    public int Cost;
    public GameObject TurretUpgradePrefab;
    public int UpgradeCost;
    public TurretType type;
    [HideInInspector]
    public int TotalCost;
    public int UpgradeCount;
}

public enum TurretType {
    StandardTurret,
    MissileTurret,
    LaserTurret
}