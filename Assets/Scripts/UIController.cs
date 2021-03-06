﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController instance = null;



    public float RedBuildPointPercent, BlueBuildPointPercent;
    public int RedBuildPoints, BlueBuildPoints;
    public int RedCrowd, BlueCrowd;

    public UnityEngine.UI.Image RedCircle, BlueCircle;
    public UnityEngine.UI.Text RedBuildPointText, BlueBuildPointText, RedCrowdText, BlueCrowdText;

    void FixedUpdate()
    {
        UpdateCanvas();
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        } if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        RedBuildPointPercent += Time.deltaTime/10.0f;
        BlueBuildPointPercent += Time.deltaTime/10.0f;
        if (RedBuildPointPercent > 1.0f)
        {
            RedBuildPointPercent = 0.0f;
            RedBuildPoints++;
            UpdateCanvas();
        }
        if (BlueBuildPointPercent > 1.0f)
        {
            BlueBuildPointPercent = 0.0f;
            BlueBuildPoints++;
            UpdateCanvas();
        }
    }

    void UpdateCanvas()
    {
        RedCircle.fillAmount = RedBuildPointPercent;
        BlueCircle.fillAmount = BlueBuildPointPercent;
        RedBuildPointText.text = "BUILD POINTS: " + RedBuildPoints;
        BlueBuildPointText.text = "BUILD POINTS: " + BlueBuildPoints;
        RedCrowdText.text = "CROWD: " + RedCrowd;
        BlueCrowdText.text = "CROWD: " + BlueCrowd;
    } 


    public void UpdateRedCrowd (int i) {
        RedCrowd += i;
    }

    public void UpdateBluCrowd (int i) {
        BlueCrowd += i;
    }
}
