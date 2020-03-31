using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField InputUsernameField;
    private string username;

    public void OnEnable()
    {
        //InputUsernameField.GetComponent<InputField>().placeholder.GetComponent<TextMeshPro>().text = "Input Username ...";
        InputUsernameField.text = "";
    }

    public void PlayGame()
    {
        username = InputUsernameField.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        Debug.Log("BYE... See you next time !");
        Application.Quit();
    }

    void OnDestroy()
    {
        PlayerPrefs.SetString("username", username);
    }
}
