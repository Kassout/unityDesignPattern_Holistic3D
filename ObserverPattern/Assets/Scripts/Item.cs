using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Event dropped;
    public Event pickup;
    public Image icon;
    
    // Start is called before the first frame update
    void Start()
    {
        dropped.Occured(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           pickup.Occured(gameObject);
           gameObject.GetComponent<MeshRenderer>().enabled = false;
           gameObject.GetComponent<Collider>().enabled = false;
           Destroy(gameObject, 5);
        }
    }
}
