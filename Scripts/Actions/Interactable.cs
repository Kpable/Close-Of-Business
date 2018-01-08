using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Set gameobject as interactable in the world. 
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class Interactable : MonoBehaviour {

    public Action action;
    public float range = 10f;
    Material mat;
    Color originalColor;
    [HideInInspector]
    public bool highlighted = false;

	// Use this for initialization
	void Start () {

        if (!action) action = GetComponent<Action>();
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;

        SphereCollider sc = GetComponent<SphereCollider>();

        if (sc == null)
        {
            sc = gameObject.AddComponent<SphereCollider>();
        }

        sc.radius = range;
        sc.isTrigger = true;

    }

    // Update is called once per frame
    void Update () {
        if (highlighted)
        {
            if(mat.color != Color.cyan) mat.color = Color.cyan;
            if (Input.GetKeyDown(KeyCode.F))
            {
                action.Act();
            }
        }
        else
            if (mat.color != originalColor) mat.color = originalColor;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<Interaction>().nearbyInteractableObjects.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<Interaction>().nearbyInteractableObjects.Remove(this);
            highlighted = false;
        }
    }
}
