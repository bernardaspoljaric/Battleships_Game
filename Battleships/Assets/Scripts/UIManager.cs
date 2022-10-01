using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;


public class UIManager : MonoBehaviour
{
    public string selectedFileName;

    [Header("Setup menu")]
    [SerializeField] private TMP_InputField playerOneInputField;
    [SerializeField] private TMP_InputField playerTwoInputField;
    [SerializeField] private Button startGameButton;

    [Header("Game menu")]
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private TMP_Text playerNameText;

    [Header("View games menu")]
    [SerializeField] private GameObject viewGamesMenu;
    [SerializeField] private GameObject recordedGamePrefab;
    [SerializeField] private GameObject scrollView;

    [Header("Button position in View games menu")]
    private float padding = 200;
    private int fileNumber = 0;
    private Vector3 startingPoint = new Vector3(220, -220, 0);

    [Header("View game")]
    [SerializeField] private GameObject viewGame;
    public TMP_InputField speedInputField;
    public TMP_Text gameName;
    public Button playButton;
    public Button forwardButton;
    public Button backwardButton;

    [Header("Win menu")]
    [SerializeField] private GameObject winMenu;
    [SerializeField] private TMP_Text winPlayerNameText;

    private void Start()
    {
        ShowPlayedGames();
    }
    private void Update()
    {
        CheckIfInputFieldsAreFilled();
    }

    // if all input fields are filled game can be started
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

    // method for saving player names
    public void SavePlayersName()
    {
        PlayerPrefs.SetString("playerOne", playerOneInputField.text);
        PlayerPrefs.SetString("playerTwo", playerTwoInputField.text);
    }

    // method for getting player1 name
    private string GetPlayerOneName()
    {
         return PlayerPrefs.GetString("playerOne");
    }

    // method for getting player2 name
    private string GetPlayerTwoName()
    {
        return PlayerPrefs.GetString("playerTwo");
    }

    // method for setting player name in game menu depending which player's turn is
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

    // method for setting wining player's name in win menu
    private void SetWinName(bool player)
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

    // method for showing win menu when game is over
    public void ShowWinMenu(bool player)
    {
        winMenu.SetActive(true);
        gameMenu.SetActive(false);
        SetWinName(player);
    }

    // method for showing all played games
    private void ShowPlayedGames()
    {
        string folderPath = Directory.GetCurrentDirectory() + "/Assets/Resources/";
        Vector3 position = startingPoint;
        string[] files = System.IO.Directory.GetDirectories(Directory.GetCurrentDirectory() + "/Assets/Resources/");
        Debug.Log(files[0]);
        foreach (string file in files)
        {
            GameObject game = Instantiate(recordedGamePrefab);
            game.GetComponent<RectTransform>().SetParent(scrollView.transform, false);
            game.transform.localPosition = position;
            

            string[] splitArray = file.Split(folderPath, System.StringSplitOptions.None);
            game.GetComponentInChildren<TextMeshProUGUI>().text = splitArray[1];
            game.GetComponent<Button>().onClick.AddListener(delegate { Selection(game.GetComponentInChildren<TextMeshProUGUI>().text); });

            if (fileNumber < 3)
            {
                fileNumber++;
                position += new Vector3(startingPoint.x + padding, 0, 0);
            }
            else
            {
                position = new Vector3(startingPoint.x, startingPoint.y - startingPoint.x - padding, 0);
                fileNumber = 0;
            }


        }
    }

    // button action on click to start showing selected game
    public void Selection(string fileName)
    {
        selectedFileName = fileName;
        viewGamesMenu.SetActive(false);
        viewGame.SetActive(true);
    }


    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
