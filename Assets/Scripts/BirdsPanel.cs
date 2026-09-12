using UnityEngine;
using UnityEngine.InputSystem;

public class BirdsPanel : MonoBehaviour, InputSystem_Actions.IUIActions
{
    [SerializeField] private TMPro.TMP_Text birdsAmount;
    [SerializeField] private Transform birdsListContainer;
    [SerializeField] private GameObject birdItemPrefab;

    public void OnCancel(InputAction.CallbackContext context) { }

    public void OnClick(InputAction.CallbackContext context) { }

    public void OnMiddleClick(InputAction.CallbackContext context) { }

    public void OnNavigate(InputAction.CallbackContext context) { }

    public void OnPoint(InputAction.CallbackContext context) { }

    public void OnRightClick(InputAction.CallbackContext context) { }

    public void OnScrollWheel(InputAction.CallbackContext context) { }

    public void OnSubmit(InputAction.CallbackContext context) { }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }

    public void WriteBirdsInfo(BirdStat[] birdStats, int maxAmount)
    {
        birdsAmount.text = birdStats.Length.ToString() + " / " + maxAmount.ToString();

        foreach (Transform child in birdsListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var birdStat in birdStats)
        {
            GameObject birdItem = Instantiate(birdItemPrefab, birdsListContainer);
            BirdInPanel birdInPanel = birdItem.GetComponent<BirdInPanel>();
            if (birdInPanel != null)
            {
                birdInPanel.SetBirdStat(birdStat);
            }
        }
    }
}
