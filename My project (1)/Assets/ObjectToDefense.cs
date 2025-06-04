using UnityEngine;

public class ObjectToDefense : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int health;

    

    public void TakeDamage(int AppliedDamage)
    {
        health -= AppliedDamage;
        Destroy(gameObject);
    }
}
