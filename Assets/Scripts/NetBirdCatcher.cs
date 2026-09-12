using UnityEngine;

public class NetBirdCatcher : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    void OnCollisionEnter2D(Collision2D collision)
    {
        BirdStat bird = collision.gameObject.GetComponent<BirdStat>();
        if (bird != null && playerStats != null)
        {
            // Example action: add the bird to the player's list of birds
            if (playerStats.AddBird(bird))
            {
                Destroy(collision.gameObject); // Example action: destroy the bird after catching it
            }
        }
    }
}
