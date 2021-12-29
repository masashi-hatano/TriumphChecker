using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVerificationText : MonoBehaviour
{
    GameObject[] point_Objects;
    GameObject clickedGameObject;
    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        gameObject = GameObject.Find("GameObject");
        point_Objects = GameObject.FindGameObjectsWithTag("point");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            clickedGameObject = null;
 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            
            if (Physics.Raycast(ray, out hit, 100)) {
                clickedGameObject = hit.collider.gameObject;
                if (clickedGameObject.transform.position == this.transform.position){
                    gameObject.GetComponent<VerificationText>().FieldActive();
                }
            }
        }
    }
}
