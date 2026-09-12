using UnityEngine;
using UnityEngine.UI;
public class BirdInPanel : MonoBehaviour
{
    public Image image;
    BirdStat birdStat;
    public TMPro.TextMeshProUGUI hungerReplenish;
    public TMPro.TextMeshProUGUI moneyWorth;

    public void SetImage(Sprite newSprite)
    {
        image.sprite = newSprite;
    }

    public void SetBirdStat(BirdStat newBirdStat)
    {
        birdStat = newBirdStat;
        hungerReplenish.text = $"🍗 {birdStat.birdWeight * 25}";
        moneyWorth.text = $"💰 {birdStat.birdWeight * 50}";
    }

    public BirdStat GetBirdStat()
    {
        return birdStat;
    }
}
