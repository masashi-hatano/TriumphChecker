﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreationNode : MonoBehaviour
{

    public GameObject Terrain;
    public GameObject panelText;
    public GameObject panelCreate;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        panelCreate = GameData.panelCreate;
        panelText = GameData.panelText;
        text = GameData.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (GameData.NewMemoryMode && !GameData.Creation)
                {
                    RaycastHit hit;
                    RaycastHit hit_node;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Collider collider = Terrain.GetComponent<Collider>();
                    if (collider.Raycast(ray, out hit, 100))
                    {
                        Physics.Raycast(ray, out hit_node, 100);
                        if (hit_node.collider.gameObject.tag != "point")
                        {
                            panelCreate.SetActive(true);
                            panelText.SetActive(false);
                            GameData.Creation = true;
                            GameData.click_pos = hit.point;
                            text.text = "Which memory do\nyou want to store ?";
                        }
                    }
                }
            }
        }
    }

}
