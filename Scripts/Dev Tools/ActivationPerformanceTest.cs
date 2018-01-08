using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Created upon request to satisfy the following conditions:
/// Spawn a bunch of cubes. 
/// Toggle a random subset of those cubes
/// Time before and after and output it
/// Rinse and repeat
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class ActivationPerformanceTest : MonoBehaviour {

    public Transform spawnLocation;
    public float repeatRate = 5f;
    public int numberToSpawn = 500;
    public GameObject objectToSpawn;

    private GameObject[] cubes;
    private int iteration;
    private Stopwatch stopWatch;
    private Text uiText;
    private float timeSinceLastFrame = 0;

    private void Awake()
    {
        cubes = new GameObject[numberToSpawn];
        if (!spawnLocation) spawnLocation = transform;

        for (int i = 0; i < numberToSpawn; i++)
        {
            GameObject go = Instantiate(objectToSpawn, spawnLocation.position, Quaternion.identity);
            go.transform.SetParent(spawnLocation);
            go.SetActive(false);
            cubes[i] = go;
        }

        uiText = gameObject.GetComponentInChildren<Text>();
        stopWatch = new Stopwatch();
    }
    // Use this for initialization
    void Start () {
        InvokeRepeating("ToggleState", repeatRate, repeatRate);
	}
	
    void ToggleState()
    {
        float frameLength = Time.time - timeSinceLastFrame;
        frameLength = repeatRate - frameLength;
        UnityEngine.Debug.Log(" time since last frame " + frameLength);

        timeSinceLastFrame = Time.time;

        string message = "";
        iteration++;
        UnityEngine.Debug.Log(name + ": State Toggling Iteration " + iteration);
        message += "State Toggling Iteration " + iteration + "\n";

        stopWatch.Reset();

        int numberOfObjectsToToggleState = Random.Range(100, 400);

        int objectsActivated = 0;
        int objectsDeactivated = 0;

        Stack<GameObject> cubeStack = new Stack<GameObject>();

        while(cubeStack.Count != numberOfObjectsToToggleState)
        {
            GameObject go = cubes[Random.Range(0, numberToSpawn)];
            if (cubeStack.Contains(go)) continue;
            cubeStack.Push(go);
        }

        UnityEngine.Debug.Log(name + ": Toggling " + numberOfObjectsToToggleState + " objects");
        message += "Toggling " + numberOfObjectsToToggleState + " objects\n";
        stopWatch.Start();
        //float startTime = Time.realtimeSinceStartup;

        for (int i = 0; i < numberOfObjectsToToggleState; i++)
        {
            GameObject toggleObject = cubeStack.Pop();
            if (toggleObject.activeInHierarchy)
            {
                objectsDeactivated++;
                toggleObject.SetActive(false);

            }
            else
            {
                objectsActivated++;
                toggleObject.SetActive(true);
            }
        }
        stopWatch.Stop();
        //float endTime = Time.realtimeSinceStartup - startTime;

        //message += "Total duration: " + stopWatch.Elapsed + "\n";
        message += "Total duration: " + frameLength + "\n";

        //UnityEngine.Debug.Log(name + ": Total duration: " + endTime);
        //UnityEngine.Debug.Log(name + ": Total duration: " + stopWatch.Elapsed);
        UnityEngine.Debug.Log(name + ": Objects Activated: " + objectsActivated);
        UnityEngine.Debug.Log(name + ": Objects Deactivated: " + objectsDeactivated);
        UnityEngine.Debug.Log(" ");

        uiText.text = message;      

    }

	// Update is called once per frame
	void Update () {
		
	}
}
