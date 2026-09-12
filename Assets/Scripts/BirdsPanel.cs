using UnityEngine;

public class BirdsPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text birdsAmount;
    [SerializeField] private Transform birdsListContainer;
    [SerializeField] private GameObject birdItemPrefab;

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
