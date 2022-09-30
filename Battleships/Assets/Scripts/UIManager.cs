using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerOneInputField;
    [SerializeField] private TMP_InputField playerTwoInputField;
    [SerializeField] private Button startGameButton;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text winPlayerNameText;
    private void Update()
    {
        CheckIfInputFieldsAreFilled();
    }

    private void CheckIfInputFieldsAreFilled()
    {
        if (playerOneInputField.text != "" && playerTwoInputField.text != "")
        {
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.interactable = false;
        }
    }

    public void SavePlayersName()
    {
        PlayerPrefs.SetString("playerOne", playerOneInputField.text);
        PlayerPrefs.SetString("playerTwo", playerTwoInputField.text);
    }

    private string GetPlayerOneName()
    {
         return PlayerPrefs.GetString("playerOne");
    }

    private string GetPlayerTwoName()
    {
        return PlayerPrefs.GetString("playerTwo");
    }

    public void SetPlayerName(bool player)
    {
        if (player)
        {
            playerNameText.text = GetPlayerOneName();
        }
        else
        {
            playerNameText.text = GetPlayerTwoName();
        }
    }

    public void SetWinName(bool player)
    {
        if (player)
        {
            winPlayerNameText.text = GetPlayerOneName() + " " + "WINS";
        }
        else
        {
            winPlayerNameText.text = GetPlayerTwoName() + " " + "WINS";
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
