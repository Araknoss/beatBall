using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleReset : MonoBehaviour
{
   [SerializeField] private PruebaMovimiento pruebaMovimiento;
   private Vector3 startPosition;
   private Quaternion startRotation;
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private GameObject triangleExplosion;
   [SerializeField] private Collider2D polygonCollider;
   [SerializeField] private SpriteRenderer spriteRenderer;
   [SerializeField] private GameObject triangleAnim;

   [SerializeField] private float ratioCoefficient;
    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        //Screen ratio
        ratioCoefficient = gameObject.GetComponentInParent<Playground>().ratioCoefficient;
        rb.mass /= ratioCoefficient;

    }
    private void Update()
    {
        if (pruebaMovimiento.resetCounter == true)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            transform.position = startPosition;
            transform.rotation = startRotation;
            polygonCollider.enabled = true;
            spriteRenderer.enabled = true;
            triangleAnim.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Instantiate(triangleExplosion, transform.position,transform.rotation);
            polygonCollider.enabled = false;
            spriteRenderer.enabled = false;
            triangleAnim.SetActive(false);
        }
    }
}
