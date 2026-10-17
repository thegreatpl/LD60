using UnityEngine;

public class CompassScript : MonoBehaviour
{

    Vector3 dir; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Player == null)
            return;

        dir.z = GameManager.Instance.Player.transform.eulerAngles.y;
        transform.localEulerAngles = dir; 
    }
}
