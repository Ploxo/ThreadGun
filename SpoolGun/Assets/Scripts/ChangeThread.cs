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
    [SerializeField] private GameObject generator;
    [SerializeField] private GameObject[] patchParticles;
    [SerializeField] private GameObject[] mouseParticles;


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
        generator.GetComponent<PolygonGenerator>().setMaterial(
            0, materials[0], liningMaterials[0], mouseParticles[0].GetComponent<ParticleSystem>(), patchParticles[0].GetComponent<ParticleSystem>());
    }

    private void setGum()
    {
        generator.GetComponent<PolygonGenerator>().setMaterial(
            1, materials[1], liningMaterials[1], mouseParticles[1].GetComponent<ParticleSystem>(), patchParticles[1].GetComponent<ParticleSystem>());
    }
}
