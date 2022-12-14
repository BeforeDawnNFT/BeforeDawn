using DanielLochner.Assets.SimpleScrollSnap;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    public GameObject[] pageViews;
    public Button mintButton;
    public Button lightPaperButton;
    public Button twitterButton;
    public SimpleScrollSnap scrollSnap;

    private void Awake()
    {
        pageViews[0].GetOrAddComponent<Image>();
        pageViews[0].GetOrAddComponent<Button>().onClick.AddListener(() => scrollSnap.GoToPanel(1));
        pageViews[1].GetOrAddComponent<Image>();
        pageViews[1].GetOrAddComponent<Button>().onClick.AddListener(() => scrollSnap.GoToPanel(0));
        
        mintButton.onClick.AddListener(() => Application.OpenURL("https://mooar.com/vote"));
        lightPaperButton.onClick.AddListener(() => Application.OpenURL("https://lightpaper.beforedawn.xyz"));
        twitterButton.onClick.AddListener(() => Application.OpenURL("https://twitter.com/beforedawnnft"));
    }

    private void Update()
    {
        if(Input.mouseScrollDelta.y < 0) scrollSnap.GoToPreviousPanel();
        if(Input.mouseScrollDelta.y > 0) scrollSnap.GoToNextPanel();
    }
}