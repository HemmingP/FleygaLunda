using UnityEditor.Animations;
using UnityEngine;

[System.Serializable]
public class BirdInfo
{
    public float birdWeight;
    public float birdMinWeight;
    public float birdMaxWeight;
    public AnimatorController animatorController;
}

public class BirdStat : MonoBehaviour
{
    public BirdInfo birdInfo;

    void Awake()
    {
        // Initialize bird weight or other properties if needed
        birdInfo = new BirdInfo
        {
            birdWeight = Random.Range(birdInfo.birdMinWeight, birdInfo.birdMaxWeight) // Example initialization, adjust as needed
        };

        Animator animator = GetComponent<Animator>();
        if (animator != null && birdInfo.animatorController != null)
        {
            animator.runtimeAnimatorController = birdInfo.animatorController;
        }
    }
}
