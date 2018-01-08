using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 
/// Camera Turret
/// 
/// Created By: Kpable
/// Date Created: 06-11-17
/// 
/// </summary>
public class CameraTurret : MonoBehaviour, IChildEventListener{

    [Header("Camera Settings")]
    [Tooltip("Maximum rotation Angle")]
    public int maxRotationAngle = 100;
    [Tooltip("Whether the camera should follow the player")]
    public bool followPlayer = true;

    [Tooltip("The delay before the camera rotates again")]
    public float delayBeforeMovingAgain = 1f;

    [SerializeField]
    private Transform hookUpHinge;
    [SerializeField]
    private Transform leftLidHinge;
    [SerializeField]
    private Transform rightLidHinge;
    [SerializeField]
    private Transform leftGun;
    [SerializeField]
    private Transform rightGun;

    [SerializeField]
    private float cycleDuration = 3;

    [Header("Turrent Settings")]
    public GameObject projectilePrefab;
    public int projectileSpeed = 10;
    public float turretFireRate = .5f;

    private float timeSinceLastShot = 0f;
    private bool canFire = false;
    private GameObject target = null;
    private Quaternion currentRotation;
    private Quaternion destinationRotation;
    private float currentTime = 0;
    private bool firstCycle = true;
    
    private bool positive = true;                   //direction the camera is rotating
    private bool targetLost = false;                // Whether the target was just lost
    private float targetLostTime = 0;
    private Quaternion initialRotation;
    //private float visualObjectResetTime = 0;
    //private Quaternion visualObjectInitialRotation;
    private Sequence gunAnimation;

    // Use this for initialization
    void Start()
    {
        currentRotation = initialRotation = hookUpHinge.localRotation;

        Vector3 desiredRotation = currentRotation.eulerAngles;
        desiredRotation.y += ((positive ? 1 : -1) * maxRotationAngle) / 2;

        destinationRotation = Quaternion.Euler(desiredRotation);

        maxRotationAngle = Mathf.Clamp(maxRotationAngle, 0, 179);

        gunAnimation = DOTween.Sequence();
        gunAnimation.Pause();


        gunAnimation.Append(leftLidHinge.DOLocalRotate(new Vector3(0, 0, -75), 0.5f))
            .Join(rightLidHinge.DOLocalRotate(new Vector3(0, 0, 75), 0.5f))
            .Append(leftGun.DOLocalMoveX(-0.2f, 0.5f))
            .Join(rightGun.DOLocalMoveX(0.2f, 0.5f))
            .SetAutoKill(false);     

    }

