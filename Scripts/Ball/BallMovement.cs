using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //Movimiento
    public float potencia;
    private Rigidbody2D rb;

    private Camera cam;
    public Vector2 minSpeed; 
    public Vector2 maxSpeed;
    private Vector2 speed; //Vector velocidad, distancia de pulsación entre tiempo de pulsación
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 currentPoint;
    [SerializeField] private float dragginTime;
    
    //Curva
    private TryLine tl;
       

    //Reset
    [SerializeField] private bool reset;
    private Vector3 startPosition; //Posición inicial de la bola
    private bool resetCounter;
    [SerializeField] private float counter;

    //Collisions
    private Collider2D cl;
    private Animator an;
    [SerializeField] private Animator portalAn;
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        tl = GetComponent<TryLine>();
        cl = GetComponent<Collider2D>();
        an = GetComponent<Animator>();
        audioManager = FindAnyObjectByType<AudioManager>();
        cam = Camera.main;
        startPoint.z = 15;
        reset = false;
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            Debug.Log("Next Level Trigger");
            rb.bodyType = RigidbodyType2D.Static;
            an.SetTrigger("Close");
            //portalAn.SetTrigger("Close");

        }
    }
  
    
    private void Update()
    {
      if (reset == false)// && Input.touchCount>0)
      {
        //Touch touch = Input.GetTouch(0);
       
        if (Input.GetMouseButtonDown(0) /*|touch.phase==TouchPhase.Began*/ )
        {

            startPoint = cam.ScreenToWorldPoint(Input.mousePosition); //Le damos los valores de posición dentro de la cámara, si no tendríamos valores altos porque contaría los pixeles
            startPoint.z = 15;
        }
        if (Input.GetMouseButton(0)/*|touch.phase==TouchPhase.Moved|touch.phase==TouchPhase.Stationary*/)
        {
            currentPoint= cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            dragginTime += Time.deltaTime;
                if (dragginTime <= 1.5f)
                {
                    tl.RenderCurve();
                }
        }
        if (Input.GetMouseButtonUp(0)/*|touch.phase==TouchPhase.Ended | touch.phase==TouchPhase.Canceled*/)
        {
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;
            speed= new Vector2(Mathf.Clamp((endPoint.x - startPoint.x)/(1+dragginTime), minSpeed.x, maxSpeed.x), Mathf.Clamp((endPoint.y- startPoint.y)/(1+dragginTime), minSpeed.y, maxSpeed.y)); //Cálculo velocidad
            Debug.Log(endPoint.x - startPoint.x);
            Debug.Log("DragginTime"+dragginTime);
            rb.AddForce(speed*potencia,ForceMode2D.Impulse); //Fuerza impulso, multiplico velocidad por potencia que le damos en el inspector
            tl.EndLine();
            dragginTime = 0;
            reset = true;                
        }
      }

       //RESET
        if (reset == true)
        {
            
           // Touch touch = Input.GetTouch(0);
            if (Input.GetKeyDown(KeyCode.E)) //| touch.phase == TouchPhase.Began)
            {
               
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = Vector2.zero;
                transform.position = startPosition;
                resetCounter = true;
              
            }            
        }
        if (resetCounter == true)
        {
            counter += Time.deltaTime;
            if (counter >= 0.2)
            {
                resetCounter = false;
                reset = false;
                counter = 0;
                
            }

        }
    }

}

