using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVerificationText : MonoBehaviour
{
    GameObject[] point_Objects;
    GameObject clickedGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
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
                for (int i = 0; i < point_Objects.Length; i++)
                {
                    if (clickedGameObject.transform == point_Objects[i].transform){
                        //messageBox.Show(Callback, "Hello World!", "Hello");
                        break;
                    }
                }
            }
        }
    }
}
