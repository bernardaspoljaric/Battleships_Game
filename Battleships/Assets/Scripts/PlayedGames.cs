using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayedGames : MonoBehaviour
{
    [SerializeField] private UIManager UIManager;
    [SerializeField] private RawImage slideshow;

    public float t; // time one image last -- affects the speed of showing played game 
    private int screnshotNumber = 0;
    private Texture2D file;

    Object[] screenshots;
   
    private void Start()
    {
        UIManager.gameName.text = UIManager.selectedFileName;
        UIManager.playButton.GetComponent<Button>().onClick.AddListener(delegate { StartViewOfGame(UIManager.selectedFileName); });
    }

    // method that starts showing all screnshots taken in single game
    private void StartViewOfGame(string fileName)
    {
        string folderPath = Directory.GetCurrentDirectory() + "/Assets/Resources/" + fileName;
        string path = @folderPath;

        screenshots = Resources.LoadAll("me VS. you 30.09.2022");
        Debug.Log(screenshots.Length);

        StartCoroutine(LoadImages(fileName, t));
    }

    // go to next screenshot
    public void GoForward()
    {
        StopAllCoroutines();

        StartCoroutine(LoadImages(UIManager.selectedFileName, t));
    }

    // go to prewious screenshot
    public void GoBackward()
    {
        StopAllCoroutines();
        screnshotNumber -= 2;
        StartCoroutine(LoadImages(UIManager.selectedFileName, t));
    }

    // corutine for showing screenshots at certain speed
    private IEnumerator LoadImages(string fileName, float seconds)
    {
        for (int i = screnshotNumber; i < screenshots.Length; i++)
        {
            screnshotNumber++;
            file = Resources.Load(fileName + "/Screenshot_" + screnshotNumber) as Texture2D;
            Debug.Log("/Screenshot_" + screnshotNumber);
            slideshow.GetComponent<RawImage>().texture = file;
            
            yield return new WaitForSeconds(seconds);
        }

        slideshow.gameObject.SetActive(false);
        UIManager.playButton.interactable = true;
        UIManager.forwardButton.interactable = false;
        UIManager.backwardButton.interactable = false;
    }

    // method for setting speed -- can't be negative or 0
    public void SetSpeed()
    {
        string inputSpeed = UIManager.speedInputField.text;
        if (inputSpeed == "")
        {
            t = 2;
        }
        else
        {
            if (inputSpeed.Contains("-") || inputSpeed == "0")
            {
                UIManager.speedInputField.text = "2";
                t = 2;
            }
            else
            {
                t = float.Parse(inputSpeed);
            }
        }
    }


}
