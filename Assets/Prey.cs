using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Prey : Agent
{
    private Vector2 _startPosition;
    
    [Range(0f, 10f)]
    public float speed = 1;
    
    public override void Initialize()
    {
        base.Initialize();

        _startPosition = transform.localPosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);

        Vector2 position = transform.localPosition;
        sensor.AddObservation(position);

        Vector2 predatorPosition = GameObject.FindGameObjectWithTag("Predator").transform.localPosition;
        sensor.AddObservation(predatorPosition);
        
        AddReward(-Vector2.Distance(position, predatorPosition));
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);
        
        // Continuous, cartesian actions
        var action = actions.ContinuousActions;

        var direction = new Vector2(action[0], action[1]);
        var velocity = Vector2.ClampMagnitude(direction, 1) * speed;
        
        // Debug.Log("Taking an action?");
        Debug.Log($"Taken action: {velocity[0]}, {velocity[1]}");

        transform.Translate(velocity * Time.fixedDeltaTime);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // base.Heuristic(in actionsOut);

        var action = actionsOut.ContinuousActions;

        if (Input.GetKey(KeyCode.D))
        {
            action[0] = 1f;
        } else if (Input.GetKey(KeyCode.A))
        {
            action[0] = -1f;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            action[1] = 1f;
        } else if (Input.GetKey(KeyCode.S))
        {
            action[1] = -1f;
        }
        
        // Debug.Log($"Heuristic action: {action[0]}, {action[1]}");
    }
}
