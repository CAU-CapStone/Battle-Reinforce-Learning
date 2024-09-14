using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;

public class AgentControl : Agent
{
    private Vector3 originPosition;
    private Rigidbody rb;
    public List<BallControl> ballScripts;
    
    // Start is called before the first frame update
    public override void Initialize()
    {
        originPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();

        Transform parent = transform.parent;
        foreach (Transform sibling in parent)
        {
            if (sibling.CompareTag("Ball"))
                ballScripts.Add(sibling.gameObject.GetComponent<BallControl>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        float magnitude = 30.0f * Mathf.Clamp(actions.ContinuousActions[0], 0f, 1f);
        
        if (magnitude > 25.0f)
            AddReward(1f);
            
        float angle = 180.0f * Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        
        rb.AddForce(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * magnitude);
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AgentBallCollider"))
            AddReward(-100f);
    }

    public override void OnEpisodeBegin()
    {
        transform.position = originPosition;
        
        foreach (var ballScript in ballScripts)
        {
            ballScript.transform.position = ballScript.originPosition;
            
            var angle = Random.Range(0.0f, 360.0f);
            var speed = ballScript.originSpeed;
            ballScript.speed = Random.Range(speed * 0.9f, speed * 1.1f);
            ballScript.moveDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        }
    }
}
