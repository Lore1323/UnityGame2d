using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    private Vector2 moveDirection;
    private void Update()
    {
        MoveProjectile();
    }
    

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }
    private void MoveProjectile()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
