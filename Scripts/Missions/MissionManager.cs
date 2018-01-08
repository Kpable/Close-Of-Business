using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 
/// Mission
/// 
/// Created By: Kpable
/// Date Created: 08-20-17
/// 
/// </summary>
public class MissionManager : MonoBehaviour {

    public Mission[] missions;
    public Text missionText;
    public GameObject objectivePrefab;
    public Transform objectiveContainer;


    public static string heldObjectTag = "";

    private Mission currentMission;
    private int currentMissionIndex = 0;
    private bool insideZone = false;
    private List<string> zonesInside = new List<string>();

    void Start () {
        currentMission = missions[currentMissionIndex];

        DestructableObject.OnBreakObject += ObjectBreak;
        DestructableObject.OnHitObject += ObjectHit;

        Zones.OnEnterZone += EnterZone;
        Zones.OnExitZone += ExitZone;

        //missionText.text = currentMisison.ToString();
        missionText.text = currentMission.missionDescription;

        UpdateObjectiveView();

        currentMission.OnObjectiveComplete += HandleObjectiveCompleted;
        currentMission.OnMissionComplete += HandleMissionCompleted;
        
    }

    public void  ObjectBreak(DestructableObject obj)
    {
        Debug.Log(obj.name + " destroyed");
        currentMission.CheckMissionObjectives(obj.gameObject.tag, zonesInside, ObjectiveType.Break);
    }

    public void ObjectHit(DestructableObject obj)
    {
        Debug.Log(obj.name + " hit");
        currentMission.CheckMissionObjectives(obj.gameObject.tag, zonesInside, ObjectiveType.Kick);
    }

    public void EnterZone(string zoneName)
    {
        Debug.Log("Player has entered" + zoneName);
        zonesInside.Add(zoneName);
        insideZone = true;
        currentMission.CheckMissionObjectives(heldObjectTag, zonesInside, ObjectiveType.GoTo);
    }

    public void ExitZone(string zoneName)
    {
        Debug.Log("Player has exited" + zoneName);
        zonesInside.Remove(zoneName);
        insideZone = false;
        currentMission.CheckMissionObjectives(heldObjectTag, zonesInside, ObjectiveType.Exit);
    }

    private void UpdateObjectiveView()
    {
        for (int j = 0; j < objectiveContainer.childCount; j++)
        {
            Destroy(objectiveContainer.GetChild(j).gameObject);
        }

        int opacityModifier = 0;

        for (int i = 0; i < currentMission.objectives.Length; i++)
        {
            GameObject ot = Instantiate(objectivePrefab);
            ot.transform.SetParent(objectiveContainer, false);

            if (currentMission.objectives[i].complete)
            {
                ot.SetActive(false);
                continue;
            }

            Text t = ot.GetComponent<Text>();
            t.text = currentMission.objectives[i].ToString();
            if (currentMission.type != MissionType.Free_For_All)
                t.color = new Color(t.color.r, t.color.g, t.color.b, Mathf.Clamp01(1 - (.25f * opacityModifier)));

            opacityModifier++;
            
        }
    }

    public void HandleObjectiveCompleted(int i)
    {
        if (i != -1 && i < objectiveContainer.childCount)
        {
            
            objectiveContainer
                .GetChild(i)
                .GetComponent<Text>()
                .DOFade(0, 1f)
                .OnComplete(() =>
                {
                    objectiveContainer.GetChild(i).gameObject.SetActive(false);
                    if (currentMission.type != MissionType.Free_For_All)
                        UpdateObjectiveView();
                });
        }
    }

    public void HandleMissionCompleted()
    {
        currentMission.OnObjectiveComplete -= HandleObjectiveCompleted;
        currentMission.OnMissionComplete -= HandleMissionCompleted;

        currentMissionIndex++;

        //objectiveContainer.GetComponent<CanvasGroup>()


        if (currentMissionIndex >= missions.Length)
        {
            // All mission Completed
            missionText.text = "Success!";
            objectiveContainer.gameObject.SetActive(false);
        }
        else
        {
            currentMission = missions[currentMissionIndex];

            missionText.text = currentMission.missionDescription;
            UpdateObjectiveView();

            currentMission.OnObjectiveComplete += HandleObjectiveCompleted;
            currentMission.OnMissionComplete += HandleMissionCompleted;
        }

    }
}
