using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallControl : MonoBehaviour
{
    public float speed = 1.0f;
    private Vector3 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        var angle = Random.Range(0.0f, 360.0f);
        moveDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * moveDirection.normalized;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            moveDirection = Vector3.Reflect(moveDirection, other.contacts[0].normal);
        }
    }
}
