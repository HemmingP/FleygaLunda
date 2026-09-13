using UnityEngine;
using System.Collections;

public class BirdSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] sounds;

    public float minDelay = 3f;
    public float maxDelay = 8f;

    void Start()
    {
        StartCoroutine(PlayRandomSounds());
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            // Wait for a random amount of time
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            // Pick a random sound
            if (sounds.Length > 0)
            {
                int randomIndex = Random.Range(0, sounds.Length);
                audioSource.PlayOneShot(sounds[randomIndex]);
            }
        }
    }
}
