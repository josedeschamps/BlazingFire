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
            float tmp;
            tmp = collision.gameObject.GetComponent<EnemyMotor>().scorePoint;
            gm._gameTime += tmp;
            Debug.Log("HasShot" + tmp);
        }
    }

    
}
