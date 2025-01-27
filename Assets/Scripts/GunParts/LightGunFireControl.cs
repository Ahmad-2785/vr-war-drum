﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGunFireControl : GunFireControl {


    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        /*****************/
        /* test hit drum */
        /*****************/
        if (Input.GetKeyDown(DebugKey))
        {
            FireTheGun(_testPos);
        }
    }

    void FireTheGun(Vector3 gunPointPos)
    {
        Instantiate(Bullet, gunPointPos, Quaternion.identity);
        _selfAnimator.Play(AnimationName, -1, 0f);
    }
}
