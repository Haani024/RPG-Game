using UnityEngine;

public class NPC : MonoBehaviour
{
    bool player_detection = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.F))
        {
            print("Dialouge SHall Start");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            player_detection = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        player_detection = false;
    }
}
