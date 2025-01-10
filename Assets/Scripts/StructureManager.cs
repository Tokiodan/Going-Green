using UnityEngine;
using UnityEngine.UI;

public class StructureManager : MonoBehaviour {
    [SerializeField] private GridManager _gridManager;

 
    [SerializeField] private Button _houseButton;
    [SerializeField] private Button _farmButton;
    [SerializeField] private Button _windmillButton;
    [SerializeField] private Button _workshopButton;

  
    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private GameObject _farmPrefab;
    [SerializeField] private GameObject _windmillPrefab;
    [SerializeField] private GameObject _workshopPrefab;

    private GameObject _currentGhostStructure;

    void Start() {
        
        _houseButton.onClick.AddListener(() => SetCurrentStructurePrefab(_housePrefab));
        _farmButton.onClick.AddListener(() => SetCurrentStructurePrefab(_farmPrefab));
        _windmillButton.onClick.AddListener(() => SetCurrentStructurePrefab(_windmillPrefab));
        _workshopButton.onClick.AddListener(() => SetCurrentStructurePrefab(_workshopPrefab));
    }

    
    public void SetCurrentStructurePrefab(GameObject structurePrefab) {
        if (_currentGhostStructure != null) {
            Destroy(_currentGhostStructure); 
        }

        _currentGhostStructure = Instantiate(structurePrefab, Vector3.zero, Quaternion.identity); 
        _currentGhostStructure.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.5f); 

    void Update() {
       
        if (_currentGhostStructure != null) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; 
            _currentGhostStructure.transform.position = new Vector3(Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.y), 0f);
        }

        
        if (_currentGhostStructure != null && Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 gridPos = new Vector2(Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.y));

            Tile tile = _gridManager.GetTileAtPosition(gridPos);
            if (tile != null && tile.CanPlaceStructure()) {
                tile.PlaceStructure(_currentGhostStructure); 
                _currentGhostStructure = null; 
            }
        }
    }
}
}