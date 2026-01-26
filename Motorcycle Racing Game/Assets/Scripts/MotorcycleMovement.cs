using System;
using Unity.VisualScripting;
using UnityEngine;

public class MotorcycleMovement : MonoBehaviour
{
    Rigidbody _rb;
    Vector2 _input = Vector2.zero;

    [SerializeField] Transform steeringHandle;
    [SerializeField] Transform steeringMotorcycle;

    [SerializeField] float accelaration;
    [SerializeField] float brakeAccelaration;
    [SerializeField] float steeringAccelearation;
    [SerializeField] float angleMultiplier;
    [SerializeField] float boostMultiplier;
    [SerializeField] float boostSpeedLimit;
    [SerializeField] float firstSpeedLimit;
    [SerializeField] float resetMultiplier;

    float startHandlePosition;
    float currentY;
    float currentZ;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        startHandlePosition = steeringHandle.localEulerAngles.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        }
        Steer();
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
}
