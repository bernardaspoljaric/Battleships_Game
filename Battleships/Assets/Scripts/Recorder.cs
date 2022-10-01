using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Recorder : MonoBehaviour
{
    private string folderPath;
    private int screenshotNumber;

    [SerializeField] private UIManager UIManager;

    private void Start()
    {
        folderPath = Directory.GetCurrentDirectory() + "/Assets/Resources/" + PlayerPrefs.GetString("playerOne") + " VS. " + PlayerPrefs.GetString("playerTwo") + " " + System.DateTime.Now.ToString("dd/MM/yyyy") + "/";
        screenshotNumber = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeScreenshot();
        }
    }

    // while playing game each click makes screenshot
    private void TakeScreenshot()
    {
        screenshotNumber++;
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        var screenshotName = "Screenshot_" + screenshotNumber + ".png";
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName));
        Debug.Log(folderPath + screenshotName);
    }

    // start recording if toggle is on
    public void CheckIfShouldRecord()
    {
        if (UIManager.recordToggle.isOn)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
