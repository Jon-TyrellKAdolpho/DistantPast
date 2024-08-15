using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate : MonoBehaviour
{
    LineRenderer lineRenderer;
    public List<Transform> points;
    List<Transform> truePoints;
    bool lineDrawn;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is not assigned!");
            return;
        }

        if (points.Count < 2)
        {
            Debug.LogError("At least two points are required to draw a line!");
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!lineDrawn)
            {
                DrawLine();
            }

        }
        else
        {
            if (lineDrawn)
            {
                lineRenderer.enabled = false;
                lineDrawn = false;
            }
        }
    }
    void DrawLine()
    {
        if(points.Count <= 0)
        {
            return;
        }
        lineRenderer.enabled = true;
        Transform startPoint = FindClosestTransform(transform, points);
        int where = 0;
        int where2 = 0;
        truePoints = new List<Transform>(points);

        List<Transform> tempPoints = new List<Transform>(truePoints);

        tempPoints.Remove(startPoint);

        Transform testStart = FindClosestTransform(transform, tempPoints);


        for (int i = truePoints.Count - 1; i >= 0; i--)
        {
            if(truePoints[i] == startPoint)
            {
                where = i;
            }
            if(truePoints[i] == testStart)
            {
                where2 = i;
            }
        }

        if(where2 > where)
        {
            for (int i = truePoints.Count - 1; i >= 0; i--)
            {
                if (i < where2)
                {
                    truePoints.Remove(truePoints[i]);
                }
            }
        }
        else
        {
            for (int i = truePoints.Count - 1; i >= 0; i--)
            {
                if (i < where)
                {
                    truePoints.Remove(truePoints[i]);
                }
            }
        }
        List<Transform> finalLine = new List<Transform>();
        finalLine.Add(transform);
        finalLine.AddRange(truePoints);
        lineRenderer.positionCount = finalLine.Count;
        for (int i = 0; i < finalLine.Count; i++)
        {
            lineRenderer.SetPosition(i, finalLine[i].position);
        }
        lineDrawn = true;
    }
    Transform FindClosestTransform(Transform target, List<Transform> transforms)
    {
        if (transforms.Count == 0)
        {
            return null;
        }

        Transform closestTransform = transforms[0];
        float minDistance = Vector3.Distance(target.position, transforms[0].position);

        for (int i = 1; i < transforms.Count; i++)
        {
            float distance = Vector3.Distance(target.position, transforms[i].position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTransform = transforms[i];
            }
        }

        return closestTransform;
    }
}
