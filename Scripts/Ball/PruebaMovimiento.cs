using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PruebaMovimiento : MonoBehaviour
{
    [Header("Movement")]
    public float potencia; //24
    public float inputDistance;
    [SerializeField] private Rigidbody2D rb;
    private float startMass;
    private float startDrag;
    private bool touchBall;
    private Camera cam;
    private Vector2 speed; //Vector velocidad, distancia de pulsaci�n entre tiempo de pulsaci�n
    public float minDragDistance; //4
    public float maxDragDistance; //20
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 currentPoint;
    private float dragginTime;


    [Header("Trail Renderer")]
    [SerializeField] private TrailRenderer tr;
    

    [Header("DragCurve")]
    [SerializeField] private TryLine tl;
    

    [Header("Reset")]
    private bool reset;
    private bool deathReset;
    private Vector3 startPosition; //Posici�n inicial de la bola
    public bool resetCounter;
    private float counter;
    [SerializeField] private float resetTime;


    [Header("Collision")]
    [SerializeField] private int cleanShotCollisions;
    [SerializeField] private Animator an;
    //[SerializeField] private Vector2 velocityLimit;
    [SerializeField] private Color targetColor;
    [SerializeField] private Color startColor;
    [SerializeField] private Color deadColor;
    [SerializeField] private float returnSpeed;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private AnimationCurve audioCurve;
    [SerializeField] private AnimationCurve colorCurve;
    [SerializeField] private float lerpDuration;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject collidePoints;
    [SerializeField] private PhysicsMaterial2D bounciness;
    private int collisions;
    
    

    [Header("Trigger")]
    private bool trigger;
    private Transform goalEffect;
    private Transform portalMask;
    private Transform bigPortalMask;
    private Transform backCollider;
    //[SerializeField] private float bigMaskTime;
    [SerializeField] private Transform attractionPoint;
    [SerializeField] private float attractionStrength;
    [SerializeField] private ParticleSystem attractionEffect;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject hpExplosion;


    [Header("Transition")]
    [SerializeField] private Collider2D cl;
    [SerializeField] private float destroyTime;
    public bool transitioning;
    //[SerializeField] private string nextLevel;


    [Header("HP")]
    [SerializeField] public HP hp;


    [Header("Next")]
    [SerializeField] public Next next;


    [Header("Points")]
    [SerializeField] public Points points;


    [Header("Level Controller")]
    [SerializeField] public LevelController lc;


    [Header("Audio Manager")]
    [SerializeField] private AudioManager am;

    [Header("Playground")]
    [SerializeField] private Playground pg;

    private void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb.bodyType = RigidbodyType2D.Static;
        
        //Reescalamos a la resolución actual
        pg = gameObject.GetComponentInParent<Playground>();
        startMass = 1 / pg.ratioCoefficient; //1
        startDrag = 0.2f / pg.ratioCoefficient; //0.3f
        tr.startWidth = 0.7f * pg.ratioCoefficient;

        PhysicsReset();
    }
    private void Awake()
    {
        //Movement      
        touchBall = false;
        cam = Camera.main;
        startPoint.z = 15;

        //Reset
        reset = false;
        deathReset = false;
        

        //Trigger
        trigger = false;
        //Transition
        //cl = GetComponent<Collider2D>();
        an = GetComponent<Animator>();
        transitioning = false;

        //HP
        hp = GameObject.FindWithTag("LevelCanvas").GetComponent<HP>();

        //Next
        next = GameObject.FindWithTag("LevelCanvas").GetComponent<Next>();

        //Points
        points = GameObject.FindWithTag("LevelCanvas").GetComponent<Points>();

        //Level controller
        lc = GameObject.FindWithTag("LevelController").GetComponent<LevelController>();

        //Sounds
        am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }


    private void OnCollisionEnter2D(Collision2D collision) //Colision con las paredes
    {
        float volume = Mathf.Lerp(0,0.5f, audioCurve.Evaluate(rb.velocity.magnitude/20f));
        am.PlayAudio(0, volume);
        Color color = Color.Lerp(startColor, targetColor, colorCurve.Evaluate(rb.velocity.magnitude / 20f));
        sr.color = color;

        if (collision.gameObject.tag != "Portal")// && velocityLimit.y < rb.velocity.y && velocityLimit.x < rb.velocity.x)
        {                   
            //friction += 1;
            bounciness.bounciness -= 0.01f;
            rb.drag += 0.005f;
            
            collisions += 1;
            if (collisions > 40)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
                rb.drag = 1;
                rb.mass = 5;
                sr.color = deadColor;
            }

            //StartCoroutine(LerpColor(targetColor,startColor,lerpDuration));

            //Puntos
            //Vector2 relativeVelocity = collision.relativeVelocity;
            //Debug.DrawLine(transform.position, relativeVelocity,Color.red,3f);
            //if (StaticStats.collidePoints > 0)
            //{
            //    points.UpCollisionPoints();
                /*PointsMovement pointsMovement = collidePoints.GetComponent<PointsMovement>();
                pointsMovement.targetPositionOffset = relativeVelocity.normalized;
                Instantiate(collidePoints, transform.position, transform.rotation);    */            
            //}
        }

        if (collision.gameObject.tag == "Reactive")
        {
            ReactiveAnim reactive = collision.gameObject.GetComponent<ReactiveAnim>();
            reactive.React();
        }

        if (collision.gameObject.tag == "Active")
        {
            Activation activation = collision.gameObject.GetComponentInParent<Activation>();
            activation.Activate();
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            cl.enabled = false;
            rb.gravityScale = 0;
            rb.velocity = rb.velocity * 0.1f;
            trigger = true;
            goalEffect = collision.gameObject.transform.GetChild(0); //Efecto particulas gol
            goalEffect.gameObject.SetActive(true);
            portalMask = collision.gameObject.transform.GetChild(4).GetChild(0);
            portalMask.gameObject.SetActive(true);
            bigPortalMask = collision.gameObject.transform.GetChild(5);
            bigPortalMask.gameObject.SetActive(true);
            Animator portalAnimator = collision.gameObject.GetComponent<Animator>();
            portalAnimator.SetTrigger("Close");
            an.SetTrigger("Close");
            attractionPoint = collision.gameObject.transform.GetChild(3);
            attractionEffect= collision.gameObject.transform.GetChild(2).GetComponent<ParticleSystem>(); //Parar partículas
            StartCoroutine(LerpEffect(10,0,1f));

            transitioning = true;

            if (/*StaticStats.collideCounter*/ collisions <= cleanShotCollisions)
            {
                points.CleanShot();
            }

            points.UpPoints();
            
                        
            
            //StartCoroutine(Destroy());
            lc.LoadNextLevel();
        }


        if (collision.gameObject.tag == "Projectile")
        {
            PulseToTheBeat projectilePulse=collision.gameObject.GetComponent<PulseToTheBeat>();
            projectilePulse.Pulse();
            Instantiate(explosion, transform.position,Quaternion.Euler(Vector3.zero));
            Reset();
        }

        if (collision.gameObject.tag == "ExtraHp")
        {
            deathReset = false;
            Instantiate(hpExplosion, collision.gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            hp.ExtraHp();
            am.PlayAudio(3, 0.6f);
            collision.gameObject.SetActive(false);
        }
    }

    

    private IEnumerator Destroy() //La bola se destruye al tocar el portal
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }


    public void NextLevel() //Comod�n de saltar nivel
    {
            rb.bodyType = RigidbodyType2D.Static;
            transitioning = true;
            lc.LoadNextLevel();
    }

    
    private void Update()
    {
        sr.color = Color.Lerp(sr.color, startColor, Time.deltaTime*returnSpeed);
        

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
                        touchBall= true;
                }
                if (Vector2.Distance(new Vector2(startPoint.x, startPoint.y), new Vector2(transform.position.x, transform.position.y)) < inputDistance && touchBall==true) //Comprobamos distancia entre input y bola para que no se pueda lanzar desde cualquier sitio. Esto da una sensaci�n de mayor control sobre el lanzamiento.
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
                        am.PlayAudio(1,1f);
                        rb.bodyType = RigidbodyType2D.Dynamic;
                        tr.enabled = true;
                        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                        endPoint.z = 15;
                        speed = new Vector2(endPoint.x - startPoint.x, endPoint.y - startPoint.y) / (1.2f + dragginTime); //Vector velocidad
                        float dragDistance = Vector2.Distance(new Vector2(startPoint.x, startPoint.y), new Vector2(endPoint.x, endPoint.y)); //M�dulo distancia
                        Debug.Log(dragDistance);
                        
                        rb.AddForce(speed * potencia / (Mathf.Clamp(dragDistance * 0.8f, minDragDistance, maxDragDistance)*pg.ratioCoefficient), ForceMode2D.Impulse); //Divido entre la distancia recorrida con el dedo aplicandole un factor para que cuanto mas lejos arrastres, mas fuerte lances pero limitandolo
                        Debug.Log((speed * potencia / (Mathf.Clamp(dragDistance * 0.8f, minDragDistance, maxDragDistance)*pg.ratioCoefficient), ForceMode2D.Impulse));
                        Debug.Log(Mathf.Clamp(dragDistance * 0.8f, minDragDistance, maxDragDistance));

                        tl.EndLine();
                        dragginTime = 0;
                        touchBall = false;

                        if (StaticStats.hP > 0 || StaticStats.infiniteHp == true)
                        {
                            reset = true;
                        }
                        else
                        {
                            deathReset = true;
                        }
                        
                    }
                }

               /*if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    // Handle finger movements based on TouchPhase
                    switch (touch.phase)
                    {
                        //When a touch has first been detected, change the message and record the starting position
                        case TouchPhase.Began:
                            // Record initial touch position.
                            startPoint = cam.ScreenToWorldPoint(Input.mousePosition); //Le damos los valores de posici�n dentro de la c�mara, si no tendr�amos valores altos porque contar�a los pixeles
                            startPoint.z = 15;
                            break;

                        //Determine if the touch is a moving touch
                        case TouchPhase.Moved:
                            // Determine direction by comparing the current touch position with the initial one
                            currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                            currentPoint.z = 15;
                            dragginTime += Time.deltaTime;
                            if (dragginTime <= 1.5f)
                            {
                                tl.RenderCurve();
                            }
                            break;

                        case TouchPhase.Ended:
                            am.PlayAudio(1, 1f);
                            rb.bodyType = RigidbodyType2D.Dynamic;
                            tr.enabled = true;
                            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                            endPoint.z = 15;
                            speed = new Vector2(endPoint.x - startPoint.x, endPoint.y - startPoint.y) / (1f + dragginTime); //Vector velocidad
                            float dragDistance = Vector2.Distance(new Vector2(startPoint.x, startPoint.y), new Vector2(endPoint.x, endPoint.y)); //M�dulo distancia
                            Debug.Log(dragDistance);

                            rb.AddForce(speed * potencia / (Mathf.Clamp(dragDistance * 0.8f, minDragDistance, maxDragDistance)), ForceMode2D.Impulse); //Divido entre la distancia recorrida con el dedo aplicandole un factor para que cuanto mas lejos arrastres, mas fuerte lances pero limitandolo

                            tl.EndLine();
                            dragginTime = 0;

                            //touchBool = false;
                            reset = true;
                            break;
                    }
                }*/
            }

            //RESET
            if (reset)// && StaticStats.hP>0)
            {
                // Touch touch = Input.GetTouch(0);
                if (Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.height * 0.85) //| touch.phase == TouchPhase.Began)
                {
                    Reset();
                }
            }

            if(deathReset && StaticStats.hP <= 0) //Vuelta al menu principal
            {
                if (Input.GetMouseButtonDown(0) && Input.mousePosition.y<Screen.height*0.85 ) //| touch.phase == TouchPhase.Began)
                {
                    //anim 0 vidas
                    //reset

                    Instantiate(explosion, transform.position, Quaternion.Euler(Vector3.zero));
                    lc.LoadStartMenu();
                    Destroy(gameObject);

                }
            }
            if (resetCounter == true) //Contador para que no se pueda interactuar durante un intervalo de tiempo
            {
                counter += Time.deltaTime;
                if (counter >= resetTime)
                {
                    resetCounter = false;
                   
                    reset = false;                    
                    counter = 0;
                }
            }
        }

        
    }

    private void Reset()
    {
        PhysicsReset();
        rb.bodyType = RigidbodyType2D.Static;
        tr.enabled = false;
        transform.position = startPosition;
        resetCounter = true;
        hp.RestHP();
        points.ResetPoints();
    }

    private void PhysicsReset()
    {
        bounciness.bounciness = 0.95f;
        rb.drag = startDrag;
        rb.mass = startMass;
        collisions = 0;
    }

    /*IEnumerator LerpColor(Color start, Color target, float lerpDuration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            renderer.color = Color.Lerp(start, target, curve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        renderer.color = target; //Le asignamos valor final ya que time deltaTime no es exacto y nunca nos va a dar el n�mero entero
        //SwitchTarget();
    }*/


    IEnumerator LerpEffect(float start, float target, float lerpDuration)
    {
        var emission = attractionEffect.emission; //hacemos del parametro emission una variable
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            emission.rateOverTimeMultiplier = Mathf.Lerp(start, target, curve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        emission.rateOverTime = target; //Le asignamos valor final ya que time deltaTime no es exacto y nunca nos va a dar el n�mero entero
        //SwitchTarget();
    }
}

