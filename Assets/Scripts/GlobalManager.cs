using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public static class GlobalManager
{
    public static int gameStage = -1; 
    // -1 - игрок в меню
    // 0 - игра не начата
    // 1 - игра начата

    public static UnityEvent<List<Rigidbody>> explosion = new UnityEvent<List<Rigidbody>>();

    public static XRController rightHandController;
    public static XRController leftHandController;

    public static List<GameObject> bulletSpots = new List<GameObject>();
    public static float bulletSpotLifetime = 60f;

    public static GameObject ricochetEffect;
}
