using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject redModelPrefab;
    [SerializeField] private GameObject yellowModelPrefab;
    [SerializeField] private GameObject greenModelPrefab;
    [SerializeField] private GameObject purpleModelPrefab;
    [SerializeField] private GameObject orangeModelPrefab;
    [SerializeField] private GameObject blueModelPrefab;

    private GameObject[] modelPrefabs;

    void Start()
    {
        // Define model prefabs
        modelPrefabs = new GameObject[] { redModelPrefab, yellowModelPrefab, greenModelPrefab, purpleModelPrefab, orangeModelPrefab, blueModelPrefab };

        int activePlayers = PlayerPrefs.GetInt("ActivePlayers", 2);

        for (int i = 0; i < activePlayers; i++)
        {
            string modelName = PlayerPrefs.GetString($"Player{i + 1}Model", "DefaultModel");
            GameObject modelPrefab = GetModelByName(modelName);
            if (modelPrefab != null)
            {
                Instantiate(modelPrefab, new Vector3(i * 2, 0, 0), Quaternion.identity);
            }
        }
    }

    private GameObject GetModelByName(string modelName)
    {
        foreach (GameObject modelPrefab in modelPrefabs)
        {
            if (modelPrefab.name == modelName)
                return modelPrefab;
        }
        return null;
    }
}
