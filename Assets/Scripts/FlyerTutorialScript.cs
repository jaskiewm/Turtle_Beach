using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class FlyerTutorialScript : MonoBehaviour
{
    // Speed of gull in idle and turn speed, time to switch from idle to turn, and time in idle flight
    [SerializeField] float idleSpeed, turnSpeed, switchSeconds, idleRatio; 

    // Store minimum, maximum, and from/to directions for gull
    [SerializeField] Vector2 animSpeedMinMax, moveSpeedMinMax, changeAnimEveryFromTo, changeTargetEveryFromTo;

    // Locations for the home and target of seagul
    [SerializeField] Transform homeTarget, playerPosition;

    // Smallest / Largest distance between seagull and player, along with min / max heights of gull
    [SerializeField] Vector2 radiusMinMax, yMinMax;

    // Used by other scripts to influence / change this script
    [SerializeField] public bool returnToBase = false;
    [SerializeField] public float randomBaseOffset = 5, delayStart = 0f;

    private Animator animator;
    private Rigidbody body;
    public float changeTarget = 0f, changeAnim = 0f, timeSinceTarget = 0f, timeSinceAnim = 0f;
    public float prevAnim, currentAnim = 0f;
    public float prevSpeed, speed, zTurn, prevZ, turnSpeedBackup;

    private Vector3 rotateTarget, position, direction, velocity, randomizedBase;
    private Quaternion lookRotation;
    public float distanceFromBase, distanceFromTarget;

    void Start()
    {
        // Initialize components
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        turnSpeedBackup = turnSpeed;

        direction = Quaternion.Euler(transform.eulerAngles) * (Vector3.forward);
        if(delayStart < 0f)
        {
            body.velocity = idleSpeed * direction;
        }
    }

    void FixedUpdate()
    {
        // Check if there is still a gull delay before sending enemy out
        if (delayStart > 0f)
        {
            delayStart -= Time.fixedDeltaTime;
            return;
        }

        // Calculate distances
        distanceFromBase = Vector3.Magnitude(randomizedBase - body.position);
        distanceFromTarget = Vector3.Magnitude(playerPosition.position - body.position);

        // Allow drastic turns close to base to allow target to be reached
        if(changeAnim < 0f)
        {
            prevAnim = currentAnim;
            currentAnim = ChangeAnim(currentAnim);
            changeAnim = Random.Range(changeAnimEveryFromTo.x, changeAnimEveryFromTo.y);
            timeSinceAnim = 0f;
            prevSpeed = speed;
            if (currentAnim == 0) speed = idleSpeed;
            else
            {
                speed = Mathf.Lerp(moveSpeedMinMax.x, moveSpeedMinMax.y, (currentAnim - animSpeedMinMax.x) / (animSpeedMinMax.y - animSpeedMinMax.x));
            }

            // New target position time?
            if (changeTarget < 0f)
            {
                rotateTarget = ChangeDirection(body.transform.position);
                if (returnToBase) changeTarget = 0.2f; // Update to change target in 0.2 seconds
                else
                {
                    changeTarget = Random.Range(changeAnimEveryFromTo.x, changeAnimEveryFromTo.y);
                    timeSinceTarget = 0f;
                }
            }

            // Update times
            changeAnim -= Time.fixedDeltaTime;
            changeTarget -= Time.fixedDeltaTime;
            timeSinceTarget += Time.fixedDeltaTime;
            timeSinceAnim += Time.fixedDeltaTime;

            if (rotateTarget != Vector3.zero)
            {
                lookRotation = Quaternion.LookRotation(rotateTarget, Vector3.up);
                Vector3 rotation = Quaternion.RotateTowards(body.transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime).eulerAngles;
                body.transform.eulerAngles = rotation;
            }

            //Move Flyer
            direction = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;
            // STOPPED TUTORIAL HERE AT 10:27 IN VIDEO
        }
    }

    public float ChangeAnim(float currentAnim)
    {
        return 1f;
    }

    public Vector3 ChangeDirection(Vector3 position)
    {
        Vector3 newVec = new Vector3(0, 0, 0);
        return Vector3.forward;
    }
}
