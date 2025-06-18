using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class GuideArrow : MonoBehaviour
{
    [Header("References")]
    public Transform[] towers;  // Asignas las 5 torres desde el inspector
    public Transform arrow;     // La flecha que rotará
    public Transform player;    // El propio player (o usa transform)

    private int currentTowerIndex = 0;
    public float rotationSpeed = 180f;
    public float angleThreshold = 5f;

    private bool isArrowActive = true;

    void Update()
    {
        if (!isArrowActive || currentTowerIndex >= towers.Length || towers[currentTowerIndex] == null) return;

        Vector2 dirToTower = towers[currentTowerIndex].position - player.position;
        float targetAngle = Mathf.Atan2(dirToTower.y, dirToTower.x) * Mathf.Rad2Deg;
        targetAngle -= 90f;
        float currentAngle = arrow.eulerAngles.z;
        float angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);

        if (Mathf.Abs(angleDiff) > angleThreshold)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationStep);
            arrow.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }
        else
        {
        
        }
    }

    public void OnTowerDefenseStart()
    {
        // Se llama cuando la torre se activa para defenderse
        isArrowActive = false;
        arrow.gameObject.SetActive(false);
    }

    public void OnTowerDefenseEnd()
    {
        // Se llama cuando la torre termina su defensa
        currentTowerIndex++;
        if (currentTowerIndex < towers.Length)
        {
            isArrowActive = true;
            arrow.gameObject.SetActive(true);
        }
        else
        {
            // Si no quedan torres, podrías desactivar la flecha permanentemente
            arrow.gameObject.SetActive(false);
        }
    }
}
