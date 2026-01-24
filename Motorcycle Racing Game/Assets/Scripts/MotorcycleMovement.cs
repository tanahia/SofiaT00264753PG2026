using UnityEngine;

public class MotorcycleMovement : MonoBehaviour
{
     Rigidbody _rb;
    [SerializeField] Transform steeringHandle;
    [SerializeField] Transform steeringMotorcycle;
    Vector2 _input = Vector2.zero;
    [SerializeField] float accelaration;
    [SerializeField] float brakeAccelaration;
    [SerializeField] float steeringAccelearation;
   [SerializeField] float angleMultiplier;
    float startHandlePosition;
    float currentX;


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


        if (Input.GetKey(KeyCode.W))
        {
            Accelarate();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Brake();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Steer();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Steer();
        }
    }
    void Steer()
    {
      _rb.linearDamping = 0.5f;
       
            _rb.AddForce(_rb.transform.right * _input.x * steeringAccelearation);
          
            currentX += _input.x*angleMultiplier*Time.deltaTime;
            currentX = Mathf.Clamp(currentX, -30f, 30f);
             steeringMotorcycle.localRotation = Quaternion.Euler(0, currentX,0);
            steeringHandle.localRotation = Quaternion.Euler(startHandlePosition, currentX, 0);

    }
    void Brake()
    {
       _rb.linearDamping = 0.8f;  
    }

    void Accelarate()
    {
        _rb.linearDamping = 0.3f;
        _rb.AddForce(_rb.transform.forward * _input.y * accelaration);
    }
}
