using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitInGame : MonoBehaviour
{
    public GameObject OnOFF;
   // public PlayerController  _playerController;
   
    // Start is called before the first frame update
    void Start()
    {
        OnOFF.GetComponent<Transform>().gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     EscapeInGame();
    }


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

    public void Resume()
    {
        OnOFF.GetComponent<Transform>().gameObject.SetActive(false);
        PlayerController.Instance.gamePauseToMenu = true;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void LoadMenu()
    {
       // SceneManager.LoadScene(sceneBuildIndex:0);
       StartCoroutine(ExitBackToMainMenu());
    }

    public void QuitGame()
    {
         Application.Quit();
    }

    IEnumerator ExitBackToMainMenu()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        Destroy((RoomManager.Instance.gameObject));
        SceneManager.LoadScene(0);
    } 
        
}
