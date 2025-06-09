using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AttackHandler : MonoBehaviour
{
    [Header("Ataque")]
    [SerializeField] private float fireCooldown = 0.5f;
    [SerializeField] private int maxShotsBeforeReload = 14;
    [SerializeField] private float reloadTime = 2f;

    [Header("Disparo")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [Header("UI")]
    [SerializeField] private List<Image> ammoIcons;
    [Header("Recarga Especial")]
    [SerializeField] private Slider reloadSlider;
    [SerializeField] private GameObject cohete;
    [SerializeField] private GameObject specialBulletPrefab; 


    private PlayerController playerController;
    private Animator animator;
    private PlayerMovement movimientoScript;
    private float lastFireTime = -Mathf.Infinity;
    private int currentShots = 0;
    private bool isReloading = false;
    public static bool isAttacking = false;
    private int reloadCount = 0;
    private int maxReloads = 4;

    private readonly int Fire_hash = Animator.StringToHash("Fire");

    private void Awake()
    {
        playerController = new PlayerController();
        animator = GetComponent<Animator>();
        movimientoScript = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        playerController.Enable();
        playerController.Combat.Attack.started += _ => TryAttack();
        playerController.Combat.SpecialAttack.started += _ => TrySpecialAttack();
    }

    private void OnDisable()
    {
        playerController.Disable();
        playerController.Combat.Attack.started -= _ => TryAttack();
        playerController.Combat.SpecialAttack.started -= _ => TrySpecialAttack();
    }

    private void TryAttack()
    {
        if (PlayerMovement.isShopOpen || !PlayerMovement.modeAttack || isReloading)
            return;

        if (Time.time < lastFireTime + fireCooldown)
            return;

        if (currentShots >= maxShotsBeforeReload)
        {
            StartCoroutine(Reload());
            return;
        }

        Attack();
    }

    private void Attack()
    {
        lastFireTime = Time.time;
        currentShots++;

        animator.SetBool("IsAttacking", true);
        isAttacking = true;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = ((Vector2)mouseWorldPos - (Vector2)spawnPoint.position).normalized;

        GameObject newBullet = ObjectPoolManager.Instance.SpawnObject(bullet, spawnPoint.position, Quaternion.identity);
        var proj = newBullet.GetComponent<Projectile>();
        if (proj != null)
            proj.SetDirection(direction);

        newBullet.SetActive(true);
        if (currentShots <= ammoIcons.Count)
        {
            int index = ammoIcons.Count - currentShots; // ← inverso
            ammoIcons[index].enabled = false;
        }
        if (currentShots >= maxShotsBeforeReload)
        {
            StartCoroutine(Reload());
        }
    }
    private void EndAttack()
    {
        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        movimientoScript.canMove = false;
        animator.SetBool("Recharge",true);
        yield return new WaitForSeconds(reloadTime);
        currentShots = 0;
        isReloading = false;
        movimientoScript.canMove = true;
        animator.SetBool("Recharge", false);
        foreach (var icon in ammoIcons)
        {
            icon.enabled = true;
        }
        // Contar recarga
        reloadCount++;
        if (reloadCount > maxReloads)
            reloadCount = maxReloads;

        // Actualizar el valor del slider (0 a 1)
        reloadSlider.value = (float)reloadCount / maxReloads;
        if (reloadCount >= maxReloads)
        {
            cohete.SetActive(true);
        }
    }
    private void TrySpecialAttack()
    {
        if (reloadCount < maxReloads) return;

        // Instanciar la bala especial
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = ((Vector2)mouseWorldPos - (Vector2)spawnPoint.position).normalized;

        GameObject Cohetee= ObjectPoolManager.Instance.SpawnObject(specialBulletPrefab, spawnPoint.position, Quaternion.identity);
        var proj = Cohetee.GetComponent<Cohete>();
        if (proj != null)
            proj.SetDirection(direction);

        Cohetee.SetActive(true);

        // Reiniciar contador e imagen
        reloadCount = 0;
        reloadSlider.value = 0;
        cohete.SetActive(false);
    }
}
