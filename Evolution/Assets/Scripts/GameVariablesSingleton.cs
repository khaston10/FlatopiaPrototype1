using UnityEngine;

public class GameVariablesSingleton : MonoBehaviour
{
    public static GameVariablesSingleton Instance;

    public int width;
    public int foodCount;
    public int plantEaterCount;

    void Awake()
    {
        // If the instance reference has not been set, yet, 
        if (Instance == null)
        {
            // Set this instance as the instance reference.
            Instance = this;
        }
        else if (Instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }

}
