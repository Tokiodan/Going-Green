using UnityEngine;

public class House : MonoBehaviour {
    public int populationIncrease = 1; // Amount of population this house provides

    // Reference to the ResourceManager (singleton)
    private void Start() {
        // Increase population when the house is placed
        ResourceManager.Instance.ChangeResource("Population", populationIncrease);
    }
}
