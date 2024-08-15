using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform compass;
    void Start()
    {
        target = KeaPlayer.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        compass.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, target.eulerAngles.y);
    }
}
