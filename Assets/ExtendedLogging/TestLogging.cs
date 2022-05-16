using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtendedLogging
{
    public class TestLogging : MonoBehaviour
    {
        public List<GameObject> gameObjects = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            ExtendedLogger.Log("Normal message");
            foreach (var item in gameObjects)
            {
                ExtendedLogger.Log(item, "Item found, click me");
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var item in gameObjects)
            {
                if (item.name == "Cube")
                {
                    ExtendedLogger.LogError(item, "Cubes are errors");
                }
              
                if (item.name == "Sphere")
                {
                    ExtendedLogger.LogWarning(item, "Spheres are warnings");
                }
            }
        }
    }
}