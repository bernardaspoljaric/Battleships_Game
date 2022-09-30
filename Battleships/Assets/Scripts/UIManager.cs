using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerOneInputField;
    [SerializeField] private TMP_InputField playerTwoInputField;
    [SerializeField] private Button startGameButton;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private TMP_Text winPlayerNameText;
    [SerializeField] private GameObject scrollView;
    [SerializeField] private RectTransform recordedGamePrefab;
    [SerializeField] private RawImage slideshow;

    private List<Image> screenshots = new List<Image>();
    private float padding = 200;
    private int fileNumber = 0;
    private Vector3 startingPoint = new Vector3(220, -220, 0);

    public float t;
    Texture2D files;
    string pathPreFix = "file://";
    Texture2D[] textureList;

    private void Start()
    {
        //ShowPlayedGames();
        StartViewOfGame("me VS. you 30.09.2022");
    }
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

    public void ShowWinMenu(bool player)
    {
        winMenu.SetActive(true);
        gameMenu.SetActive(false);
        SetWinName(player);
    }

    public void ShowPlayedGames()
    {
        string folderPath = Directory.GetCurrentDirectory() + "/Assets/Resources/";
        Vector3 position = startingPoint;
        string[] files = System.IO.Directory.GetDirectories(Directory.GetCurrentDirectory() + "/Assets/Resources/");
        Debug.Log(files[0]);
        foreach (string file in files)
        {
            RectTransform game = Instantiate(recordedGamePrefab);
            game.SetParent(scrollView.transform, false);
            game.localPosition = position;
            string[] splitArray = file.Split(folderPath, System.StringSplitOptions.None);
            game.GetComponentInChildren<TextMeshProUGUI>().text = splitArray[1];
            Debug.Log(fileNumber);

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

    private void StartViewOfGame(string fileName)
    {
        string folderPath = Directory.GetCurrentDirectory() + "/Screenshots/" + fileName;
        string path = @folderPath;
        files = Resources.Load("me VS. you 30.09.2022/Screenshot_1") as Texture2D;
        slideshow.GetComponent<RawImage>().texture = files;
        //files = System.IO.Directory.GetFiles(path);
        //Debug.Log(files[3]);
        //StartCoroutine(LoadImages());
    }

    //public void Play()
    //{
    //    for (int i = 0; i < textureList.Length; i++)
    //    {
    //        Debug.Log(textureList[i]);
    //        StartCoroutine(Display(t, textureList[i]));
    //    }
    //}

    //private IEnumerator LoadImages()
    //{
    //    textureList = new Texture2D[files.Length];
    //    int fileCount = 0;
    //    foreach (string file in files)
    //    {
    //        string pathTemp = pathPreFix + file;
    //        WWW www = new WWW(file);
    //        yield return www;
    //        Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
    //        www.LoadImageIntoTexture(texTmp);

    //        textureList[fileCount] = texTmp;
    //        fileCount++;
    //    }
    //}

    private IEnumerator Display(float seconds, Texture2D image)
    {
        Debug.Log("idee");
        slideshow.GetComponent<Renderer>().material.mainTexture = image;
        yield return new WaitForSeconds(seconds);
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
