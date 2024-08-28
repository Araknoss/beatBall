using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCircles : MonoBehaviour
{
    private float timer;
    [SerializeField] private bool right;
    private void Start()
    {
        timer = 0;
        Move();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            timer = 0;
            Move();
        }
    }

    public void Move()
    {
       
        
       if (right)
        {
            Vector2 movement = new Vector2(transform.position.x, transform.position.y) - Vector2.one;
            transform.LeanMove(movement, 2).setEaseOutBack();
                        
        }
        else
        {
            Vector2 movement = new Vector2(transform.position.x, transform.position.y) + Vector2.one;
            transform.LeanMove(movement, 2).setEaseOutBack();
        }
       
    }
}
