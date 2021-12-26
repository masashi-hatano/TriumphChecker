using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveToClickPoint : MonoBehaviour {
    NavMeshAgent agent;
    GameObject[] point_Objects;
    GameObject clickedGameObject;
    Animator animator;
    public GameObject fieldObject;
    
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        point_Objects = GameObject.FindGameObjectsWithTag("point");
        animator = GetComponent<Animator>();
        fieldObject = GameObject.Find("Canvas");
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            clickedGameObject = null;
            if (true)
            {   
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
        animator.SetFloat("Forward", agent.velocity.magnitude);
        //animator.SetFloat("Turn", agent.velocity.magnitude);
        //animator.SetFloat("Crouch", agent.velocity.magnitude);
        //animator.SetFloat("OnGround", agent.velocity.magnitude);
        //animator.SetFloat("Jump", agent.velocity.magnitude);
        //animator.SetFloat("JumpLeg", agent.velocity.magnitude);
    }
}
