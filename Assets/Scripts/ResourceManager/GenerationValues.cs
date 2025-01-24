using UnityEngine;
using TMPro;
using System.Linq;

public class GenerationValues : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI food;
    public TextMeshProUGUI population;
    public TextMeshProUGUI power;
    public TextMeshProUGUI polution;

    public ResourceManager manager;

    private void Update()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        money.text = (manager.workShops.Count * 3).ToString() + "P/s";
        food.text = (Mathf.Floor(manager.cornFarms.Count * 5 - manager.totalPopulation) + 1).ToString() + "P/s";
        population.text = (manager.houses.Count * 0.2f).ToString() + "P/s";
        power.text = (manager.windMills.Count * 5 - manager.workShops.Count * 3).ToString() + "P/s";
        polution.text = (manager.cornFarms.Count * -1).ToString() + "P/s";
    }
}
