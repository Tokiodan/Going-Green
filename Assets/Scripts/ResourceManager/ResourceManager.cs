using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    [Header("Resource UI elementen")]
    public TextMeshProUGUI money;
    public TextMeshProUGUI food;
    public TextMeshProUGUI population;
    public TextMeshProUGUI power;
    public TextMeshProUGUI polution;

    [Header("hoeveelheid resources")]
    [SerializeField] private float totalMoney;
    [SerializeField] private float totalFood;
    [SerializeField] private float totalPopulation;
    [SerializeField] private float totalPower;
    [SerializeField] private float totalPolution;

    [Header("resource generatie")]
    [SerializeField] private int moneyGeneration;
    [SerializeField] private int foodGeneration;
    [SerializeField] private float populationGeneration;
    [SerializeField] private int powerGeneration;
    [SerializeField] private int polutionGeneration;

    [Header("Houd bij hoeveel gebouwen er zijn")]
    public List<int> cornFarms = new List<int>(); // Lijst van CornFarms die food genereren
    public List<int> houses = new List<int>(); // Lijst van CornFarms die food genereren
    public List<int> workShops = new List<int>(); // Lijst van CornFarms die food genereren
    public List<int> windMills = new List<int>(); // Lijst van CornFarms die food genereren


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cornFarms.Clear();
        houses.Clear();
        workShops.Clear();
        windMills.Clear();
        CountResourceGeneration("all");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateResources();
    }

    public void AddToList(string type)
    {
        if(type == "Food" || type == "all")
        {
            cornFarms.Add((cornFarms.Count + 1));
            CountResourceGeneration(type);
        }else if (type == "House" || type == "all")
        {
            houses.Add((houses.Count + 1));
            CountResourceGeneration(type);
        }
        else if (type == "Work" || type == "all")
        {
            workShops.Add((workShops.Count + 1));
            CountResourceGeneration(type);
        }
        else if (type == "Mill" || type == "all")
        {
            windMills.Add((windMills.Count + 1));
            CountResourceGeneration(type);
        }
        
    }

    public void CountResourceGeneration(string type) //houd bij hoeveel er gegenereerd wordt per item
    {
        if(type == "Food" || type == "all")
        {
            foodGeneration = 0;
            foreach(int farm in cornFarms)
            {
                foodGeneration += 5;
                polutionGeneration -= 1;
            }
        } else if (type == "House" || type == "all")
        {
            populationGeneration = 0;
            foreach (int house in houses)
            {
                totalPopulation += 2;
                populationGeneration += 0.2f; //food consumption staat onderaan in UpdateResources
            }
        }
        else if (type == "Work" || type == "all")
        {
            moneyGeneration = 0;
            foreach (int shop in workShops)
            {
                moneyGeneration += 3;
                totalPopulation -= 5; //power consumption staat onderaan in UpdateResources
            }
        }
        else if (type == "Mill" || type == "all")
        {
            powerGeneration = 0;
            foreach (int mill in windMills)
            {
                powerGeneration += 5;
            }
        }
    }

    public void UpdateResources()
    {
        totalFood += (foodGeneration - totalPopulation) * Time.deltaTime ;  // Afronden naar 1 decimaal
        if(totalFood < 0) totalFood = 0 ;
        food.text = (Mathf.Floor(totalFood)).ToString();

        totalMoney += moneyGeneration * Time.deltaTime;  // Afronden naar 1 decimaal
        money.text = (Mathf.Floor(totalMoney)).ToString();

        if(totalFood <= 0)
        {
            totalPopulation -= populationGeneration * Time.deltaTime;  // Afronden naar 1 decimaal
        }
        else
        {
            totalPopulation += populationGeneration * Time.deltaTime;  // Afronden naar 1 decimaal
        }
        population.text = (Mathf.Floor(totalPopulation)).ToString();


        totalPower += (powerGeneration - (workShops.Count * 3)) * Time.deltaTime;  // Afronden naar 1 decimaal
        power.text = (Mathf.Floor(totalPower)).ToString();

        totalPolution += polutionGeneration * Time.deltaTime;  // Afronden naar 1 decimaal
        polution.text = (Mathf.Floor(totalPolution)).ToString();

    }
}
