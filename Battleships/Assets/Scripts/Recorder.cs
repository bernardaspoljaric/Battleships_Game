using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Recorder : MonoBehaviour
{
    private string folderPath;
    private int screenshotNumber;

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
    private void TakeScreenshot()
    {
        screenshotNumber++;
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        var screenshotName = "Screenshot_" + screenshotNumber + ".png";
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName));
        Debug.Log(folderPath + screenshotName);
    }
}