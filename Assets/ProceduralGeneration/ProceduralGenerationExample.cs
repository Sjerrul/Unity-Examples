using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProceduralGenerationExample : MonoBehaviour
{
    public GameObject StarPrefab;

    private IList<StarSystem> stars;
    public int Width;
    public int Height;

    void Start()
    {
        this.stars = new List<StarSystem>();
        for (int y  = 0; y < Width; y++)
        {
            for (int x  = 0; x < Height; x++)
            {
                StarSystem starSystem = new StarSystem(x, y);
                stars.Add(starSystem);
            }
        }   

        foreach (var starSystem in this.stars)
        {
            if (!starSystem.HasStar)
            {
                continue;
            }

            var star = Instantiate(StarPrefab, new Vector2(starSystem.X, starSystem.Y), Quaternion.identity, transform);

            // Set size
            float scale = Mathf.Lerp(0.5f, 1f, starSystem.StarSize / 100f);
            star.transform.localScale = Vector3.one * scale;

            // Set color
            star.GetComponent<MeshRenderer>().material.color = starSystem.color;
        }           
    }

}
