using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public void startGame(){
		SceneManager.LoadScene("Level");
	}
	public void Quit(){
		Application.Quit();
	}
	public void startLevel1(){
		SceneManager.LoadScene("Level1");
	}
	public void startLevel2(){
		SceneManager.LoadScene("ChooseLevel");
	}
	public void Back(){
		SceneManager.LoadScene("Start");
	}	
}
