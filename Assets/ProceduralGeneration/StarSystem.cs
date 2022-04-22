using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem 
{
    private Color[] colors = new Color[] 
    {
        Color.red, Color.yellow, Color.cyan, Color.magenta, Color.white
    };

    public bool HasStar {get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int StarSize { get; set; }

    public Color color;

    public StarSystem(int x, int y)
    {
        this.X = x;
        this.Y = y;

        int seed = StarSystem.GetSeedForCoordinate(this.X, this.Y);
        Random.InitState(seed);

        this.HasStar = Random.Range(0, 100) < 10;

        this.StarSize = Random.Range(1, 100);
        this.color = this.colors[Random.Range(0, colors.Length)];
    }

    private static int GetSeedForCoordinate(int x, int y)
    {
         return y << 16 | x;
    }

}
