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
        var renderer = _currentGhostStructure.GetComponent<Renderer>();
        if (renderer != null) {
            renderer.material.color = new Color(1f, 1f, 1f, 0.5f);
        } else {
            Debug.LogWarning("Structure prefab heeft geen renderer");
        }
    }

    void Update() {
        if (_currentGhostStructure != null) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Vector2 gridPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            _currentGhostStructure.transform.position = new Vector3(gridPos.x, gridPos.y, 0f);

            if (Input.GetMouseButtonDown(0)) {
                Tile tile = _gridManager.GetTileAtPosition(gridPos);
                if (tile != null && tile.CanPlaceStructure()) {
                    tile.PlaceStructure(_currentGhostStructure);
                    _currentGhostStructure = null;
                } else {
                    Debug.Log("Kan hier niks Bouwen, of uit de border of er staat al iets");
                }
            }
        }
    }
}
