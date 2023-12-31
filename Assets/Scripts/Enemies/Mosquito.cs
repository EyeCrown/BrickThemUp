using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    #region MOVEMENTS
    protected override void Idle()
    {
        if(idleSpeed == 0 || idleRange == 0)
        {
            return;
        }
        if(Mathf.Abs(idleDistance) > idleRange)
        {
            idleSpeed = -idleSpeed;
            
        }
        float distance = idleSpeed * Time.deltaTime;
        idleDistance += distance;
        transform.position = new Vector3(transform.position.x + distance, transform.position.y);
    }
    #endregion
}
