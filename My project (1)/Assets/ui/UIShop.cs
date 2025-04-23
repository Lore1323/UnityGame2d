using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UIShop : MonoBehaviour
{
    public GameObject Shop;
    public GameObject Player;
    
    private void Awake()
    {
        Movement script=Player.GetComponent<Movement>();
    }

    private void Update()
    {
        
    }

}
