using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Play()
	{
		SceneManager.LoadScene(1);
	}
    public void PlayStatic()
    {
        SceneManager.LoadScene(2);
    }

    public void Learn()
	{
		SceneManager.LoadScene(3);
	}

    public void BackToMain()
	{
		SceneManager.LoadScene(0);
	}

	public void Quit()
	{
		Debug.Log("QUIT!");
        //BackToMain();
		Application.Quit();
	}
}
