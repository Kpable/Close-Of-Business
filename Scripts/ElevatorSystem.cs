using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// 
/// The Elevator system
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class ElevatorSystem : MonoBehaviour {

    [Header("Elevator Settings")]
    public Transform elevator;
    public float speed = 10f; 
    [Header("Elevator Panel")]
    public GameObject[] panelButtons;
    [Header("Floors")]
    public Floor[] floors;
    public int defaultFloor = 1;

    [Header("Dev View")]
    [SerializeField]
    int currentFloor;
    [SerializeField]
    int targetFloor;

    Transform leftDoor1, leftDoor2, leftDoor3, rightDoor1, rightDoor2, rightDoor3;
    private Sequence doorAnimation;

    // Use this for initialization
    void Start () {
        defaultFloor = Mathf.Clamp(defaultFloor, 0, floors.Length);
        currentFloor = targetFloor = defaultFloor;

        //Debug.Log("floors " + floors.Length);

        leftDoor1 = transform.Find("Door_Left_1");
        leftDoor2 = transform.Find("Door_Left_2");
        leftDoor3 = transform.Find("Door_Left_3");

        rightDoor1 = transform.Find("Door_Right_1");
        rightDoor2 = transform.Find("Door_Right_2");
        rightDoor3 = transform.Find("Door_Right_3");

        doorAnimation = DOTween.Sequence();
        doorAnimation.Pause();

        doorAnimation
            .Append(leftDoor3.DOLocalMoveX(leftDoor2.localPosition.x, 0.5f))
            .Join(rightDoor3.DOLocalMoveX(rightDoor2.localPosition.x , 0.5f))

            .Append(leftDoor3.DOLocalMoveX(leftDoor1.localPosition.x, 0.5f))
            .Join(rightDoor3.DOLocalMoveX(rightDoor1.localPosition.x, 0.5f))
            .Join(leftDoor2.DOLocalMoveX(leftDoor1.localPosition.x, 0.5f))
            .Join(rightDoor2.DOLocalMoveX(rightDoor1.localPosition.x, 0.5f))

            .Append(leftDoor3.DOLocalMoveX(leftDoor1.localPosition.x + .2257f, 0.5f))
            .Join(rightDoor3.DOLocalMoveX(rightDoor1.localPosition.x - .2257f, 0.5f))
            .Join(leftDoor2.DOLocalMoveX(leftDoor1.localPosition.x + .2257f, 0.5f))
            .Join(rightDoor2.DOLocalMoveX(rightDoor1.localPosition.x - .2257f, 0.5f))
            .Join(leftDoor1.DOLocalMoveX(leftDoor1.localPosition.x + .2257f, 0.5f))
            .Join(rightDoor1.DOLocalMoveX(rightDoor1.localPosition.x - .2257f, 0.5f))

            .SetAutoKill(false);

        OpenDoor();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (targetFloor != currentFloor && !doorAnimation.IsPlaying())
        {
            Vector3 pos = elevator.position;
            float yDelta = 0.1f;
            if (Mathf.Abs(pos.y - floors[targetFloor].floorPositionY) < 0.1f)
            {
                yDelta = ((floors[currentFloor].floorPositionY < floors[targetFloor].floorPositionY) ? 1 : -1) * Mathf.Abs(pos.y - floors[targetFloor].floorPositionY);
            }
            else
            {
                yDelta = ((floors[currentFloor].floorPositionY < floors[targetFloor].floorPositionY) ? 0.1f : -0.1f);
            }

            Vector3 offset = new Vector3(0, yDelta, 0) * Time.fixedDeltaTime * speed;
            pos += offset;
            pos.y = Mathf.Clamp(pos.y, (floors[currentFloor].floorPositionY < floors[targetFloor].floorPositionY) ? floors[currentFloor].floorPositionY : floors[targetFloor].floorPositionY,
                (floors[currentFloor].floorPositionY < floors[targetFloor].floorPositionY) ? floors[targetFloor].floorPositionY : floors[currentFloor].floorPositionY);
            elevator.position = pos;

            if (Mathf.Approximately(elevator.position.y, floors[targetFloor].floorPositionY))
            {
                //GameObject.Find("Player").transform.SetParent(null, false);
                currentFloor = targetFloor;
                OpenDoor();

            }
        }
        
	}

    public void GoToFloor(int floor)
    {
        if (currentFloor == floor)
        {
            Debug.Log("Already on this floor " + floor);
        }
        else if (currentFloor != targetFloor)
        {
            Debug.Log("Elevator already moving");
        }
        else
        {
            Debug.Log("Going to floor " + floor);
            //GameObject.Find("Player").transform.SetParent(elevator, false);
            CloseDoor();
            targetFloor = floor;
            Debug.Log(floors[floor].floorPositionY);
        }
    }

    public void CallElevator(int floor)
    {
        GoToFloor(floor);
    }
    
    void OpenDoor()
    {
        doorAnimation.PlayForward();
    }

    void CloseDoor()
    {
        doorAnimation.PlayBackwards();

    }
}

[System.Serializable]
public class Floor {
    public float floorPositionY;
    public GameObject callButton;
}
