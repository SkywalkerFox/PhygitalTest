using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public static float sliderValue = 0.5f;
    public static bool mouseOverUI = false;
    public static GameObject currentBall;

    public CameraController camController;

    public List<GameObject> balls;

    private int index;

    private void Start()
    {
        currentBall = balls[0];
        index = 0;
    }

    public void SetSliderValue(Slider slider)
    {
        sliderValue = slider.value;
    }

    public void Right()
    {
        currentBall.GetComponent<BallController>().PauseMovement();

        if (index == balls.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        currentBall = balls[index];
        camController.target = currentBall.transform;
    }

    public void Left()
    {
        currentBall.GetComponent<BallController>().PauseMovement();
        
        if (index == 0)
        {
            index = balls.Count - 1;
        }
        else
        {
            index--;
        }
        currentBall = balls[index];
        camController.target = currentBall.transform;
    }

    public void OnPointerEnter()
    {
        mouseOverUI = true;
    }

    public void OnPointerExit()
    {
        mouseOverUI = false;
    }
}