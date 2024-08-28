using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] private bool resetLevel;
    [SerializeField] private Animator an;
    [SerializeField] private float transitionTime;
    [SerializeField] private TextMeshProUGUI levelValue;
    [SerializeField] private CanvasGroup cv;
    [SerializeField] private TextMeshProUGUI recordValue;

    //Transition animations
    [SerializeField] private List<Animator> animators = new List<Animator>();
    private GameObject floor;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject hpImage;
    [SerializeField] private GameObject nextButton;

    private void Awake()
    {
        floor = GameObject.FindGameObjectWithTag("suelo");
    }
    public void Start()
    {        
        if (SceneManager.GetActiveScene().buildIndex == 0 ) //Si estamos en el menu principal
        {          
            //hP.ResetHP();
            recordValue.text = StaticStats.initialRecord.ToString("");
        }
        else
        {
            /*optionsButton.transform.localScale = Vector3.zero;
            nextButton.transform.localScale = Vector3.zero;
            hpImage.transform.localScale = Vector3.zero;
            optionsButton.LeanScale(Vector3.one, transitionTime).setEase(LeanTweenType.easeOutBack);
            nextButton.LeanScale(Vector3.one, transitionTime).setEase(LeanTweenType.easeOutBack);
            hpImage.LeanScale(Vector3.one, transitionTime).setEase(LeanTweenType.easeOutBack);*/
            
            float floorStartPositionX = floor.transform.position.x;
            floor.transform.position = new Vector3(-40, floor.transform.position.y, floor.transform.position.z);
            floor.LeanMoveLocalX(floorStartPositionX, transitionTime);
            levelValue.text = SceneManager.GetActiveScene().buildIndex.ToString();
        }
    }
   
    public void LoadNextLevel()
    {
        if (resetLevel)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }
        else if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            StaticStats.record += 1;
        }
        else
        {
            LoadStartMenu();
        }
       
    }

    
    public void LoadStartMenu() //Solo se activa cuando morimos por llegar a 0 vidas o cuando reseteamos desde el menu
    {

        if (StaticStats.record > StaticStats.initialRecord && StaticStats.infiniteHp==false)
        {
            StaticStats.initialRecord = StaticStats.record;
        }
        StaticStats.record = 0;
        StaticStats.hP = StaticStats.initialHp+StaticStats.extraHp;
        StaticStats.next = StaticStats.initialNext + StaticStats.extraNext;

        StartCoroutine(LoadLevel(0));
    }
    IEnumerator LoadLevel(int levelIndex)
    {

        cv = GetComponentInParent<CanvasGroup>();
        cv.blocksRaycasts = false; //Bloqueamos raycast en el canvas mientras transicionamos
        yield return new WaitForSeconds(transitionTime);
        //Play animation
        //an.SetTrigger("Out");
        floor.LeanMoveLocalX(40, 1);
        yield return new WaitForSeconds(1);
        for (int i=0; i< animators.Count; i++)
        {
            animators[i].SetTrigger("Close");
        }
        
        /*optionsButton.LeanScale(Vector3.zero, transitionTime).setEase(LeanTweenType.easeInBack);
        nextButton.LeanScale(Vector3.zero, transitionTime).setEase(LeanTweenType.easeInBack);
        hpImage.LeanScale(Vector3.zero, transitionTime).setEase(LeanTweenType.easeInBack);*/


        //Wait
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            transitionTime += 1;
        }
        yield return new WaitForSeconds(transitionTime);

        BackgroundController bc = FindObjectOfType<BackgroundController>().GetComponent<BackgroundController>();
        bc.ChangeBackground(levelIndex);
        //an.SetTrigger("In");
        //cv.blocksRaycasts = true;

        //Load scene
        SceneManager.LoadScene(levelIndex);
    }

    public void Pause()
    {
        PruebaMovimiento pm = FindAnyObjectByType<PruebaMovimiento>();
        pm.transitioning = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PruebaMovimiento pm = FindAnyObjectByType<PruebaMovimiento>();
        pm.transitioning = false;
    }

    public void Restart() //Reiniciando desde el menu
    {
        StaticStats.hP = StaticStats.initialHp + StaticStats.extraHp;
        StaticStats.next = StaticStats.initialNext + StaticStats.extraNext;
        StartCoroutine(LoadLevel(1));
    }

    public void ResumeForResetLevel()
    {
        Time.timeScale = 1;
        GameObject ball = GameObject.FindGameObjectWithTag("Player");
        ball.SetActive(false);
    }
}
