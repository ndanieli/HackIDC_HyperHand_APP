using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public bool isMyTurn;
    public long eotTime;
    public int numOfButtons;
    public string[] buttonTexts;

    public Parser parseIsMyTurnResponse(string response)
    {
        string[] strResponse = response.Split('|');

        //initiate isMyTurn value.
        if (strResponse[0].ToLower().Equals("true"))
        {
            isMyTurn = true;
        }
        else
        {
            isMyTurn = false;
        }

        //initiate eotTime value.
        eotTime = long.Parse(strResponse[1]);

        //initiate numOfButtons value.
        numOfButtons = int.Parse(strResponse[2]);

        //initiate buttonTexts value.
        buttonTexts = new string[5];
        Array.Copy(strResponse, 3, buttonTexts, 0, 5);

        return this;
    }

    public Parser parseGetDecisions(string response)
    {
        buttonTexts = response.Split('|');
        return this;
    }
}
