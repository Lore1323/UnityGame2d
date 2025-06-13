using UnityEngine;
using UnityEngine.InputSystem;

public class UIShop : MonoBehaviour
{
    [SerializeField]private AudioSource source;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeShop;
    
    private PlayerController controller;
    public static bool TurretSelected { get; set; } = false;
    public static bool modeShop { get; set; } = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = new PlayerController();
    }

    private void OnEnable()
    {
        controller.Enable();
        controller.B.OpenShop.started += ctx => ToggleShop();
    }

    private void OnDisable()
    {
        controller.B.OpenShop.started -= ctx => ToggleShop();
        controller.Disable();
    }

    public void ToggleShop()
    {    
        animator.SetTrigger("OpenShop");   
    }

    public void SelectTurret()
    {
        UIShop.TurretSelected = true;
        UIShop.modeShop = true;
        animator.SetTrigger("OpenShop");
        Time.timeScale = 1f;

        // NO activar modo ataque aquí.
        // Movement.modeAttack = true;
        Movement.isShopOpen = false;
    }
    public void ActiveTrue()
    {
        AttackHandler.ignoreNextClick = true;
    }
    public void ActiveFalse()
    {
        AttackHandler.ignoreNextClick = false;
    }
    public void OpenShop()
    {
        source.PlayOneShot(openSound);
    }
    public void CloseShop()
    {
        source.PlayOneShot(closeShop);
    }
}
