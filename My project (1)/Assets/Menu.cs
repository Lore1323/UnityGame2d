using UnityEngine;
using TMPro;
using System;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI nameTurretText;
    [SerializeField] private TextMeshProUGUI costTurretText;
    [SerializeField] private TextMeshProUGUI nameTurretText2;
    [SerializeField] private TextMeshProUGUI costTurretText2;
    [SerializeField] private TextMeshProUGUI nameTurretText3;
    [SerializeField] private TextMeshProUGUI costTurretText3;

    private void OnGUI()
    {
        
        currencyText.text = ShopManager.Instance.Currency.ToString();
        ShopTower[] towers = BuildTurrets.Instance.towers;

        if (towers.Length >= 3)
        {
            nameTurretText.text = towers[0].name;
            nameTurretText2.text = towers[1].name;
            nameTurretText3.text = towers[2].name;

            costTurretText.text = "$" + towers[0].cost.ToString();
            costTurretText2.text = "$" + towers[1].cost.ToString();
            costTurretText3.text = "$" + towers[2].cost.ToString();
        }
    }
}

