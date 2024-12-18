using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;

    string newGameScene = "SampleScene";
    
    void Start()
    {
        int HighScore = SaveLoadManager.Instance.LoadHighScore();
        highScoreUI.text = $"Top Wave Survived: {HighScore}";
    }

    public void StartNewGame(){
        SceneManager.LoadScene(newGameScene);
    }


    public void ExitApplication(){

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;

#else
    Application.Quit();

#endif
    }

}
