using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddListenerYes : MonoBehaviour
{
    private Button button;
    public GameObject fieldObject;
    public string LevelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }

    // Update is called once per frame
    public void OnClickButton()
    {
        SceneManager.LoadScene(LevelToLoad);
        GameData.SaveGame();
    }
}