    // Update is called once per frame
    void Update()
    {

        // If there is no target
        if (target == null)
        {
            // Update currentTime
            currentTime += Time.deltaTime;

            // If we just lost the target
            if (targetLost)
            {
                // Update time since target was lost
                targetLostTime += Time.deltaTime;

                // Wait delay if any
                if (targetLostTime < delayBeforeMovingAgain) return;

                CloseGuns();

                // Figure out what our current angle is
                float currentAngle = (hookUpHinge.localRotation.y >= 0 ? 1 : -1) * Quaternion.Angle(initialRotation, hookUpHinge.localRotation);
                // Figure out what the angle of our destination is
                float destinationAngle = ((positive ? 1 : -1) * Quaternion.Angle(initialRotation, destinationRotation));

                // Determine how much distance is left to travel by percentage
                float distanceLeftToTravel = Mathf.Abs((currentAngle - destinationAngle) / maxRotationAngle);
                // Inverse that to get distance traveled which is the same as position in time cycle
                float positionInCycle = 1 - distanceLeftToTravel;

                // Set the current time relative to the position in cycle
                currentTime = (firstCycle ? cycleDuration / 2 : cycleDuration) * positionInCycle;

                // Reset params
                targetLostTime = 0;
                targetLost = false;

            }

            // Make sure it never goes above set cycleDuration. If this is the first cycle,
            //  we are already half done to start
            currentTime = Mathf.Clamp(currentTime, 0, (firstCycle ? cycleDuration / 2 : cycleDuration) + delayBeforeMovingAgain);

            // Get the Lerp frame position between 0,1
            float frame = currentTime / (firstCycle ? cycleDuration / 2 : cycleDuration);
            // If the Lerp is on the last frame
            if (frame >= 1f)
            {
                // If there is a delay before turning back around wait it out. 
                if (currentTime < (firstCycle ? cycleDuration / 2 : cycleDuration) + delayBeforeMovingAgain) return;

                // Disable the firstCycle flag           
                if (firstCycle) firstCycle = false;
                // Reset our currentTime
                currentTime = 0;
                // Toggle rotation direction
                positive = !positive;
                // Save the new currentRotation
                currentRotation = hookUpHinge.localRotation;       // Alternatively currentRotation = destinationRotation

                // Set the destinationRotation to maxRotationAngle in the opposite direction
                Vector3 desiredRotation = currentRotation.eulerAngles;
                desiredRotation.y += ((positive ? 1 : -1) * maxRotationAngle);

                destinationRotation = Quaternion.Euler(desiredRotation);

                // Start over again
                return;
            }

            // Set our rotation
            hookUpHinge.localRotation = Quaternion.Lerp(currentRotation, destinationRotation, frame);

            if (!Mathf.Approximately(0, (Quaternion.Angle(initialRotation, hookUpHinge.localRotation))))
                hookUpHinge.localRotation = Quaternion.Lerp(hookUpHinge.localRotation, initialRotation, Time.deltaTime);

        }
        else
        {
            OpenGuns();

            if (followPlayer)
            {

                Quaternion currentRot = hookUpHinge.localRotation;
                hookUpHinge.LookAt(target.transform.position);
                Vector3 eulerRotation = hookUpHinge.localRotation.eulerAngles;
                //if(eulerRotation.y > initialRotation.y + (maxRotationAngle / 2) || eulerRotation.y < initialRotation.y + (maxRotationAngle / 2))
                //{
                //    hookUpHinge.localRotation = currentRot;
                //    return;
                //}
                eulerRotation.x = initialRotation.eulerAngles.x;
                eulerRotation.z = initialRotation.eulerAngles.z;
                hookUpHinge.localRotation = currentRot;

                Quaternion rotation = Quaternion.Slerp(hookUpHinge.localRotation, Quaternion.Euler(eulerRotation), Time.deltaTime);

                //eulerRotation = rotation.eulerAngles;
                //eulerRotation.y = Mathf.Clamp(eulerRotation.y, initialRotation.y - (maxRotationAngle / 2), initialRotation.y + (maxRotationAngle / 2));
                //rotation.eulerAngles = eulerRotation;
                hookUpHinge.localRotation = rotation;

            }
        }

        if (canFire) Fire();

    }

    void OpenGuns()
    {
        gunAnimation.PlayForward();
        canFire = true;

    }

    void CloseGuns()
    {
        gunAnimation.PlayBackwards();
        canFire = false;
    }

    private void Fire()
    {
        // If we are still wating to shoot again do nothing.
        if (Time.time - timeSinceLastShot < turretFireRate) return;

        // Create the projectile
        GameObject projectile = Instantiate(projectilePrefab, leftGun.position, hookUpHinge.rotation);

        projectile.transform.LookAt(target.transform);
        // Make it go from 0 to 60.. err i mean projectileSpeed, in 0 seconds. 
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;

        // Create the projectile
        projectile = Instantiate(projectilePrefab, rightGun.position, hookUpHinge.rotation);
        projectile.transform.LookAt(target.transform);

        // Make it go from 0 to 60.. err i mean projectileSpeed, in 0 seconds. 
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;

        // Remember when we last fired
        timeSinceLastShot = Time.time;
    }

    public void OnChildTriggerEnter(Collider other)
    {
        //Debug.Log(name + ": " + other.name + "Entered camera zone");

        if (other.CompareTag("Player") || other.name == "Player")
        {

            target = other.gameObject;

        }
    }

    public void OnChildTriggerExit(Collider other)
    {
        //Debug.Log(name + ": " + other.name + "Exited camera zone");

        if (other.CompareTag("Player") || other.name == "Player")
        {

            target = null;

            // Set the targetLost flag
            targetLost = true;

        }
    }

    public void OnChildCollisionEnter(Collision collision)
    {
        throw new NotImplementedException();
    }

    public void OnChildCollisionExit(Collision collision)
    {
        throw new NotImplementedException();
    }

}
