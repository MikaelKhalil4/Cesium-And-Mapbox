using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null)
        {
            // Trigger game over when the plane collides with any object
            gameManager.TriggerGameOver();
        }
    }
}
