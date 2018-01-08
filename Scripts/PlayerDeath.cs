using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// Handling the player's death
/// 
/// Created By: Kpable
/// Date Created: 07-07-17
/// 
/// </summary>
public class PlayerDeath : MonoBehaviour {

    private Health health;

    public GameObject sign;

    // Use this for initialization
    void Start () {
        health = GetComponent<Health>();
        health.zeroHealthAlert += Die;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine("CaptureScene");
        }
	}

    void Die()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator CaptureScene()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("Capturing Scene");
        Texture2D texture = new Texture2D(Screen.width, Screen.height);

        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //texture.Resize(Mathf.RoundToInt(sign.transform.localScale.x), Mathf.RoundToInt(sign.transform.localScale.y));
        texture.Apply();

        sign.SetActive(true);
        sign.GetComponent<Renderer>().material.mainTexture = texture;
    }
}
