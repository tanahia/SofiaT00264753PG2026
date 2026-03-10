using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MotorcycleMovement : MonoBehaviour
{
    Rigidbody _rb;
    Vector2 _input = Vector2.zero;

    [SerializeField] Transform steeringHandle;
    [SerializeField] Transform steeringMotorcycle;

    [SerializeField] float accelaration=50f;
    [SerializeField] float brakeAccelaration;
    [SerializeField] float steeringAccelearation;
    [SerializeField] float angleMultiplier;
    [SerializeField] float boostMultiplier;
    [SerializeField] float boostSpeedLimit;
    [SerializeField] float firstSpeedLimit;
    [SerializeField] float resetMultiplier;
    [SerializeField] GameObject[] wheels;

    float startHandlePosition;
    float currentY;
    float currentZ;
    float currentX;

    bool choiceMaking = false;

    public enum State
    {
        Tutorial,
        UserControled,
        IntersectionChoice
    }

    State currentState = State.Tutorial;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        startHandlePosition = steeringHandle.localEulerAngles.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (currentState)
        {
            case State.Tutorial:
                Cursor.lockState = CursorLockMode.None;
                _rb.linearVelocity = Vector3.zero;
                break;
            case State.UserControled:
                Cursor.lockState = CursorLockMode.Locked;
                _input.x = Input.GetAxis("Horizontal");
                _input.y = Input.GetAxis("Vertical");

                bool isBoosting = Input.GetKey(KeyCode.LeftShift);
                bool isBraking = Input.GetKey(KeyCode.LeftControl);

                float currentSpeedLimit = isBoosting ? boostSpeedLimit : firstSpeedLimit;
                float currentAcceleration = isBoosting ? accelaration * boostMultiplier : accelaration;
                if (isBraking)
                {
                    Brake();
                }
                else
                {
                        if (_rb.linearVelocity.magnitude <= currentSpeedLimit)
                            Accelarate(currentAcceleration);
                        else
                            _rb.linearVelocity = _rb.linearVelocity.normalized * currentSpeedLimit;
                        Steer();
                        // print("stop");                   
                }
                break;
            case State.IntersectionChoice:
                Cursor.lockState = CursorLockMode.None;
                _rb.linearVelocity = Vector3.zero;
                print("enter");
                break;
        }

        
    }

    private void Brake()
    {
        if (_rb.linearVelocity.magnitude > 0.1f)
        {
            Vector3 brakeDirection = -_rb.linearVelocity.normalized;
            _rb.AddForce(brakeDirection * brakeAccelaration, ForceMode.Acceleration);

        }
    }

    void Steer()
    {

        _rb.AddForce(_rb.transform.right * _input.x * steeringAccelearation);

        currentY += _input.x*angleMultiplier* Time.fixedDeltaTime;
        currentZ+= -_input.x*angleMultiplier* Time.fixedDeltaTime;
        currentY = Mathf.Clamp(currentY, -30f, 30f);
        currentZ = Mathf.Clamp(currentZ, -5f, 5f);

        if (_input.x == 0)
        {
            ResetRotation();
           
        }
        else {
            steeringMotorcycle.localRotation = Quaternion.Euler(0, 0, currentZ);
            steeringHandle.localRotation = Quaternion.Euler(startHandlePosition, currentY, 0);

          //  print(_input.x);
        }
       
    }

    void Accelarate(float accelaration)
    {
        _rb.AddForce(_rb.transform.forward * accelaration);
        currentX += accelaration * Time.fixedDeltaTime;
        foreach (var wheel in wheels)
        {
            wheel.transform.localRotation = Quaternion.Euler(currentX, 0, 0);
        }

        //  print(_rb.linearVelocity.magnitude);
    }
    void ResetRotation()
    {
        Quaternion targetMotorRot = Quaternion.Euler(0f, 0f, 0f);
        Quaternion targetHandleRot = Quaternion.Euler(startHandlePosition, 0f, 0f);

        steeringMotorcycle.localRotation = Quaternion.Lerp(steeringMotorcycle.localRotation, targetMotorRot, Time.fixedDeltaTime * resetMultiplier);
        steeringHandle.localRotation = Quaternion.Lerp(steeringHandle.localRotation, targetHandleRot, Time.fixedDeltaTime * resetMultiplier);

        currentY = Mathf.Lerp(currentY, 0f, resetMultiplier * Time.fixedDeltaTime);
        currentZ = Mathf.Lerp(currentZ, 0f, resetMultiplier * Time.fixedDeltaTime);
    }
   public void OnTriggerEnter(Collider other)
    {
       currentState = State.IntersectionChoice;
       
    }


    public void GoToState(State newState)
    {
        currentState= newState;

    }
    public State getCurrentState()
    {
        return currentState;
    }
}
