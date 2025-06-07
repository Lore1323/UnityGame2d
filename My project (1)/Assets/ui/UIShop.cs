using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UIShop : MonoBehaviour
{

    public GameObject Shop;
    public PlayerController Controller;    
    public static bool TurretSelected=false;
    public static bool modeShop=false;

    private void Awake()
    {
        Controller = new PlayerController();
    }

    private void OnEnable()
    {
        Controller.Enable();    
    }

    private void Update()
    {
        Controller.B.OpenShop.started += ctx => OpenShop();
        Controller.B.OpenShop.started -= ctx => OpenShop();
    }

    public void OpenShop()
    {
        modeShop = true;
        Movement.modeAttack = false;
        Movement.isShopOpen = true;
        bool isActive = Shop.activeSelf;
        
        Shop.SetActive(!isActive);
        Time.timeScale = isActive ? 1f : 0f;
        if (isActive)
        {
            modeShop=false;
            Movement.isShopOpen = false;
            Movement.modeAttack=true;
        }
    }
    public void SelectedTurret()
    {
        TurretSelected = true;
        if (TurretSelected == true)
        {
            Shop.SetActive(false);    
        }
    }
}
