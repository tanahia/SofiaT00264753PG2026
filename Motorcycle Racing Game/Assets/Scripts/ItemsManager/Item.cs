using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemManager theManager;
    float timeAlive, timer=0;

    internal void Iam(ItemManager itemManager)
    {
      theManager=itemManager;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       timeAlive= UnityEngine.Random.Range(2, 15);
    }

    // Update is called once per frame
    void Update()
    {
      transform.Rotate(Vector3.up, 20 * Time.deltaTime);
      timer+=Time.deltaTime;
        if(timer>=timeAlive)
        {
           IbeenCollected();
        }
    }
    void IbeenCollected()
    {
        theManager.IbeenCollected(this);
        Destroy(gameObject);
       
    }
}
