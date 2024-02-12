using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Car : MonoBehaviour
{
    #region Public Variables
    public float speed, power, TorquePower;
    public WheelJoint2D[] wheels;
    public CircleCollider2D[] circles;
    public LayerMask layer;
    public GameObject restartBtn;
    #endregion 
    //-----------------------------

    #region private Variables  
    private JointMotor2D _motor;
    bool groundOverlap = false;
    #endregion 

    #region Public methods
    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        foreach (CircleCollider2D c in circles)
        {
            if (Physics2D.OverlapCircle(c.transform.position, c.radius, layer.value))
                groundOverlap = true;
        }
        if (!groundOverlap)
            GetComponent<Rigidbody2D>().AddTorque(TorquePower * Input.GetAxis("Horizontal"));

        groundOverlap = false;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            _motor.motorSpeed = speed * -Input.GetAxis("Horizontal");
        _motor.maxMotorTorque = power;
       foreach(WheelJoint2D w in wheels)
        {
            w.motor = _motor;

        }
       if(Input.GetAxis("Horizontal") == 0)
        {
            foreach(WheelJoint2D w in wheels)
            {
                w.useMotor = false;
            }
        }
        


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ground")
        {
            restartBtn.SetActive(true);
            Time.timeScale = 0;
        }
    }
    #endregion 

    #region Private methods
    #endregion 
}
