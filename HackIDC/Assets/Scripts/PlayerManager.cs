using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    private int playerId;
    private int selectedButton;
    private int tableNumber;
    private int seatNumber;
    public Text playerIDText;

    public int PlayerId { get { return playerId; } }
    public int SelectedButton { get { return selectedButton; } }
    public int TableNumber { get { return tableNumber; } }
    public int SeatNumber { get { return seatNumber; } }


    private void Start()
    {
        tableNumber = 0;
        seatNumber = 0;
    }

    public void setPlayerId(int id)
    {
        playerId = id;
        playerIDText.text = "Player ID is " + id;
    }

    public void setSelectedButton(int buttonIndex)
    {
        selectedButton = buttonIndex;
    }

}
