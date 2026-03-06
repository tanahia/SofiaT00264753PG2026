using UnityEngine;
using UnityEngine.Windows;

public class CameraScript : MonoBehaviour
{
    float mouseX;
    private float currentY;
   [ SerializeField] float mouseSensetivity;
    MotorcycleMovement player;
    MotorcycleMovement.State currentState;


    void Start()
    {
        player = FindFirstObjectByType<MotorcycleMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = UnityEngine.Input.GetAxis("Mouse X");
       
       cameraRotation();
    }
    public void cameraRotation()
    {
        currentState=player.getCurrentState();
        if(currentState == MotorcycleMovement.State.UserControled )
        {
            currentY += mouseX * mouseSensetivity * Time.deltaTime;
            currentY = Mathf.Clamp(currentY, -30f, 30f);
            transform.localRotation = Quaternion.Euler(0, currentY, 0);
        }
        
       
    }
}
