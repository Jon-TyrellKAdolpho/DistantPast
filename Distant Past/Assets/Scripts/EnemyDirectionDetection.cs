using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyDirectionDetection : MonoBehaviour
{
    [SerializeField] Transform playerLocation;
    [SerializeField] Health playerHealth;
    [SerializeField] List<Image> locations;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.lastAttacker != null)
        {
            ShowEnemy();
        }
    }
    public void ShowEnemy()
    {

        if(GetDirection(playerLocation, playerHealth.lastAttacker) == 1)
        {
            DisplayLocation(1);
        }
        if (GetDirection(playerLocation, playerHealth.lastAttacker) == 2)
        {
            DisplayLocation(2);
        }
        if (GetDirection(playerLocation, playerHealth.lastAttacker) == 3)
        {
            DisplayLocation(3);
        }
        if (GetDirection(playerLocation, playerHealth.lastAttacker) == 4)
        {
            DisplayLocation(4);
        }
        if (GetDirection(playerLocation, playerHealth.lastAttacker) == 5)
        {
            DisplayLocation(5);
        }
        if (GetDirection(playerLocation, playerHealth.lastAttacker) == 6)
        {
            DisplayLocation(6);
        }
        if (GetDirection(playerLocation, playerHealth.lastAttacker) == 7)
        {
            DisplayLocation(7);
        }
        if (GetDirection(playerLocation, playerHealth.lastAttacker) == 8)
        {
            DisplayLocation(8);
        }

        playerHealth.lastAttacker = null;
    }
    void DisplayLocation(int value)
    {
        Color color = locations[value - 1].color;
        color.a = .5f;
        locations[value - 1].color = color;
    }
    int GetDirection(Transform fromTransform, Transform toTransform)
    {
        // Get the forward vector of the first transform
        Vector3 forward = fromTransform.forward;

        // Calculate the vector from the first transform to the second transform
        Vector3 toOther = (toTransform.position - fromTransform.position).normalized;

        // Calculate the angle between the forward vector and the vector to the other transform
        float angle = Vector3.SignedAngle(forward, toOther, Vector3.up);

        if (angle >= -22.5f && angle < 22.5f)
            return 1; // Front
        else if (angle >= 22.5f && angle < 67.5f)
            return 2; // Front-Right
        else if (angle >= 67.5f && angle < 112.5f)
            return 3; // Right
        else if (angle >= 112.5f && angle < 157.5f)
            return 4; // Right-Behind
        else if ((angle >= 157.5f && angle <= 180f) || (angle >= -180f && angle < -157.5f))
            return 5; // Behind
        else if (angle >= -157.5f && angle < -112.5f)
            return 6; // Behind-Left
        else if (angle >= -112.5f && angle < -67.5f)
            return 7; // Left
        else if (angle >= -67.5f && angle < -22.5f)
            return 8; // Front-Left

        // Return 0 if none of the conditions match (should not happen)
        return 0;
    }
}
