using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    public CanvasGroup[] pageViews;
    public Button twitterButton;

    private void Awake()
    {
        GoToPageView(0, false);
        pageViews[0].GetOrAddComponent<Image>();
        pageViews[0].GetOrAddComponent<Button>().onClick.AddListener(() => GoToPageView(1));
        pageViews[1].GetOrAddComponent<Image>();
        pageViews[1].GetOrAddComponent<Button>().onClick.AddListener(() => GoToPageView(2));

        twitterButton.onClick.AddListener(() => Application.OpenURL("https://twitter.com/beforedawnnft"));
    }

    private void GoToPageView(int pageViewIdx, bool fade = true)
    {
        if (pageViewIdx < 0) pageViewIdx = 0;
        if (pageViewIdx >= pageViews.Length) pageViewIdx = pageViews.Length - 1;
        foreach (var canvasGroup in pageViews)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

        if (fade) pageViews[pageViewIdx].DOFade(1, 0.5f);
        else pageViews[pageViewIdx].alpha = 1;
        pageViews[pageViewIdx].blocksRaycasts = true;
    }
}