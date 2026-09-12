using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Destination")]
    [SerializeField] public Vector2 Destination;
    [Header("Speed")]
    [SerializeField] public float Speed = 0.5f; 
    [Header("TurnSpeed")]
    [SerializeField] public float TurnSpeed = 0.5f; 
    public float DangerSense = 0f;


    public void SetDestination(Vector2 destination, Vector2 direction)
    {
        Destination = destination;
    }

    private void Observe()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject catcher = GameObject.FindWithTag("NetCatcher");

        DangerSense = player.transform.Y - catcher.transform.Y;
    }


    private void Update()
    {
        Observe();
        Vector2 direction = (Destination - (Vector2)transform.position).normalized;

        if(DangerSense > 0.1)
            direction.Y = 100;

        transform.position += (Vector3)(direction * Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, Destination) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
