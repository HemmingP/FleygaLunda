using UnityEngine;

public class FallDamageDeterminer : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public LayerMask validGroundLayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        float fallVelocityY = playerMovement.GetPlayerRb2dLinearVelocity().y;
        if (validGroundLayer == (validGroundLayer | (1 << other.gameObject.layer)) && fallVelocityY < -10f)
        {
            // Handle fall damage logic here
            playerMovement.TakeDamage(Mathf.Abs(fallVelocityY + 10f) * 30);
        }
    }
}
