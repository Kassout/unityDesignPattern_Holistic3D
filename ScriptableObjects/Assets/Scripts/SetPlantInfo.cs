using UnityEngine;
using UnityEngine.UI;

public class SetPlantInfo : MonoBehaviour
{
    public GameObject plantInfoPanel;
    public GameObject plantIcon;
    public Text planeName;
    public Text threatLevel;

    public void OpenPlantPanel()
    {
        plantInfoPanel.SetActive(true);
    }

    public void ClosePlantPanel()
    {
        plantInfoPanel.SetActive(false);
    }
}
