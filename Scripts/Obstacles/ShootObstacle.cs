using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObstacle : MonoBehaviour
{
    [SerializeField] private float startTimer;
    [SerializeField] private float shotTimer;
    [SerializeField] private Transform shootController;
    private float timer;

    [Header("Projectile")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float destroyTimer;
    [SerializeField] private Vector3 rotation;

    [SerializeField] private Vector2 speed;
    [SerializeField] private Vector3 rescaled;

    [SerializeField] private float ratioCoefficient;
    // Update is called once per frame
    private void Start()
    {
        timer = startTimer;
        //Screen ratio
        ratioCoefficient = gameObject.GetComponentInParent<Playground>().ratioCoefficient;
    }
    void Update()
    {
        
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            GameObject clone = Instantiate(projectile, shootController.position,Quaternion.Euler(rotation));
            clone.transform.localScale *= ratioCoefficient; //Para que los proyectiles se reescalen
            Projectile cloneScript = clone.GetComponent<Projectile>();
            Transform projectileImage = clone.gameObject.transform.GetChild(0);
            projectileImage.rotation = Quaternion.Euler(rotation);
            cloneScript.destroyTimer = destroyTimer;
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.velocity = transform.TransformDirection(speed);
            timer = shotTimer;
        }
    }
    
}
