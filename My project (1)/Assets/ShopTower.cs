using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class ShopTower 
{
    public string name;
    public int cost;
    public GameObject turretPrefab;
    private TextMeshProUGUI nameTurretText;
    private TextMeshProUGUI costTurretText;


    public ShopTower(string _name, int _cost, GameObject _turretPrefab)
    {
        name = _name;
        cost = _cost;
        turretPrefab = _turretPrefab;
        costTurretText.SetText("cost");
    }   
}
