using UnityEngine;

public class StructurePlacer : MonoBehaviour
{
    [SerializeField] private GridManager gridManager; // Reference to the GridManager
    [SerializeField] private Camera mainCamera; // Reference to the camera
    private GameObject currentStructure; // The structure currently being placed
    private bool isPlacing = false;

    // Reference to the structure prefab we are placing (set dynamically at runtime)
    private GameObject structurePrefab;

    // Start placing a structure (called when a button is clicked)
    public void StartPlacing(GameObject prefab)
    {
        structurePrefab = prefab; // Set the prefab to be placed
        isPlacing = true;

        // Instantiate the structure but keep it disabled initially
        if (structurePrefab != null)
        {
            currentStructure = Instantiate(structurePrefab);
            currentStructure.SetActive(false); // Keep it inactive initially
        }
        else
        {
            Debug.LogError("Structure prefab is not set!");
            return;
        }
    }

    private void Update()
    {
        if (isPlacing && currentStructure != null)
        {
            // Follow the mouse position, snapping to grid
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 gridPosition = new Vector3(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y), 0f);

            // Activate the structure when placing begins
            if (!currentStructure.activeSelf)
            {
                currentStructure.SetActive(true); // Show the structure while placing
            }

            // Update the structure position to follow the mouse/grid
            currentStructure.transform.position = gridPosition;

            // If left-clicked, place the structure
            if (Input.GetMouseButtonDown(0))
            {
                PlaceStructure(gridPosition);
            }
        }
    }

    private void PlaceStructure(Vector3 position)
    {
        // Instantiate the structure at the snapped position
        GameObject placedStructure = Instantiate(structurePrefab, position, Quaternion.identity);

        // Register the structure with the relevant managers (e.g., GridManager, StructureManager)
        gridManager.AddStructure(placedStructure.GetComponent<Structure>());
        StructureManager.Instance.AddStructure(placedStructure.GetComponent<Structure>());

        // Handle any post-placement logic (e.g., population for houses)
        HandlePostPlacement(placedStructure);

        // Stop placing the structure
        isPlacing = false;
        currentStructure = null; // Clear the reference
    }

    private void HandlePostPlacement(GameObject placedStructure)
    {
        // If the placed structure is a House, increase the population
        House house = placedStructure.GetComponent<House>();
        if (house != null)
        {
            ResourceManager.Instance.ChangeResource("Population", house.populationIncrease);
        }
    }
}
