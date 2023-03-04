using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/*
This class is responsible for controlling UI
*/
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider fruitCounterSlider;
    public TextMeshProUGUI fruitCounterText,
        gameOverText,
        gameOverSubText;
    public Image sliderFill;
    public Color fruitColorRed = new Color(253f, 94f, 83f);
    public Color fruitColorGreen = new Color(185f, 230f, 178f, 255f);
    private PhotonView PV;

    // Runs before Start
    // Accesses Photon networking system
    private void Awake()
    {
        instance = this;
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    // Sets default messages in UI
    void Start()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.GetComponent<TextMeshProUGUI>().enabled = false;
        gameOverSubText.GetComponent<TextMeshProUGUI>().enabled = false;
        sliderFill.color = fruitColorRed;
    }
}
