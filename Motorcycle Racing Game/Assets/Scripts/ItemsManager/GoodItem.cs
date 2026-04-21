using UnityEngine;

public class GoodItem : Item
{
    private HealthManager healthManager;
    private void Start()
    {
        healthManager = FindFirstObjectByType<HealthManager>();
    }
    internal void DoGoodThing()
    {
       healthManager.AddHealth(1);
    }
}
