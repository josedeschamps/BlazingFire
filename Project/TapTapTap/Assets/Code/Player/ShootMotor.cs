using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootMotor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        gm.buttonPressed = true;
     
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gm.buttonPressed = false;

    }

}
