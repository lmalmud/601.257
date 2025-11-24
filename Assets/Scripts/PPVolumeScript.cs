using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;

/*
    Author: Teddy Starynski
    Date Created: 10/22/25
    Last Edited: 10/22/25
    Controls the Post Processing effects when damage is taken.

*/

public class PPVolumeScript : MonoBehaviour
{

    [SerializeField] private Volume volume;
    private Vignette vignette;
    private ChromaticAberration chromAb;
    private VolumeProfile profile;
    
    void Awake() {
        EndPointDetection endPointDetector = GameObject.Find("EndPoint").GetComponent<EndPointDetection>();
        endPointDetector.onReachEnd.AddListener(pulseWhenDamage);
    }

    // Start is called before the first frame update
    void Start()
    {
        //vignette = ScriptableObject.CreateInstance<Vignette>();
        profile = volume.sharedProfile;
        profile.TryGet<Vignette>(out vignette); //returns a bool depending of if the thing was found
        profile.TryGet<ChromaticAberration>(out chromAb);

        volume.weight = 0;
    }

    void pulseWhenDamage()
    {
        Debug.Log("pulse when damage");
        vignette.intensity.Override(1.0f);
        chromAb.intensity.Override(1.0f);

        StartCoroutine(fadeEffects(2f));
    }

    IEnumerator fadeEffects(float duration)
    {
        float timer = 0f;

        while (timer < duration){
            timer += Time.deltaTime;
            volume.weight = Mathf.Exp(-timer);
            yield return null;
        }
        volume.weight = 0; //make sure the weight is fully 0 by the end of the fade out
    }

    void OnDestroy() {
        volume.weight = 0; //make sure the effects go away when ending the scene
    }
}
