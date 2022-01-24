using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (GameData.DeleteMemoryMode && !GameData.Creation)
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
    }
}