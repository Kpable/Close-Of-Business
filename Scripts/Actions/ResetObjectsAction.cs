using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Restore a GameObject in the scene to its original transform settings.
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class ResetObjectsAction : Action {

    public GameObject[] objectsToReset;
    List<TransformData> originalTransforms = new List<TransformData>();

    public override void Act()
    {
        ResetObjects();
    }

    void ResetObjects()
    {
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].transform.SetPositionAndRotation(originalTransforms[i].position, originalTransforms[i].rotation);
            objectsToReset[i].transform.localScale = originalTransforms[i].localScale;

            if (objectsToReset[i].GetComponent<Rigidbody>()) objectsToReset[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            TransformData td = new TransformData(
                objectsToReset[i].transform.position,
                objectsToReset[i].transform.rotation,
                objectsToReset[i].transform.localScale);

            originalTransforms.Add(td);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [System.Serializable]
    public class TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 localScale;

        public TransformData(Vector3 position, Quaternion rotation, Vector3 localScale)
        {
            this.position = position;
            this.rotation = rotation;
            this.localScale = localScale;
        }
        
    }
}
