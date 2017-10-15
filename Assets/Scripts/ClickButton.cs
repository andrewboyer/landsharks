using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
