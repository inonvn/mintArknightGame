using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations;

public class OBJ : MonoBehaviour
{
 public StatPlayer StatPlayer;
    public Rigidbody Rigidbody;
    public float speed;
    public bool itGround;
    public BoxCollider boxCol;
    public void gavity ()
    {
        if (Rigidbody.velocity.y == 0)
        {

            itGround = true;

        }
        else
        {
            itGround = false;
        }
    }
    public void Move()
    {
        if (itGround)
        {
            float horizol = Input.GetAxis("Horizontal");


            Rigidbody.velocity = new Vector3(horizol * speed, 0);
            
        }
        else
        {
            Physics.gravity = new Vector3(0, -20, 0);
        }
    }
    public void checkGrourd ()
    {
       bool grourd = Physics.BoxCast(boxCol.bounds.center,boxCol.bounds.size,Vector3.down);
        bool top = Physics.BoxCast(boxCol.bounds.center, boxCol.bounds.size, Vector3.up);
        print(grourd);
    }    

    private void FixedUpdate()
    {
        print(Rigidbody.velocity.y);
        checkGrourd();
        gavity();
        Move();
       
    }
}
