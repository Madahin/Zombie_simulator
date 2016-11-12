using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    //public FadeScreenAutoRef fadeScreen;
    public GameObject Player;
    public GameObject ambientMusic;
    public string currentScene;

   

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void LoadLevel(string levelName)
    {
        //fadeScreen.FadeOut();
        //StartCoroutine(DelayedLoadLevel(levelName));
    }

    private IEnumerator DelayedLoadLevel(string levelString)
    {
        yield return new WaitForSeconds(3.0f);
        //Application.LoadLevel(levelString);
        SceneManager.LoadScene(levelString);

    }
    

}
