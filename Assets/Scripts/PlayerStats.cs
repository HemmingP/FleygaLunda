using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private BarHorizontalAmount staminaBar;
    [SerializeField] private BirdsPanel birdsPanel;
    [SerializeField] private float maxStamina = 100f;
    private float currentStamina;
    private int cash = 0;

    public int GetCash()
    {
        return cash;
    }

    public void AddCash(int amount)
    {
        cash += amount;
    }

    public List<BirdInfo> birds;
    public int maxBirdsInInventory = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStamina = maxStamina;

        // Initialize the list of birds
        birds = new List<BirdInfo>();
        if (birdsPanel != null)
        {
            birdsPanel.SetPlayerStats(this);
        }
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

    public bool AddBird(BirdInfo bird)
    {
        if (bird != null && !birds.Contains(bird) && birds.Count < maxBirdsInInventory)
        {
            birds.Add(new BirdInfo { birdWeight = bird.birdWeight });
            if (birdsPanel != null)
            {
                birdsPanel.WriteBirdsInfo();
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

    public BirdsPanel GetBirdsPanel()
    {
        return birdsPanel;
    }

    public void HandleBirdSelection(BirdInfo selectedBird)
    {
        if (selectedBird != null && birds.Contains(selectedBird))
        {
            // Implement logic for handling the selection of a bird
            // For example, you could highlight the selected bird in the UI
            if (playerMovement.IsInStore)
            {
                // Handle the case when the player is in the store
                AddCash(Mathf.FloorToInt(selectedBird.birdWeight * 50)); // Example: add cash based on bird's weight
            }
            else
            {
                // Eat the bird
                // Example: restore stamina based on bird's weight
                currentStamina += Mathf.Clamp(Mathf.FloorToInt(selectedBird.birdWeight * 25), 0, maxStamina);
            }
            birds.Remove(selectedBird);
            if (birdsPanel != null)
            {
                birdsPanel.WriteBirdsInfo();
                EventSystem.current.SetSelectedGameObject(GetBirdsPanel().GetBirdItems()[0]);

                if (birds.Count == 0)
                {
                    playerMovement.GetInputActions().UI.Disable();
                    playerMovement.GetInputActions().Player.Enable();
                }
            }
        }
    }
}
