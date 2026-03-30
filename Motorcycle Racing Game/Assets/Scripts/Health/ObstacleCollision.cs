using UnityEngine;

public class ObstacleCollision : MonoBehaviour, IHealth
{
    public int health = 10;
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Dealing damage.");
        if (health <= 0)
        {
            Debug.Log("Player destroyed!");
            //Destroy(gameObject);
        }
    }

public void OnCollisionEnter(Collision collision)
    {
       
      /*  if (collision.gameObject.CompareTag("Obstacle"))
        {

                Debug.Log("Collision detected");
                TakeDamage(5);
            
        }*/
    }
}
