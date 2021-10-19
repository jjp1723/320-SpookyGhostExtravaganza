using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broomstick : MonoBehaviour
{
    [SerializeField]
    private GameObject left;
    [SerializeField]
    private GameObject right;
    [SerializeField]
    private GameObject center;

    public void HideLeft()
    {
        left.SetActive(false);
    }

    public void HideRight()
    {
        right.SetActive(false);
    }

    public void HideCenter()
    {
        center.SetActive(false);
    }
}
