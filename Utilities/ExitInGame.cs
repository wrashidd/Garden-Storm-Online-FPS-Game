using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
This class is responsible exiting in game logic
*/
public class ExitInGame : MonoBehaviour
{
    public GameObject OnOFF;

    void Start()
    {
        OnOFF.GetComponent<Transform>().gameObject.SetActive(false);
    }

    // Update is called once per frame
    // Launches exit game sequence on user input
    void Update()
    {
        EscapeInGame();
    }

    // Defines exit game logic
    private void EscapeInGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pressing Escape Button");
            if (OnOFF.gameObject.activeSelf == true)
            {
                OnOFF.GetComponent<Transform>().gameObject.SetActive(false);
                PlayerController.Instance.gamePauseToMenu = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                OnOFF.GetComponent<Transform>().gameObject.SetActive(true);
                PlayerController.Instance.gamePauseToMenu = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    // Resumes the game
    public void Resume()
    {
        OnOFF.GetComponent<Transform>().gameObject.SetActive(false);
        PlayerController.Instance.gamePauseToMenu = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Loads foreground menu
    public void LoadMenu()
    {
        StartCoroutine(ExitBackToMainMenu());
    }

    // Closes the entire game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Sends user to the Main Menu
    IEnumerator ExitBackToMainMenu()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        Destroy((RoomManager.Instance.gameObject));
        SceneManager.LoadScene(0);
    }
}
