using UnityEngine;

[System.Serializable]
public class BirdInfo
{
    public float birdWeight;
}

public class BirdStat : MonoBehaviour
{
    public BirdInfo birdInfo;

    void Awake()
    {
        // Initialize bird weight or other properties if needed
        birdInfo = new BirdInfo
        {
            birdWeight = Random.Range(1f, 3f) // Example initialization, adjust as needed
        };
    }
}
