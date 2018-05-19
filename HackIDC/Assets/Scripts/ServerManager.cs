using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{

    public PlayerManager playerManager;
    public Parser parser;
    public RestManager restManager;
    public ButtonManager buttonManager;
    public VideoPlayer winningVideoPlayer;
    private bool isMyTurn;
    private bool updatedForThisTurn;
    private int tableNumber;
    private int seatNumber;
    private long time;
    public bool isGameStarted;
    public Text debugText;
    private bool isWaitingForRest;
    private string lastIsMyTurnString;

    private void Start()
    {
        isGameStarted = false;
        isMyTurn = false;
        tableNumber = playerManager.TableNumber;
        seatNumber = playerManager.SeatNumber;
        time = 0;
        updatedForThisTurn = false;
        isWaitingForRest = false;
        lastIsMyTurnString = string.Empty;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!isMyTurn && isGameStarted)
        {
            //do this on another thread
            Debug.Log("In loop");
            string isMyTurnString = restManager.sendIsMyTurnRequest(playerManager.PlayerId);
            if (isMyTurnString == "false|0|0")
            {
                buttonManager.showZeroButtons();
                isMyTurn = true;
            }
            else if (isMyTurnString != "" && !isMyTurnString.Equals(lastIsMyTurnString))
            {
                Debug.Log(isMyTurnString);
                Parser isMyTurnParse = parser.parseIsMyTurnResponse(isMyTurnString);
                isMyTurn = isMyTurnParse.isMyTurn;

                if (isMyTurn)
                {
                    myTurn(parser.buttonTexts);
                }
                else if (!updatedForThisTurn)
                {
                    if (isMyTurnParse.numOfButtons == 0)
                    {
                        buttonManager.showZeroButtons();
                        updatedForThisTurn = true;
                    }
                    else if (isMyTurnParse.numOfButtons == 2)
                    {
                        buttonManager.showTwoButtons(isMyTurnParse.buttonTexts);
                        updatedForThisTurn = true;
                    }
                    else if (isMyTurnParse.numOfButtons == 4)
                    {
                        buttonManager.showFourButtons(isMyTurnParse.buttonTexts);
                        updatedForThisTurn = true;
                    }
                }
            }
        }
    }

    private void myTurn(string[] buttonTexts)
    {
        buttonManager.showFourButtons(buttonTexts);
    }

    public IEnumerator getDecisions()
    {
        string decisionResultString = restManager.sendReceiveDecisionResults(playerManager.PlayerId);
        Parser decisionParser = parser.parseGetDecisions(decisionResultString);
        int decisionButtonIndex = -1;
        int bestDecisionCount = -1;

        for (int stringIndex = 0; stringIndex < decisionParser.buttonTexts.Length; stringIndex++)
        {
            int buttonDecisionNum = int.Parse(decisionParser.buttonTexts[stringIndex]);

            if (buttonDecisionNum > bestDecisionCount)
            {
                bestDecisionCount = buttonDecisionNum;
                decisionButtonIndex = stringIndex;
            }
        }

        buttonManager.updateButtonTexts(decisionParser.buttonTexts);

        isMyTurn = false;
        updatedForThisTurn = false;

        yield return null;
    }
}
