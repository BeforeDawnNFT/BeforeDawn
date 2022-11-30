using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class RemoteVideo : MonoBehaviour
{
    public string videoClipName;

    private void Awake()
    {
        var player = GetComponent<VideoPlayer>();
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoClipName);
    }
}
