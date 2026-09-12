using UnityEngine;
using System.Collections.Generic;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private BarHorizontalAmount staminaBar;
    [SerializeField] private BirdsPanel birdsPanel;
    [SerializeField] private float maxStamina = 100f;
    private float currentStamina;

    public List<BirdStat> birds;
    public int maxBirdsInInventory = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStamina = maxStamina;

        // Initialize the list of birds
        birds = new List<BirdStat>();
    }

    // Update is called once per frame
    void Update()
    {
        currentStamina -= Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        if (staminaBar != null)
        {
            staminaBar.SetBarAmount(currentStamina / maxStamina);
        }
    }

    public bool AddBird(BirdStat bird)
    {
        if (bird != null && !birds.Contains(bird) && birds.Count < maxBirdsInInventory)
        {
            birds.Add(bird);
            if (birdsPanel != null)
            {
                birdsPanel.WriteBirdsInfo(birds.ToArray(), maxBirdsInInventory);
            }
            if (birds.Count >= maxBirdsInInventory)
            {
                if (playerMovement.GetPlayerState() == PlayerState.Fleyging)
                {
                    // Toggle back to walking state if inventory is full
                    playerMovement.SwitchState(PlayerState.Walking);
                }
            }
            return true;
        }
        return false;
    }
}
