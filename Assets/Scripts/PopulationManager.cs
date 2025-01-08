using UnityEngine;

public class PopulationManager : MonoBehaviour {
    [SerializeField] private ResourceManager resourceManager; // Reference to the ResourceManager

    [SerializeField] private int populationPerHouse = 1; // How much population each house adds (editable in Inspector)

    private void Start() {
        UpdatePopulationText(); // Initialize the population text when the game starts
    }

    // Call this method when a house is placed
    public void OnHousePlaced() {
        resourceManager.ChangeResource("Population", populationPerHouse); // Increase population by 1
    }

    // Update the population text (called whenever population changes)
    private void UpdatePopulationText() {
        int currentPopulation = resourceManager.GetResourceAmount("Population");
        // Update your UI with the new population value
        resourceManager.UpdateResourceText();
    }
}
