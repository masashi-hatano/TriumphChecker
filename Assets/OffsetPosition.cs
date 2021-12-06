using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPosition : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
 
    void Update()
    {
       offset = new Vector3(0, 7.79f, -11.57f);
       this.transform.position = target.position + offset;
    }
}
