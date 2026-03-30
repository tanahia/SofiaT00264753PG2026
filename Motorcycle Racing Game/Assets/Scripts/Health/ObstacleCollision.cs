using UnityEngine;

public class ObstacleCollision : MonoBehaviour, IHealth
{
    public int maxHealth = 3;
    public int curHealth;
    public HealthConrol health;

    public void Start()
    {
       curHealth = maxHealth;
        health.setMaxHealth(maxHealth);
    }


    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        Debug.Log("Dealing damage.");
        health.setHealth(curHealth);
        if (curHealth <= 0)
        {
            Debug.Log("Player destroyed!");
            //Destroy(gameObject);
        }
    }
  
    public void OnCollisionEnter(Collision collision)
    {
       
       if (collision.gameObject.CompareTag("Obstacle"))
        {

                Debug.Log("Collision detected");
                TakeDamage(1);
            
        }
    }


}
