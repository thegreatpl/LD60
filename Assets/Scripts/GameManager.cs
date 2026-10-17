using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    public Camera Camera; 


    public PrefabManager PrefabManager;

    public TerrainGeneration TerrainGeneration;


    public GameObject Player; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return; 
        }
        Instance = this;
        PrefabManager = GetComponent<PrefabManager>();
        TerrainGeneration = GetComponent<TerrainGeneration>();


        StartNewGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Camera GetCurrentCamera()
    {
        return Camera;
    }

    public void StartNewGame()
    {
        StartCoroutine(StartNewGameCo()); 
    }


    public IEnumerator StartNewGameCo()
    {
        yield return null;

        //spawn in player here. 

        yield return TerrainGeneration.GenerateChunk(0, 0, "Vault");

        StartCoroutine(TerrainGeneration.ChunkManager()); 

        yield return null;


    }

}
