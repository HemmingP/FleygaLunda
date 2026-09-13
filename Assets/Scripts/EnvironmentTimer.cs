using System.Collections;
using UnityEngine;
[System.Serializable]
public class AfterProcentGone
{
    [Range(0f, 1f)]
    public float procent = 0.5f; // Example: 50% of the time has passed
    public UnityEngine.Events.UnityEvent onAfterProcentGone;
}
public class EnvironmentTimer : MonoBehaviour
{
    public float gameTimeLimit = 300f;

    private float currentTime = 0f;
    bool timeLimitReached = false;
    public UnityEngine.Events.UnityEvent onTimeLimitReached;
    public bool AffectMainCameraBackgroundColor = false;
    Color originalCameraBackgroundColor;
    public AfterProcentGone[] afterProcentGones;

    void Start()
    {
        if (AffectMainCameraBackgroundColor)
        {
            originalCameraBackgroundColor = Camera.main.backgroundColor;
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= gameTimeLimit && !timeLimitReached)
        {
            // Handle the environment timer reaching the limit
            timeLimitReached = true;
            onTimeLimitReached?.Invoke();
        }
        if (AffectMainCameraBackgroundColor && !timeLimitReached)
        {
            // Make the camera background transition over to black
            Camera.main.backgroundColor = Color.Lerp(originalCameraBackgroundColor, Color.black, currentTime / gameTimeLimit);
        }
    }
    void OnEnable()
    {
        foreach (var afterProcentGone in afterProcentGones)
        {
            StartCoroutine(AfterProcentGoneCoroutine(afterProcentGone));
        }
    }

    IEnumerator AfterProcentGoneCoroutine(AfterProcentGone afterProcentGone)
    {
        yield return new WaitUntil(() => currentTime / gameTimeLimit >= afterProcentGone.procent);
        afterProcentGone.onAfterProcentGone?.Invoke();
    }
}
