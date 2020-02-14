using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{

    //Tap and Swipe Controls---//
    private bool tap;
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch;
    private Vector2 swipeDelta;


    //----Getting Inputs--//
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool Tap { get { return tap; } }



    void Update()
    {



        tap = swipeLeft = swipeRight = swipeDown = false;

        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {

            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {

            isDraging = false;
            ResetInputs();
        }



        #endregion
        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {

            if (Input.touches[0].phase == TouchPhase.Began)
            {

                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }

            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {

                isDraging = false;
                ResetInputs();
            }

        }

        #endregion


        //Checking for Distance
        #region Checking Input Distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {

            if (Input.touches.Length > 0)

                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        #endregion

        //Hiting deadZone
        #region Has Reach DeadZone
        if (swipeDelta.magnitude > 125)
        {

            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {

                if (x < 0)
                    swipeLeft = true;

                else

                    swipeRight = true;
            }
            else
            {
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            ResetInputs();


        }


        #endregion



    }




    private void ResetInputs()
    {

        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}

