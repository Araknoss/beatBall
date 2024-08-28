using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallUpgrades : MonoBehaviour
{
    [Header("Movement")]
    public float potencia; //24
    public float inputDistance;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool touchBall;
    private Camera cam;
    private Vector2 speed; //Vector velocidad, distancia de pulsaci�n entre tiempo de pulsaci�n
    public float minDragDistance; //4
    public float maxDragDistance; //20
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 currentPoint;
    [SerializeField] private float dragginTime;


    [Header("Trail Renderer")]
    [SerializeField] private TrailRenderer tr;


    [Header("DragCurve")]
    [SerializeField] private TryLine tl;


    [Header("Reset")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool reset;
    private Vector3 startPosition; //Posici�n inicial de la bola
    public bool resetCounter;
    [SerializeField] private float counter;
    [SerializeField] private float resetTime;



    [Header("Trigger")]
    [SerializeField] private AnimationCurve effectCurve;
    private bool trigger;
    private Transform goalEffect;
    private Transform portalMask;
    private Transform bigPortalMask;
    private Transform backCollider;
    [SerializeField] private float bigMaskTime;
    [SerializeField] private Transform attractionPoint;
    [SerializeField] private float attractionStrength;
    [SerializeField] private ParticleSystem attractionEffect;
    [SerializeField] GameObject explosion;


    [Header("Transition")]
    [SerializeField] private Collider2D cl;
    [SerializeField] private float destroyTime;
    [SerializeField] public bool transitioning;
    [SerializeField] private string nextLevel;

    [SerializeField] private GameObject upgradeButton;
    // Start is called before the first frame update
    void Start()
    {
        //Movement      
        rb.bodyType = RigidbodyType2D.Static;

        touchBall = false;
        cam = Camera.main;
        startPoint.z = 15;

        //Reset
        reset = false;
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Trigger
        trigger = false;
        //Transition
        transitioning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Upgrades")
        {
            StartCoroutine(ResetGoal());
            RythmPortalUpgrades rythmPortal=collision.gameObject.GetComponent<RythmPortalUpgrades>();
            rythmPortal.Goal();
        }

        if (collision.gameObject.tag != "Upgrades")
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(Vector3.zero));
            StartCoroutine(Reset());
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Upgrades")
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(Vector3.zero));
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        transitioning = true;
        tr.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = startPosition;
        yield return new WaitForSeconds(0.3f);
        trigger = false;
        reset = false;
        cl.enabled = true;
        transitioning = false;
        yield return null;
        
    }

    IEnumerator ResetGoal()
    {
        transitioning = true;
        rb.velocity = rb.velocity * 0.2f;
        cl.enabled = false; //Desactivamos collider de la bola
        trigger = true;
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        trigger = false;
        rb.bodyType = RigidbodyType2D.Static;
        tr.enabled = false;
        reset = false;
        transform.position = startPosition;
        animator.SetTrigger("Open");
        cl.enabled = true;

        //Button
        upgradeButton.SetActive(true);
        UpgradeButton ub = upgradeButton.GetComponent<UpgradeButton>();
        ub.ActiveButton();
        

        yield return new WaitForSeconds(1f);
        transitioning = false;
        yield return null;
    }


    void Update()
    {
        if (trigger == true)
        {
            Vector3 direction = (attractionPoint.position - transform.position).normalized;
            rb.AddForce(direction * attractionStrength);
        }
        if (transitioning == false)
        {
            if (reset == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startPoint = cam.ScreenToWorldPoint(Input.mousePosition); //Le damos los valores de posici�n dentro de la c�mara, si no tendr�amos valores altos porque contar�a los pixeles
                    startPoint.z = 15;
                    touchBall = true;
                }
                if (Vector2.Distance(new Vector2(startPoint.x, startPoint.y), new Vector2(transform.position.x, transform.position.y)) < inputDistance && touchBall == true) //Comprobamos distancia entre input y bola para que no se pueda lanzar desde cualquier sitio. Esto da una sensaci�n de mayor control sobre el lanzamiento.
                {                                                                                                                                                         //Tambien comprobamos que se ha vuelto a pulsar la pantalla para lanzar la bola

                    if (Input.GetMouseButton(0))
                    {

                        currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                        currentPoint.z = 15;
                        dragginTime += Time.deltaTime;
                        if (dragginTime <= 1.5f)
                        {
                            tl.RenderCurve();
                        }

                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        rb.bodyType = RigidbodyType2D.Dynamic;
                        tr.enabled = true;
                        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                        endPoint.z = 15;
                        speed = new Vector2(endPoint.x - startPoint.x, endPoint.y - startPoint.y) / (1.2f + dragginTime); //Vector velocidad
                        float dragDistance = Vector2.Distance(new Vector2(startPoint.x, startPoint.y), new Vector2(endPoint.x, endPoint.y)); //M�dulo distancia
                        Debug.Log(dragDistance);

                        rb.AddForce(speed * potencia / (Mathf.Clamp(dragDistance * 0.8f, minDragDistance, maxDragDistance)), ForceMode2D.Impulse); //Divido entre la distancia recorrida con el dedo aplicandole un factor para que cuanto mas lejos arrastres, mas fuerte lances pero limitandolo

                        tl.EndLine();
                        dragginTime = 0;
                        touchBall = false;

                        reset = true;
                    }
                }
            }
        }
    }
}
