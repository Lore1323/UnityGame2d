
using UnityEngine;

public class EnemyTutorrial : MonoBehaviour
{
    private int maxHealth = 100;
    private int actualHealth = 100;
    private Animator myAnimator;
    private bool isDead = false;
    private int worth = 50;
    
    void Start()
    {
        actualHealth = maxHealth;
    }
    
    
    public void TakeDamage(int appliedDamage)
    {
        if (isDead) return;
        actualHealth -= appliedDamage;
        actualHealth = Mathf.Clamp(actualHealth, 0,500);
        if (actualHealth <= 0)
        {
            isDead = true;
            myAnimator.SetBool("IsDead", true);
            ShopManager.Instance.AddCurrency(worth);    
        }
    }

    private void Dead()
    {
        Destroy(this.gameObject);
    }
}
