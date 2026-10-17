using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    public Camera Camera; 


    public PrefabManager PrefabManager;

    public TerrainGeneration TerrainGeneration;

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


        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Camera GetCurrentCamera()
    {
        return Camera;
    }
}
