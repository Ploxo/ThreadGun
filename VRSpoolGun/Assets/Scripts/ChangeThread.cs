using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeThread : MonoBehaviour
{
    [SerializeField] public Button ice;
    [SerializeField] public Button gum;


    void Start()
    {
        ice.onClick.AddListener(setIce);
        gum.onClick.AddListener(setGum);

        setIce();
    }

    private void setIce()
    {
        ThreadManager.Instance.SetActiveThread(ThreadType.Ice);
    }

    private void setGum()
    {
        ThreadManager.Instance.SetActiveThread(ThreadType.Gum);
    }
}
