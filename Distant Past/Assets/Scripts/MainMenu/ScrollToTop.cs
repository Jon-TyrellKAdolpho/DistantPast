using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollToTop : MonoBehaviour
{
    ScrollRect scrollRect;

    public void ResetScrollRect()
    {
        scrollRect = GetComponent<ScrollRect>();

    }
}
