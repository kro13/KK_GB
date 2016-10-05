using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Points : MonoBehaviour
{

    public Text text;
    private int lifetime = 0;

    private static List<Points> pointsList = new List<Points>();

    public void SetValue(int val)
    {
        if (val >= 0)
        {
            text.color = GameColor.Green;
        }
        else
        {
            text.color = GameColor.Red;
        }
        text.text = val.ToString();
        pointsList.Add(this);
    }

    public void FixedUpdate()
    {
        if (lifetime < 100)
        {
            transform.position += new Vector3(0, 0.1f, 0);
            text.transform.localScale +=new Vector3(0.005f, 0.005f, 0);
            lifetime++;
        }
        else
        {
            Destroy(this.gameObject);
            pointsList.Remove(this);
        }
    }

    public static void DestroyAll()
    {
        foreach (Points p in pointsList)
        {
            Destroy(p.gameObject);
        }
        pointsList.Clear();
    }
}
