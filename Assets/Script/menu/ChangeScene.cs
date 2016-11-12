using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour{

    public void LoadStage(string yourStage)
    {
        SceneManager.LoadScene(yourStage);
    }

    public void LoadStage()
    {
        SceneManager.LoadScene("Main");
    }

}
