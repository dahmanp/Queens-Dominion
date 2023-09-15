using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameUI : MonoBehaviour
{
    public PlayerUIContainer[] playerContainers;
    public GameObject MiddayWin;
    public GameObject MidnightWin;
    public TextMeshProUGUI winText;
    public Slider nightSlider;
    public Slider daySlider;

    // instance
    public static GameUI instance;

    void Awake()
    {
        // set the instance to this script
        instance = this;
    }

    void Start()
    {
        InitializePlayerUI();
    }

    void InitializePlayerUI()
    {
        // loop through all containers
        for (int x = 0; x < playerContainers.Length; ++x)
        {
            PlayerUIContainer container = playerContainers[x];
            //problem above
            // only enable and modify UI containers we need
            if (x < PhotonNetwork.PlayerList.Length)
            {
                container.obj.SetActive(true);
                container.nameText.text = PhotonNetwork.PlayerList[x].NickName;
                container.hatTimeSlider.maxValue = GameManager.instance.timeToWin;
            }
            else
                container.obj.SetActive(false);
        }
    }

    void Update()
    {
        UpdatePlayerUI();
    }

    void UpdatePlayerUI()
    {
        // loop through all players
        for (int x = 0; x < GameManager.instance.players.Length; ++x)
        {
            if (GameManager.instance.players[x] != null)
            {
                playerContainers[x].hatTimeSlider.value = GameManager.instance.players[x].curHatTime;
                //problem above
            }
        }
    }

    public void SetWinText(string winnerName)
    {
        if (nightSlider.value == 20)
        {
            MidnightWin.gameObject.SetActive(true);
            winText.text = winnerName + " ended as the queen!";
            winText.gameObject.SetActive(true);
        } else if (daySlider.value == 20)
        {
            MiddayWin.gameObject.SetActive(true);
            winText.text = winnerName + " ended as the queen!";
            winText.gameObject.SetActive(true);
        }
    }
}

[System.Serializable]
public class PlayerUIContainer
{
    public GameObject obj;
    public TextMeshProUGUI nameText;
    public Slider hatTimeSlider;
}


