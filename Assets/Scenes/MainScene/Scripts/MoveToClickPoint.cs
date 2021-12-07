using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
    
public class MoveToClickPoint : MonoBehaviour {
    NavMeshAgent agent;
    GameObject[] point_Objects;
    GameObject clickedGameObject;
    
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        point_Objects = GameObject.FindGameObjectsWithTag("point");
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            clickedGameObject = null;
 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            
            if (Physics.Raycast(ray, out hit, 100)) {
                clickedGameObject = hit.collider.gameObject;
                for (int i = 0; i < point_Objects.Length; i++)
                {
                    if (clickedGameObject.transform == point_Objects[i].transform){
                        agent.destination = hit.point;
                        break;
                    }
                }

                
            }
        }
    }
}
