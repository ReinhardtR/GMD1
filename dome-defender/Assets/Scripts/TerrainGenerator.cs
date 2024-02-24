using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject terrain;
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private float width = 10.0f;
    [SerializeField]
    private float height = 10.0f;

    void Start()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        // starting at y = 0 going down, centered at x = 0
        // each rock is 1 unit wide and 1 unit tall
        for (float y = 0; y > -height; y--)
        {
            for (float x = -width / 2; x < width / 2; x++)
            {
                Vector3 position = new(x, y, 0);
                GameObject rock = Instantiate(rockPrefab, position, Quaternion.identity);
                rock.transform.parent = terrain.transform;
            }
        }
    }
}
