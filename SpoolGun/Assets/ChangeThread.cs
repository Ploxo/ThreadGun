using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeThread : MonoBehaviour
{

    [SerializeField] public Button ice;
    [SerializeField] public Button gum;
    [SerializeField] public List<Material> materials;
    [SerializeField] public List<Material> threadMaterials;
    [SerializeField] private GameObject generator;


    void Start()
    {
        ice.onClick.AddListener(setIce);
        gum.onClick.AddListener(setGum);
    }

    void Update()
    {
        
    }

    private void setIce()
    {
        generator.GetComponent<PolygonGenerator>().setMaterial(materials[0], threadMaterials[0], 0);
    }

    private void setGum()
    {
        generator.GetComponent<PolygonGenerator>().setMaterial(materials[1], threadMaterials[1], 1);
    }
}
