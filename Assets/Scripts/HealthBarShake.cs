using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Author: Teddy Starynski
    Date Created: 11/25/25
    Last Edited: 11/26/25
    A script to make the health bar shake when damage has been taken
*/

public class HealthBarShake : MonoBehaviour
{
    private Vector3 basePosition;
    private RectTransform rectTnsfm;
    [SerializeField] private float shakeDuration = 0.5f;

    void Start()
    {
        rectTnsfm = (RectTransform)transform;
        basePosition = rectTnsfm.anchoredPosition;
        GameManager.instance.onHealthChange.AddListener(ShakeOnDamage);
    }

    void ShakeOnDamage()
    {
        Debug.Log("shake repeating called");

        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            rectTnsfm.anchoredPosition = basePosition + (Vector3.right * 10.0f);
            rectTnsfm.anchoredPosition = basePosition + (Vector3.left * 10.0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTnsfm.anchoredPosition = basePosition; //reset to initial position
    }

    void OnDestroy()
    {
        GameManager.instance.onHealthChange.RemoveListener(ShakeOnDamage);
    }
}
