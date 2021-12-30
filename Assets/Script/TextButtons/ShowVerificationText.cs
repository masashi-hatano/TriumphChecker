using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVerificationText : MonoBehaviour
{
    GameObject clickedGameObject;
    public int ID;
    public GameObject panel;
    public GameObject panelText;
    public GameObject panelTextDelete;

    // Start is called before the first frame update
    void Start()
    {
        panel = GameData.panel;
        panelText = GameData.panelText;
        panelTextDelete = GameData.panelTextDelete;
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameData.NewMemoryMode && !GameData.DeleteMemoryMode && !GameData.Creation && Input.GetMouseButtonDown(0)) {
            clickedGameObject = null;
 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            
            if (Physics.Raycast(ray, out hit, 100)) {
                clickedGameObject = hit.collider.gameObject;
                if (clickedGameObject.transform.position == this.transform.position){
                    GameData.ID = ID;
                    panel.SetActive(true);
                    panelText.SetActive(false);
                    panelTextDelete.SetActive(false);
                    GameData.Creation = true;
                }
            }
        }
    }
}
