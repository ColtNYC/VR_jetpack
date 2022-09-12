using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class JetPackInput : MonoBehaviour
{

    //get the info from OVR Input
    //set the current thrust based on that
    // send out info as event to thrustcontroller

    [SerializeField]
    private OVRInput.Controller m_controller = OVRInput.Controller.None;

    [SerializeField]
    private Transform engine, fan;

    [SerializeField]
    private float minFanRotationSpeed, maxFanRotationSpeed;

    private float currentFanRotationSpeed;

    private float currentThrustInput;
    

    private Vector3 currentForce;

    public Vector3 CurrentForce { get => currentForce; }

    
    [SerializeField]
    private float engineRotationSpeed;


    public event Action<Vector3> EvtThrustChanged = delegate {};
    public event Action<float> EvtThrustInputChanged = delegate{};

    //needs to send out actual force. or at least transform and thrust
    //maybe just use one here and the thrust controller mulitplies it. again Update about force changes, not the force
    

     
    private void Start()
    {
        ThrustInputChanged(0f);
    }

    // Update is called once per frame
    private void Update()
    {
        float newInput = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);

        if(newInput != currentThrustInput)
        {
            ThrustInputChanged(newInput);
        }

        if (OVRInput.Get(OVRInput.Button.One, m_controller))
            engine.Rotate(new Vector3(engineRotationSpeed * Time.deltaTime, 0f,0f));

        else if (OVRInput.Get(OVRInput.Button.Two, m_controller))
            engine.Rotate(new Vector3(-engineRotationSpeed * Time.deltaTime, 0f, 0f));

        fan.Rotate(new Vector3(0f,0f, currentFanRotationSpeed * Time.deltaTime));

        
    }

    public void ThrustInputChanged(float newInput)
    {
        currentThrustInput = newInput;
        currentForce = newInput * engine.forward;

        //CurrentThrust = Mathf.Clamp(CurrentThrust + newInput, minThrust, maxThrust);

        //thrustInPercentOfMax = (CurrentThrust - minThrus) / (maxThrus - minThrust);

        currentFanRotationSpeed = newInput * maxFanRotationSpeed + (1f - newInput) * minFanRotationSpeed;

        EvtThrustInputChanged(newInput);
        EvtThrustChanged(currentForce);
    }
}
