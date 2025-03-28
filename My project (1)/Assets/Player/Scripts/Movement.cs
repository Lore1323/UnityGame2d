using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] KeyCode keyUp;
    [SerializeField] KeyCode keyDown;
    [SerializeField] KeyCode keyLeft;
    [SerializeField] KeyCode keyRight;
    public float velocity = 0.01f;
    private void Update()
    {
        Move();
    }
    void Move()
    {
        if (Input.GetKey(keyLeft))
        transform.Translate(Vector2.left * velocity);

        if (Input.GetKey(keyUp))
            transform.Translate(Vector2.up * velocity);

        if (Input.GetKey(keyDown))
            transform.Translate(Vector2.down * velocity);

        if (Input.GetKey(keyRight))
            transform.Translate(Vector2.right * velocity);
    }
}
