using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager main;

    public int currency;
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        currency = 100;
    }
    public void IncreaseCurrency(int ammount)
    {
        currency += ammount;
    }
    public bool SpendCurrency(int ammount)
    {
        if (ammount <= currency)
        {
            //buy item
            currency -= ammount;
            return true;
        }
        else {
            Debug.Log("no money");
            return false;
        }
    }
}
