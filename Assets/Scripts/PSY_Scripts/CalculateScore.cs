﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateScore : MonoBehaviour {

    static public int score = 0;
    static public int combo = 0;
    static public int maxcombo = 0;

    static public string PreviousHit = "";

    private Transform[] perfecthitpositions;

	// Use this for initialization
	void Start () {
        perfecthitpositions = new Transform[5];
        perfecthitpositions[0] = GameObject.Find("LeftBullet").transform;
        perfecthitpositions[1] = GameObject.Find("LeftRay").transform;
        perfecthitpositions[2] = GameObject.Find("RightBullet").transform;
        perfecthitpositions[3] = GameObject.Find("RightRay").transform;
        perfecthitpositions[4] = GameObject.Find("HitYellow").transform;

        score = 0;
        maxcombo = 0;
        combo = 0;
        PreviousHit = "";
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(score);
    }

    void calScore(object[] obj)
    {
        //Debug.Log(obj[0] + " " + obj[1]);

        int index = (int)obj[0];
        Vector3 hitpoint = (Vector3)obj[1];

        if (index < 5)
        {
            if ((hitpoint - perfecthitpositions[index - 1].position).magnitude < 1)
            {
                //Debug.Log("Perfect");
                score += 2 * 100 + combo * 50;
                PreviousHit = "Perfect!!";
            }
            else
            {
                score += 1 * 100 + combo * 50;
                PreviousHit = "Good!";
            }
        }
        else
        {
            if ((hitpoint - perfecthitpositions[index - 1].position).magnitude < 1)
                score += 1 * 25;
        }
        //Debug.Log("score: " + score);
    }

    void calCombo(bool comboing)
    {
        if (!comboing)
        {
            if(combo > maxcombo)
            {
                maxcombo = combo;
            }
            combo = 0;
            PreviousHit = "Miss...";
        }
        else
        {
            combo++;
        }
        //Debug.Log("combo: " + combo);
    }
}
