using UnityEngine;

public class ViewController : MonoBehaviour
{
    public CanvasGroup[] pageViews;

    private void Awake()
    {
        GoToPageView(0);
    }

    public void GoToPageView(int pageViewIdx)
    {
        if (pageViewIdx < 0) pageViewIdx = 0;
        if (pageViewIdx >= pageViews.Length) pageViewIdx = pageViews.Length - 1;
        foreach (var go in pageViews) go.alpha = 0;
        pageViews[pageViewIdx].alpha = 1;
    }
}