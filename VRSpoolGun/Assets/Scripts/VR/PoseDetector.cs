using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseDetector : MonoBehaviour
{
    public List<ActiveStateSelector> poses;
    public TMPro.TextMeshProUGUI text;

    void Start()
    {
        foreach (var item in poses)
        {
            item.WhenSelected += () => SetTextToPoseName(item.gameObject.name);
            item.WhenUnselected += () => SetTextToPoseName("No Pose detected");
        }
    }

    private void SetTextToPoseName(string newText)
    {
        Debug.Log("Detected " + newText);
        text.text = newText;
    }
}
