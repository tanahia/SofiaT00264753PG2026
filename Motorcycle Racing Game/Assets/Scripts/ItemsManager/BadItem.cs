using UnityEngine;

public class BadItem :Item
{
    private HealthManager healthManager;
    void Start()
    {
        healthManager = FindFirstObjectByType<HealthManager>();
    }
    internal void DoBadThing()
    {
       healthManager.TakeDamage(1);
    }
}
