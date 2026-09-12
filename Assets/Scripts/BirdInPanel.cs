using UnityEngine;
using UnityEngine.UI;
public class BirdInPanel : MonoBehaviour
{
    public Image image;
    BirdStat birdStat;
    public TMPro.TextMeshProUGUI hungerReplenish;
    public TMPro.TextMeshProUGUI moneyWorth;
    int hungerReplenishValue;
    int moneyWorthValue;

    public void SetImage(Sprite newSprite)
    {
        image.sprite = newSprite;
    }

    public void SetBirdStat(BirdStat newBirdStat)
    {
        birdStat = newBirdStat;
        hungerReplenishValue = Mathf.FloorToInt(birdStat.birdWeight * 25);
        moneyWorthValue = Mathf.FloorToInt(birdStat.birdWeight * 50);
        hungerReplenish.text = $"🍗 {hungerReplenishValue}";
        moneyWorth.text = $"💰 {moneyWorthValue}";
    }

    public BirdStat GetBirdStat()
    {
        return birdStat;
    }
}
