using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeaPath : MonoBehaviour
{
    public List<Transform> points;
    Navigate navigate;

    private void Start()
    {
        navigate = FindObjectOfType<Navigate>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            navigate.points = points;
        }
    }
    public void RemovePath()
    {
        if(navigate.points[0] == points[0])
        {
            navigate.points.Clear();
            Destroy(gameObject);
        }
    }
}
