using UnityEngine;

public class Building : MonoBehaviour
{
    private ResourceManager resourceManager;
    public string type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>(); // Vind de ResourceManager
    }

    public void AddToList()
    {
        resourceManager.AddToList(type); // Registreer de boerderij in de manager
    }
}
