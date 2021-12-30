using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationNode : MonoBehaviour
{

    public GameObject Terrain;
    public GameObject panelText;
    public GameObject panelCreate;
    // Start is called before the first frame update
    void Start()
    {
        panelCreate = GameData.panelCreate;
        panelText = GameData.panelText;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.NewMemoryMode && !GameData.Creation && Input.GetMouseButtonDown(0))
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
                }
            }
        }
    }

}
