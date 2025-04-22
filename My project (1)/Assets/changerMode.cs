using UnityEngine;

public class changerMode : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
            playerController.Interact.Interact.started += _ => Mode();
            
    }

    private void Mode()
    {
        
    }




}
