using System;
using UnityEngine;

[Serializable]
public class ShopTower 
{
    public string name;
    public int cost;
    public GameObject turretPrefab;


    public ShopTower(string _name, int _cost, GameObject _turretPrefab)
    {
        name = _name;
        cost = _cost;
        turretPrefab = _turretPrefab;
    }
}
