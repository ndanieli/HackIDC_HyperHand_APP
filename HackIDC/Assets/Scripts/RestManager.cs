using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RestManager : MonoBehaviour {

    private string sendDecisionUrl = "http://jonibr.pythonanywhere.com/send_decision?player_id={0}&button_id={1}";
    private string sendIsMyTurnRequestUrl = "http://jonibr.pythonanywhere.com/is_my_turn?player_id={0}"; 
    private string sendReceiveDecisionResultsUrl = "http://jonibr.pythonanywhere.com/check_decisions?player_id={0}"; 
    private string result = string.Empty;
    private bool working;

    public string sendIsMyTurnRequest(int playerId)
    {
        string url = string.Format(sendIsMyTurnRequestUrl, playerId);
        StartCoroutine(GetRequest(url));

        return result;
    }

    public string sendDecision(int playerId, int buttonId)
    {
        string url = string.Format(sendDecisionUrl, playerId, buttonId);
        StartCoroutine(GetRequest(url));
        Debug.Log(result);
        return result;
    }

    public string sendReceiveDecisionResults(int playerId)
    {
        string url = string.Format(sendReceiveDecisionResultsUrl, playerId);
        StartCoroutine(GetRequest(url));
        
        return result;
    }

    IEnumerator GetRequest(string url)
    {
        working = true;
        UnityWebRequest request = new UnityWebRequest(url);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SendWebRequest();

        while (request.downloadHandler.text == "") ;
        result = request.downloadHandler.text;
        /*while (result == "0|0|0|0|0")
        {
            new WaitForSeconds(0.5f);
            StartCoroutine(GetRequest(url));
        }*/
        working = false;

        yield return null;
    }

}
