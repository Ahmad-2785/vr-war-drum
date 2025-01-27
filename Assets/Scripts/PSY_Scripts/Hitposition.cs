﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitposition : MonoBehaviour {

    private Transform[] firefrom;

    // Use this for initialization
    void Start () {
        firefrom = new Transform[4];
        firefrom[0] = GameObject.Find("LeftGun").transform;
        firefrom[1] = GameObject.Find("LeftRocket").transform;
        firefrom[2] = GameObject.Find("RightGun").transform;
        firefrom[3] = GameObject.Find("RightRocket").transform;

        //Vector3 distance = new Vector3(0.0f, 0.0f, 150.0f * 0.1f);

        if (gameObject.name == "LeftBullet")
        {
            transform.position = firefrom[0].position + firefrom[0].up * 15;//distance;
        }
        else if (gameObject.name == "LeftRay")
        {
            transform.position = firefrom[1].position + firefrom[1].forward * 15;
        }
        else if (gameObject.name == "RightBullet")
        {
            transform.position = firefrom[2].position + firefrom[2].up * 15;
        }
        else if (gameObject.name == "RightRay")
        {
            transform.position = firefrom[3].position + firefrom[3].forward * 15;
        }
        else if (gameObject.name == "HitYellow")
        {
            Vector3 firefrom_position = (firefrom[1].position + firefrom[3].position) / 2;
            transform.position = firefrom_position - Vector3.forward * 200;
        }
        //Debug.Log(transform.position);
    }
	
	// Update is called once per frame
	void Update () {
    }
}
