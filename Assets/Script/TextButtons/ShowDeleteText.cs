using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDeleteText : MonoBehaviour
{
    GameObject clickedGameObject;
    public int ID;
    public GameObject panelDelete;
    public GameObject panelTextDelete;

    // Start is called before the first frame update
    void Start()
    {
        panelDelete = GameData.panelDelete;
        panelTextDelete = GameData.panelTextDelete;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.DeleteMemoryMode && !GameData.Creation && Input.GetMouseButtonDown(0))
        {
            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, 100))
            {
                clickedGameObject = hit.collider.gameObject;
                if (clickedGameObject.transform.position == this.transform.position)
                {
                    GameData.ID = ID;
                    GameData.delete_node = this.gameObject;
                    panelDelete.SetActive(true);
                    panelTextDelete.SetActive(false);
                    GameData.Creation = true;
                }
            }
        }
    }
}