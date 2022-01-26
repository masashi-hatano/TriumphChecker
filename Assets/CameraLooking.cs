using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLooking : MonoBehaviour
{

    private Transform textMeshTransform;
    // Start is called before the first frame update
    void Start()
    {
        textMeshTransform = GetComponent<TextMesh>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshTransform.rotation = Quaternion.LookRotation(textMeshTransform.position - Camera.main.transform.position);
    }
}
