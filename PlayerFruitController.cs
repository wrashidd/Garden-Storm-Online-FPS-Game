using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerFruitController : MonoBehaviour
{

    public static PlayerFruitController instance;
    public int maxFruits, currentFruits;
    //private PhotonView PV;
   
    
//----------------------Game Events Start-----------------------------------------
    private void Awake()
    {
        instance = this;
       // PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //currentFruits = maxFruits;
        currentFruits = 75;
        UIController.instance.fruitCounterSlider.maxValue = maxFruits;
        UIController.instance.fruitCounterSlider.value = currentFruits;
        UIController.instance.fruitCounterText.text = "Fruits: " + currentFruits + "/" + maxFruits;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
    }
    */
    
    //----------------------Game Events End-----------------------------------------


//--------------------------Methods-------------------------------------------------

    public void LoseFruits(int loseAmount) 
    {
        if (currentFruits > 0)
        {
            currentFruits -= loseAmount;
            UIController.instance.fruitCounterSlider.value = currentFruits;
            UIController.instance.fruitCounterText.text = "Fruits: " + currentFruits + "/" + maxFruits;
        }

        if (currentFruits <= 0)
        {
            gameObject.SetActive(false);
            UIController.instance.gameOverText.GetComponent<TextMeshProUGUI>().enabled = true;
            UIController.instance.gameOverSubText.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    public void AddFruits(int addAmount)
    {
        currentFruits += addAmount;
        UIController.instance.fruitCounterSlider.value = currentFruits;
        UIController.instance.fruitCounterText.text = "Fruits: " + currentFruits + "/" + maxFruits;
        StartCoroutine(fruitBarColorChange());
    }

    IEnumerator fruitBarColorChange()
    {
        UIController.instance.sliderFill.color= UIController.instance.fruitColorGreen;
        yield return new WaitForSeconds(0.2f);
        UIController.instance.sliderFill.color= UIController.instance.fruitColorRed;
        
    }
}



