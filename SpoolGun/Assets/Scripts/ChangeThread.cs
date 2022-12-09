using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeThread : MonoBehaviour
{

    [SerializeField] public Button ice;
    [SerializeField] public Button gum;
    [SerializeField] public List<Material> materials;
    [SerializeField] public List<Material> liningMaterials;
    [SerializeField] public List<PhysicMaterial> physicMaterials;
    [SerializeField] private GameObject[] patchParticles;
    [SerializeField] private GameObject[] mouseParticles;
    [SerializeField] private GameObject generator;
    [SerializeField] private GameObject mouseTracker;


    void Start()
    {
        ice.onClick.AddListener(setIce);
        gum.onClick.AddListener(setGum);

        setIce();
    }

    private void setIce()
    {
        mouseTracker.GetComponent<MouseTracker>().SetMaterial(0);
        generator.GetComponent<PolygonGenerator>().setMaterial(
            0, materials[0], liningMaterials[0], physicMaterials[0], mouseParticles[0], patchParticles[0]);
    }

    private void setGum()
    {
        mouseTracker.GetComponent<MouseTracker>().SetMaterial(1);
        generator.GetComponent<PolygonGenerator>().setMaterial(
            1, materials[1], liningMaterials[1], physicMaterials[1], mouseParticles[1], patchParticles[1]);
    }
}
