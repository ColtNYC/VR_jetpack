using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem sys;

    [SerializeField]
    private JetPackInput jetPackInput;

    [SerializeField]
    private ThrustController thrustController;

    [SerializeField]
    private float minStartSpeed, maxStartSpeed;

    [SerializeField]
    private Vector3 minPosition, maxPosition;

    [SerializeField]
    private float minEmmision, maxEmmision;

    private ParticleSystem.EmissionModule sysEmit;

    private ParticleSystem.MainModule sysMain;


    private void OnEnable()
    {
        jetPackInput.EvtThrustInputChanged += SetStartSpeed;

        if (minPosition != maxPosition)
            jetPackInput.EvtThrustInputChanged += SetEmission;

        if (minEmmision != maxEmmision)
            jetPackInput.EvtThrustInputChanged += SetEmission;

        sysEmit = sys.emission;

        sysMain = sys.main;
    }

    private void SetStartSpeed(float thrustInPercent)
    {
        float speed = thrustInPercent * maxStartSpeed + (1f - thrustInPercent) * minStartSpeed;
        
        sysMain.startSpeed = speed;
    }

    private void SetPosition(float thrustInPercent)
    {
        Vector3 pos = thrustInPercent * maxPosition + (1f - thrustInPercent) * minPosition;

        sys.transform.localPosition = pos;
    }



private void OnDisable()
{
    jetPackInput.EvtThrustChanged -= SetStartSpeed;

    if(minPosition != maxPosition)
    jetPackInput.EvtThrustChanged -= SetPosition;
}
    

[ContextMenu("SetMinPosition")]

private void SetMinPosition()
{
    minPosition = sys.transform.localPosition;
}

[ContextMenu("SetMaxPosition")]

private void SetMaxPosition()
{
    maxPosition = sys.transform.localPosition;
}
        
    
}
