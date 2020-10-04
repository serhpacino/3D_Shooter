using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public float bobbingSpeed = 0.18f;
    public float bobbingHeight = 0.2f;
    public float midpoint = 1.8f;
    public bool isHeadBobbing = true;
    private float timer = 0.0f;

    void Update()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;//checking current position of our camera

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)//cheking if player pressed any buttons responsible for movement 
        {
            timer = 0.0f;//if not we are setting timer to 0
        }
        else
        {
            //if he pressed we are creating sine wave
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;//then we adding bobbingSpeed to our timer every frame 
            if (timer > Mathf.PI * 2)//if timer goes beyond value 2PI we set it back to 0 and everything start from the beginnig 
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)//if waveslice, that is our sine wave is not equal to 0 that means we are moving 
        {
            //then according to the sine wave we are calculating the cameras position change
            float translateChange = waveslice * bobbingHeight;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            if(isHeadBobbing ==true)
                cSharpConversion.y = midpoint + translateChange;
            //and we are adding this change to cameras midpoint
            //id waveslice equals to 0 that means we are not moving .
            //So our cameras position in y axis is equal to midpoint
            else if(isHeadBobbing == false)
                        cSharpConversion.x = translateChange;
        }
        else
        {
            if (isHeadBobbing == true)
                cSharpConversion.y = midpoint;
            else if(isHeadBobbing == false)
                    cSharpConversion.x = 0;
        }

        transform.localPosition = cSharpConversion;//at the end we are settig main cameras position to the new position
    }
}
