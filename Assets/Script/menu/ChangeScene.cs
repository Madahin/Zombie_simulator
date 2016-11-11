using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour{

    public void LoadStage(string yourStage)
    {
        Application.LoadLevel(yourStage);
    }

    public void LoadStage()
    {
        Application.LoadLevel("Main");
    }

}
