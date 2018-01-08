using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 
/// Fade after delay. 
/// Uses DoTween lib.
/// 
/// Created By: Kpable
/// Date Created: 08-06-17
/// 
/// </summary>
public class FadeAfterDelay : MonoBehaviour {

    [Tooltip("Seconds before this object Fades away.")]
    public float delay = 2f; // seconds
    [Tooltip("Duration of fade")]
    public float fadeDuration = 0.5f; // speed object fades away

	void Start () {
        Invoke("FadeAway", delay);	
	}

    public void FadeAway()
    {
        gameObject.GetComponent<CanvasGroup>().DOFade(0, fadeDuration).SetDelay(transform.childCount * fadeDuration)
            .OnComplete(() => { gameObject.SetActive(false); });

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Image>())
                transform.GetChild(i).GetComponent<Image>().DOFade(0, fadeDuration).SetDelay(i * fadeDuration);
            else if (transform.GetChild(i).GetComponent<Text>())
                transform.GetChild(i).GetComponent<Text>().DOFade(0, fadeDuration).SetDelay(i * fadeDuration);
        }
    }
}
