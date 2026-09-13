using UnityEngine;

public class MoneyPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI moneyText;

    public void SetMoney(int amount)
    {
        if (moneyText != null)
        {
            moneyText.text = $"💰 {amount}";
        }
    }
}
