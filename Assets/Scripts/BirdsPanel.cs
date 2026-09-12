using UnityEngine;

public class BirdsPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text birdsAmount;
    [SerializeField] private Transform birdsListContainer;
    [SerializeField] private GameObject birdItemPrefab;

    public void WriteBirdsAmount(int amount, int maxAmount)
    {
        birdsAmount.text = amount.ToString() + " / " + maxAmount.ToString();
    }

    public void WriteBirdsList(BirdStat[] birdStats)
    {
        foreach (Transform child in birdsListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var birdStat in birdStats)
        {
            GameObject birdItem = Instantiate(birdItemPrefab, birdsListContainer);
            TMPro.TMP_Text birdText = birdItem.GetComponent<TMPro.TMP_Text>();
            if (birdText != null)
            {
                birdText.text = birdStat.name;
            }
        }
    }
}
