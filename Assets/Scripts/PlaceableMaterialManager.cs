using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableMaterialManager : MonoBehaviour
{
    
    [SerializeField] Material previewMaterial;
    [SerializeField] Material placedMaterial;
    
    public ParticleSystem placeEffect;


    // 0 = transparent/preview
    // 1 = opaque/placed
    public void setMaterial(int matType)
    {
        gameObject.GetComponent<Renderer>().material = matType == 1 ? placedMaterial : previewMaterial;
    }

    public void playPlaceParticle()
    {
        Debug.Log("play effect of tower placing");
        placeEffect.Play();
    }

}
