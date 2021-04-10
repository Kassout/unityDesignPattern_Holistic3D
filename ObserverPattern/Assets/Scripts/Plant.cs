using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private PlantData info;
    SetPlantInfo spi;

    void Start()
    {
        spi = GameObject.FindWithTag("PlantInfo").GetComponent<SetPlantInfo>();
    }

    void OnMouseDown()
    {
        spi.OpenPlantPanel();
        spi.plantName.text = info.Name;
        spi.threatLevel.text = info.Threat.ToString();
        spi.plantIcon.GetComponent<RawImage>().texture = info.Icon;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && info.Threat == PlantData.THREAT.High)
        {
            PlayerController.dead = true;
        }
    }
}
