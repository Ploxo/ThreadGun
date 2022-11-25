using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeThread : MonoBehaviour
{

    [SerializeField] public Button ice;
    [SerializeField] public Button gum;
    [SerializeField] public List<Material> materials;
    [SerializeField] private GameObject generator;
    // Start is called before the first frame update
    void Start()
    {
        ice.onClick.AddListener(setIce);
        gum.onClick.AddListener(setGum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setIce()
    {
        generator.GetComponent<TestGenerateMesh>().setMaterial(materials[0], 2);
    }

    private void setGum()
    {
        generator.GetComponent<TestGenerateMesh>().setMaterial(materials[1], 1);
    }
}
