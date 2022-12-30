using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassthroughManager : MonoBehaviour
{
    public OVRPassthroughLayer passthrough; // The actual passthrough component
    //public OVRInput.Button button; // Which button to check
    //public OVRInput.Controller controller; // The controller to check for buttons
    public List<Gradient> colorMapGradients; // These are supposed to change the texture into a gradient
    public Image edgePreview; // Shows which color the edges are using for edge rendering


    void Update()
    {
        //if (OVRInput.GetDown(button, controller))
        //{
        //    Debug.Log("Passthrough set to " + !passthrough.hidden);
        //    passthrough.hidden = !passthrough.hidden;
        //}
    }

    public void SetOpacity(float value)
    {
        passthrough.textureOpacity = value;
    }

    public void SetColorMapGradient(int index)
    {
        passthrough.colorMapEditorGradient = colorMapGradients[index];
    }

    public void SetBrightness(float value)
    {
        passthrough.colorMapEditorBrightness = value;
    }

    public void SetContrast(float value)
    {
        passthrough.colorMapEditorContrast = value;
    }

    public void SetPosterize(float value)
    {
        passthrough.colorMapEditorPosterize = value;
    }

    public void SetEdgeRendering(bool value)
    {
        passthrough.edgeRenderingEnabled = value;
    }

    public void SetEdgeRed(float value)
    {
        Color newColor = new Color(value, passthrough.edgeColor.g, passthrough.edgeColor.b);
        passthrough.edgeColor = newColor;
        edgePreview.color = newColor;
    }

    public void SetEdgeGreen(float value)
    {
        Color newColor = new Color(passthrough.edgeColor.r, value, passthrough.edgeColor.b);
        passthrough.edgeColor = newColor;
        edgePreview.color = newColor;
    }

    public void SetEdgeBlue(float value)
    {
        Color newColor = new Color(passthrough.edgeColor.r, passthrough.edgeColor.g, value);
        passthrough.edgeColor = newColor;
        edgePreview.color = newColor;
    }
}
