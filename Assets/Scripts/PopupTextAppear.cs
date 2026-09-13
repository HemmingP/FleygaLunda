using System.Collections;
using UnityEngine;

public class PopupTextAppear : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textMeshProUGUI;
    bool hidingInProgress;
    float hideTimer;
    [SerializeField] float showDuration = 4f;
    [SerializeField] float fadeDuration = 0.5f;
    float currentFadeDuration;

    public void SetDuration(float showDuration = 4f)
    {
        this.showDuration = showDuration;
    }

    public void SetFadeDuration(float fadeDuration = 0.5f)
    {
        this.fadeDuration = fadeDuration;
    }

    public void ShowText(string text)
    {
        gameObject.SetActive(true);
        textMeshProUGUI.text = text;
        textMeshProUGUI.alpha = 1f;
        hideTimer = 0;
    }

    private void Update()
    {
        hideTimer += Time.deltaTime;
        if (hideTimer >= showDuration && !hidingInProgress)
        {
            hidingInProgress = true;
            currentFadeDuration = fadeDuration;
        }
        // Optional: Implement any per-frame logic for the popup text here
        if (hidingInProgress)
        {
            currentFadeDuration -= Time.deltaTime;
            if (currentFadeDuration <= 0f)
            {
                textMeshProUGUI.alpha = 0f;
                hidingInProgress = false;
                gameObject.SetActive(false);
            }
            else
            {
                textMeshProUGUI.alpha = currentFadeDuration / this.fadeDuration;
            }
        }
    }
}
