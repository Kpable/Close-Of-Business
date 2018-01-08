using UnityEngine;

/// <summary>
/// 
/// Self Destruct After Delay
/// 
/// Created By: Kpable
/// Date Created: 08-06-17
/// 
/// </summary>
public class SelfDestructAfterDelay : MonoBehaviour {

    [Tooltip("Seconds before this object dissapears.")]
    public float delay = 2f; // seconds

    void Start()
    {
        Invoke("SelfDestruct", delay);
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
