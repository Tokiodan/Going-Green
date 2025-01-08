using UnityEngine;
using System.Collections.Generic;
using TMPro; // Import TMP_Text

public class ResourceManager : MonoBehaviour {
    public static ResourceManager Instance { get; private set; }

    [System.Serializable]
    public class Resource {
        public string name;
        public int amount;
    }

    [SerializeField] private List<Resource> resources; // List of all resources
    [SerializeField] private TMP_Text resourceText; // Reference to the TMP_Text element for displaying resources

    private void Awake() {
        // Singleton pattern to ensure one instance of ResourceManager
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        UpdateResourceText();
    }

    // Add or subtract from a specific resource
    public void ChangeResource(string resourceName, int amount) {
        Resource resource = resources.Find(r => r.name == resourceName);
        if (resource != null) {
            resource.amount += amount;
            UpdateResourceText();
        } else {
            Debug.LogWarning($"Resource {resourceName} not found!");
        }
    }

    // Specific method for adding food to the resources
    public void AddFood(int amount) {
        ChangeResource("Food", amount); // Assuming you have a "Food" resource
    }

    // Update the UI to display current resources
    public void UpdateResourceText() {
        string resourceDisplay = "Resources:\n";
        foreach (var resource in resources) {
            resourceDisplay += $"{resource.name}: {resource.amount}\n";
        }

        resourceText.text = resourceDisplay; // Update TMP_Text with the new resource display
    }

    // Get the amount of a specific resource
    public int GetResourceAmount(string resourceName) {
        Resource resource = resources.Find(r => r.name == resourceName);
        return resource != null ? resource.amount : 0;
    }
}
