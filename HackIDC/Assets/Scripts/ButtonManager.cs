using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public PlayerManager playerManager;
    public Button button0;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button playerSelectionButton1;
    public Button playerSelectionButton2;
    public Button playerSelectionButton3;
    public ServerManager serverManager;

    private void Start()
    {
        enablePlayerSelectionButtons();
        showZeroButtons();
    }

    public void OnButton0Pressed()
    {
        changeSelectedButton(0);
    }

    public void OnButton1Pressed()
    {
        changeSelectedButton(1);
    }

    public void OnButton2Pressed()
    {
        changeSelectedButton(2);
    }

    public void OnButton3Pressed()
    {
        changeSelectedButton(3);
    }

    public void OnButton4Pressed()
    {
        changeSelectedButton(4);
    }

    public void OnPlayerButton1Pressed()
    {
        playerManager.setPlayerId(0);
        disablePlayerSelectionButtons();
    }

    public void OnPlayerButton2Pressed()
    {
        playerManager.setPlayerId(1);
        disablePlayerSelectionButtons();
    }

    public void OnPlayerButton3Pressed()
    {
        playerManager.setPlayerId(2);
        disablePlayerSelectionButtons();
    }

    public void showFourButtons(string[] buttonTexts)
    {
        button0.gameObject.SetActive(true);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(true);
        button4.gameObject.SetActive(true);

        updateButtonTexts(buttonTexts);
    }

    public void showTwoButtons(string[] buttonTexts)
    {
        button0.gameObject.SetActive(true);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);

        updateButtonTexts(buttonTexts);
    }

    public void showZeroButtons()
    {
        button0.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
    }

    private void changeSelectedButton(int buttonIndex)
    {
        playerManager.setSelectedButton(buttonIndex);
    }

    private void disablePlayerSelectionButtons()
    {
        playerSelectionButton1.gameObject.SetActive(false);
        playerSelectionButton2.gameObject.SetActive(false);
        playerSelectionButton3.gameObject.SetActive(false);
        serverManager.isGameStarted = true;
    }

    private void enablePlayerSelectionButtons()
    {
        playerSelectionButton1.gameObject.SetActive(true);
        playerSelectionButton2.gameObject.SetActive(true);
        playerSelectionButton3.gameObject.SetActive(true);
    }

    public void updateButtonTexts(string[] buttonTexts)
    {
        button0.GetComponentInChildren<Text>().text = buttonTexts[0];
        button1.GetComponentInChildren<Text>().text = buttonTexts[1];
        button2.GetComponentInChildren<Text>().text = buttonTexts[2];
        button3.GetComponentInChildren<Text>().text = buttonTexts[3];
        button4.GetComponentInChildren<Text>().text = buttonTexts[4];
    }

    public void getDecisionsFunction()
    {
        StartCoroutine(serverManager.getDecisions());
    }

    public void submitFunction()
    {
        serverManager.restManager.sendDecision(serverManager.playerManager.PlayerId, serverManager.playerManager.SelectedButton);
    }

}
