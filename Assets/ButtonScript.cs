using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public Button yourButton;
    public string setScene;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Scene getScene = SceneManager.GetSceneByName(setScene);
        SceneManager.LoadScene(setScene);
        SceneManager.SetActiveScene(getScene);
    }
}
