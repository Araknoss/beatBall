using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float destroyTimer;
    private float timer;
    [SerializeField] bool independentMovement;
    [SerializeField] Vector2 speed;
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        timer = destroyTimer;
        
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }

        if (independentMovement)
        {
            rb.velocity = transform.TransformDirection(speed);
        }
    }
}
