using DanielLochner.Assets.SimpleScrollSnap;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    public GameObject[] pageViews;
    public Button twitterButton;
    public SimpleScrollSnap scrollSnap;

    private void Awake()
    {
        // GoToPageView(0, false);
        pageViews[0].GetOrAddComponent<Image>();
        pageViews[0].GetOrAddComponent<Button>().onClick.AddListener(() => scrollSnap.GoToPanel(1));
        pageViews[1].GetOrAddComponent<Image>();
        pageViews[1].GetOrAddComponent<Button>().onClick.AddListener(() => scrollSnap.GoToPanel(0));
        
        twitterButton.onClick.AddListener(() => Application.OpenURL("https://twitter.com/beforedawnnft"));
    }
}