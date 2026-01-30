using UnityEngine;
using UnityEngine.Windows;

public class CameraScript : MonoBehaviour
{
    float mouseX;
    private float currentY;
   [ SerializeField] float mouseSensetivity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = UnityEngine.Input.GetAxis("Mouse X");
        currentY += mouseX * mouseSensetivity * Time.deltaTime;
        currentY = Mathf.Clamp(currentY, -30f, 30f);
        transform.localRotation = Quaternion.Euler(0, currentY, 0);
    }
}
