using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour
{
    private GameManager gm;
    public float clickRate = 0.5f;
    private float nextClick;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && gm.buttonPressed==true && Time.time > nextClick)
        {
            nextClick = Time.time + clickRate;
            collision.gameObject.GetComponent<EnemyMotor>().HurtSystem();
            int tmp;
            tmp = collision.gameObject.GetComponent<EnemyMotor>().scorePoint;
            gm._gameTime += tmp;
            gm._score += tmp;
            gm.ScoreDisplay();
            gm.totalKillScore += tmp;
            Debug.Log("HasShot" + tmp);
        }


        if (collision.gameObject.CompareTag("Civilian") && gm.buttonPressed == true && Time.time > nextClick)
        {
            nextClick = Time.time + clickRate;
            collision.gameObject.GetComponent<CivilianMotor>().HurtSystem();
            int tmp;
            tmp = collision.gameObject.GetComponent<CivilianMotor>().scoreReduce;
            gm._gameTime -= tmp;
            gm._score -= tmp;
            gm.ScoreDisplay();
            Debug.Log("HasShot" + tmp);
        }







    }

    
}
