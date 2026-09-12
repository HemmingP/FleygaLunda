using UnityEngine;

public class BirdStat : MonoBehaviour
{
    public float birdWeight;

    void Awake()
    {
        // Initialize bird weight or other properties if needed
        birdWeight = Random.Range(1f, 10f); // Example initialization, adjust as needed
    }
}
