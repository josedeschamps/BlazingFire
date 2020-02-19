using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EnemyMotor : MonoBehaviour
{
    private GameManager gm;
    private CameraShakeMotor cam;
    private Transform body;
    [Header("Enemy Setting")]
    [Tooltip("These control the enemy attribute.")]
    public float aliveTime = 1f;
    public float ShootSpeed = 1f;
    public bool canShoot = false;
    public bool hasDoubleShot = false;
    [Header("Reward Setting")]
    [Tooltip("In Game rewards setting and values")]
    public int scorePoint = 1;
    public int killScore = 1;
    public float currency;



    private void Start()
    {
        DOTween.Init();
        //stats = new EnemyStats();
        gm = FindObjectOfType<GameManager>();
        cam = FindObjectOfType<CameraShakeMotor>();
        body = GetComponent<Transform>();
        Movement();
       
    }
    private void Movement()
    {
        StartCoroutine(BeginMovement());
    }
    IEnumerator BeginMovement()
    {

        if(body==null)
        {
         
            yield return BeginMovement();
           
        }

        transform.DOMoveX(0f, 0.5f, true).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(aliveTime);
        if (!gm.buttonPressed && body != null)
        {
            transform.DOScale(new Vector3(2.8f, 3.8f, 1), 0.25f).SetEase(Ease.OutElastic);
            transform.DOMoveX(4f, 0.5f, true).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
  

    }

    public void HurtSystem()
    {
        StartCoroutine(DeathAnimation());
    }
    IEnumerator DeathAnimation()
    {
        StopCoroutine(BeginMovement());
        transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.25f).SetEase(Ease.OutElastic);
       
        cam.ShakeCamera(0.02f, 0.1f);
        yield return new WaitForSeconds(.3f);
     
        cam.ResetPosition();
        Destroy(gameObject);

        

    }

}
