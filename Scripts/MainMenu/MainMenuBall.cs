using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBall : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] private Color targetColor;
    [SerializeField] private Color startColor;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float lerpDuration;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager am;
    // Start is called before the first frame update
    IEnumerator LerpColor(Color start, Color target, float lerpDuration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            spriteRenderer.color = Color.Lerp(start, target, curve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = target; //Le asignamos valor final ya que time deltaTime no es exacto y nunca nos va a dar el n�mero entero
        //SwitchTarget();

    }
    private void OnCollisionEnter2D(Collision2D collision) //Colision con las paredes
    {
        //if (collision.gameObject.tag == "Wall")
        //{
            //an.SetTrigger("Collision");
            //am.PlayAudio(0, .5f);
            spriteRenderer.color = targetColor;
            StartCoroutine(LerpColor(targetColor, startColor, lerpDuration));

        //}
    }
}
