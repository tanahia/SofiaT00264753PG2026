using UnityEngine;

public class GoodItem : Item
{
    private HealthManager healthManager;
    private int rotationMultiplier=20;
    private int healthToAdd=1;
    private void Start()
    {
        healthManager = FindFirstObjectByType<HealthManager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up, rotationMultiplier * Time.deltaTime);
    }
    internal void DoGoodThing()
    {
       healthManager.AddHealth(healthToAdd);
    }
}
