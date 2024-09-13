using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;

public class AgentControl : Agent
{
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        AddReward(-1f);
        
        float accelerator = Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        float handle = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        
        rb.AddTorque(transform.up * handle * 30);
        rb.AddForce(transform.forward * accelerator * 30);
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            AddReward(-100f);
        }
    }
}
