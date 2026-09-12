using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BirdsPanel : MonoBehaviour, InputSystem_Actions.IUIActions
{
    [SerializeField] private TMPro.TMP_Text birdsAmount;
    [SerializeField] private Transform birdsListContainer;
    [SerializeField] private GameObject birdItemPrefab;
    private PlayerStats playerStats;
    private InputSystem_Actions inputActions;
    private List<GameObject> birdItems = new List<GameObject>();
    public GameObject[] GetBirdItems()
    {
        return birdItems.ToArray();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputActions.UI.Disable();
            inputActions.Player.Enable();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void OnClick(InputAction.CallbackContext context) { }

    public void OnMiddleClick(InputAction.CallbackContext context) { }

    public void OnNavigate(InputAction.CallbackContext context) { }

    public void OnPoint(InputAction.CallbackContext context) { }

    public void OnRightClick(InputAction.CallbackContext context) { }

    public void OnScrollWheel(InputAction.CallbackContext context) { }

    public void OnSubmit(InputAction.CallbackContext context) { }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }

    public void WriteBirdsInfo()
    {
        birdsAmount.text = playerStats.birds.Count.ToString() + " / " + playerStats.maxBirdsInInventory.ToString();

        foreach (Transform child in birdsListContainer)
        {
            Destroy(child.gameObject);
        }
        birdItems.Clear();

        foreach (var birdInfo in playerStats.birds)
        {
            GameObject birdItem = Instantiate(birdItemPrefab, birdsListContainer);
            birdItems.Add(birdItem);
            BirdInPanel birdInPanel = birdItem.GetComponent<BirdInPanel>();
            if (birdInPanel != null)
            {
                birdInPanel.SetBirdStat(birdInfo, playerStats, null);
            }
        }
    }

    public void SetInputActions(InputSystem_Actions actions)
    {
        inputActions = actions;
        inputActions.UI.SetCallbacks(this);
    }

    public void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }
}
