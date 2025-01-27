﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumStick : MonoBehaviour {
    public Transform Target;
    public float ReturnSpeed = 1.0f;
    public float FollowRange = 1.0f;
    public float ResetRange = 2.0f;

    public GameObject HitEffect;
    public Vector3 EffectScale = Vector3.one;

    public DrumScript.DrumType HittedDrumType = DrumScript.DrumType.NotADrum;
    public DrumScript.DrumPosition HittedDrumSide = DrumScript.DrumPosition.None;

    [SerializeField] private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    private Rigidbody _selfRigidbody;
    private bool _lock = false;

	// Use this for initialization
	void Start () {
        _selfRigidbody = GetComponent<Rigidbody>();
        transform.position = trackedObj.transform.position;
    }

    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    void Update () {
        if (device == null) { return; }
        Vector3 tmp_heading = Target.position - transform.position;
        float tmp_distance = tmp_heading.magnitude;
        Vector3 tmp_dir = tmp_heading / tmp_distance;

        if (tmp_heading.sqrMagnitude > FollowRange * FollowRange)
        {
            _selfRigidbody.velocity = device.velocity + (tmp_dir * ReturnSpeed); //transform.forward;
            _selfRigidbody.angularVelocity = device.angularVelocity;

        }
        else if(!_lock){
            _selfRigidbody.velocity = device.velocity;
            _selfRigidbody.angularVelocity = device.angularVelocity;
        }

    }

    void OnCollisionEnter(Collision c)
    {
        _selfRigidbody.velocity = Vector3.zero;
        
        if(c.collider.tag.Contains("Drum"))
        {
            DrumScript tmp_drum = c.gameObject.GetComponent<DrumScript>();
            float tmp_acceptableForce = tmp_drum.AcceptableHittingForce;
            if (c.relativeVelocity.magnitude >= tmp_acceptableForce)
            {
                if (HitEffect && tmp_drum._canFire)
                {
                    GameObject tmp_FX = Instantiate(HitEffect, transform.position, transform.rotation);
                    tmp_FX.transform.localScale = EffectScale;
                    Destroy(tmp_FX, tmp_FX.GetComponent<ParticleSystem>().main.duration+0.2f);
                }
                device.TriggerHapticPulse(3000);
                HittedDrumType = tmp_drum.Type;
                HittedDrumSide = tmp_drum.Side;
            }
            else
            {
                device.TriggerHapticPulse(500);
            }
        }
        _lock = true;
    }
    
    void OnCollisionStay(Collision c)
    {
        _selfRigidbody.velocity = Vector3.zero;
        HittedDrumType = DrumScript.DrumType.NotADrum;
        HittedDrumSide = DrumScript.DrumPosition.None;
    }

    void OnCollisionExit(Collision c)
    {
        if (c.collider.tag.Contains("Drum"))
        {
            device.TriggerHapticPulse(500);
            HittedDrumType = DrumScript.DrumType.NotADrum;
            HittedDrumSide = DrumScript.DrumPosition.None;
        }
        _lock = false;
    }
}
