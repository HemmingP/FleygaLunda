using UnityEngine;
using UnityEngine.UI;
public class BirdInPanel : MonoBehaviour
{
    public Image image;
    BirdInfo birdInfo;
    public TMPro.TextMeshProUGUI hungerReplenish;
    public TMPro.TextMeshProUGUI moneyWorth;
    int hungerReplenishValue;
    int moneyWorthValue;
    PlayerStats playerStats;

    public void SetBirdStat(BirdInfo newBirdInfo, PlayerStats playerStats, Sprite newSprite = null)
    {
        image.sprite = newSprite;
        birdInfo = newBirdInfo;
        hungerReplenishValue = Mathf.FloorToInt(birdInfo.birdWeight * 25);
        moneyWorthValue = Mathf.FloorToInt(birdInfo.birdWeight * 50);
        hungerReplenish.text = $"🍗 {hungerReplenishValue}";
        moneyWorth.text = $"💰 {moneyWorthValue}";
        this.playerStats = playerStats;
    }

    public BirdInfo GetBirdStat()
    {
        return birdInfo;
    }

    public void Select()
    {
        if (playerStats != null)
        {
            playerStats.HandleBirdSelection(birdInfo);
        }
    }
}