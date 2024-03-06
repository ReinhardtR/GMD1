using System.Collections.Generic;
using System.Linq;
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

    void Awake()
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
                rock.SetActive(false);

                Mineable mineable = rock.GetComponent<Mineable>();

                float roll = Random.Range(0.0f, 1.0f);
                float total = 0.0f;
                foreach (RockType rockType in RockType.GetAll())
                {
                    total += rockType.SpawnChance;
                    if (roll <= total)
                    {
                        mineable.RockType = rockType;
                        break;
                    }
                }

                rock.transform.parent = terrain.transform;
                rock.SetActive(true);
            }
        }
    }
}
