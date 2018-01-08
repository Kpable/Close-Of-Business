using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Call the elevator. 
/// 
/// Created By: Kpable
/// Date Created: 06-22-17
/// 
/// </summary>
public class ElevatorButtonAction : Action {
    public int floor = 0;
    public ElevatorSystem elevatorSystem;

    public override void Act()
    {
        if(elevatorSystem)
            elevatorSystem.GoToFloor(floor);
    }

}
