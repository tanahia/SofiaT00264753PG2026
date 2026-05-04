using UnityEngine;

public class TestHealth : MonoBehaviour
{
   [SerializeField] HealthManager healthManager;
    internal int damageValue=1;
    internal int healValue=1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            healthManager.TakeDamage(damageValue);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            healthManager.AddHealth(healValue);
        }
    }
}
