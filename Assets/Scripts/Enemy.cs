using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Destination")]
    [SerializeField] public Vector2 Destination;

    [Header("Speed")]
    [SerializeField] public float Speed = 2.0f;

    [Header("Turn Speed")]
    [SerializeField] public float TurnSpeed = 60f;

    [Header("Danger")]
    [SerializeField] public float DangerSense = 0f;

    [Header("Panic")]
    [SerializeField] public float PanicSense = 0f;

    public bool isFowardFacing = true;
    private bool Danger = false;
    private bool Panic = false;

    public Vector2 currentDirection;


    public void SetDestination(Vector2 destination, Vector2 direction)
    {
        Destination = destination;
        currentDirection = direction.normalized;
    }


    private void Observe()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject catcher = GameObject.FindWithTag("NetCatcher");

        if (player != null && catcher != null)
        {
            DangerSense = catcher.transform.position.y - player.transform.position.y;
            PanicSense = Vector3.Distance(catcher.transform.position, transform.position);
        }
        Panic = PanicSense < 2.5;
        Danger = DangerSense > 0.5f;
    }


    private void Update()
    {
        Observe();
        Vector2 targetDirection = Destination;
        Vector2 dangerDestination = Destination + new Vector2(0f, 10f);
        Vector2 escapeDestination = Destination + new Vector2(0f, 1000f);

        if (Danger) // Go towards a point far above the destination.
        {
            targetDirection = (dangerDestination - (Vector2)transform.position).normalized;
        }
        else
            targetDirection = (Destination - (Vector2)transform.position).normalized;  // Go towards the normal destination.

        if (Panic) // GTFO
            targetDirection = (escapeDestination - (Vector2)transform.position).normalized;

        // Gradually turn towards the destination.
        currentDirection = (Vector2)Vector3.RotateTowards(
            currentDirection,
            targetDirection,
            TurnSpeed * Mathf.Deg2Rad * Time.deltaTime,
            0f
        );

        if (targetDirection.x < transform.position.x)
            isFowardFacing = false;
        else
            isFowardFacing = true;

        // Move forward.
        transform.position += (Vector3)(currentDirection * Speed * Time.deltaTime);

        // Destroy when destination is reached.
        if (Vector2.Distance(transform.position, Destination) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
