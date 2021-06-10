using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private void FixedUpdate()
    {
        foreach (Agent agent in FindObjectsOfType<Agent>())
        {
            // agent.RequestDecision();
        }
    }
}
