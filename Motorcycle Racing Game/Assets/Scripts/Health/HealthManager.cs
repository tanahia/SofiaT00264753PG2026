using UnityEngine;

public class HealthManager : MonoBehaviour, IHealth
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
    
   public void AddHealth(int health)
    {
        curHealth += health;
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        this.health.setHealth(curHealth);
    }


}
