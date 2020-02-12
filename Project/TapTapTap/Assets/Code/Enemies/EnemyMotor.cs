using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMotor : MonoBehaviour
{
    //[System.Serializable]
    //public class EnemyStats
    //{

    //    public float aliveTime = 1f;
    //    public float ShootSpeed = 1f;
    //    public bool canShoot = false;
    //    public bool hasDoubleShot = false;
    //    public float scorePoint = 1f;
    //    public float currency;
      
    //}
    //public EnemyStats stats;
    private GameManager gm;
    private Transform body;
    public float aliveTime = 1f;
    public float ShootSpeed = 1f;
    public bool canShoot = false;
    public bool hasDoubleShot = false;
    public float scorePoint = 1f;
    public float currency;







    private void Start()
    {
        DOTween.Init();
        //stats = new EnemyStats();
        gm = FindObjectOfType<GameManager>();
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
            StopAllCoroutines();
        }

    }

    public void HurtSystem()
    {
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.25f).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);

    }

}
