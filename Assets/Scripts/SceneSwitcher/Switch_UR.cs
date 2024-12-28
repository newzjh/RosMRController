using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch_UR : MonoBehaviour
{
    public string sceneToLoad;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}