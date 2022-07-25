using System.Collections.Generic;
using UnityEngine;

public static class MyLine
{
    private static Dictionary<int, LineRenderer> lineDict = new Dictionary<int, LineRenderer>();
    private static Stack<LineRenderer> lineStack = new Stack<LineRenderer>();
    private static Material lineMaterial;
    private static Color defaultColor = Color.white;

    public static void SetColor(Color color)
    {
        defaultColor = color;
    }

    public static void SetupLine(int index, Color color, Transform parentTransform = null, bool hasGradient = true, float startWidth = -1, float endWidth = -1)
    {
        if (lineDict.ContainsKey(index))
        {
            DisposeLine(index);
        }

        lineDict[index] = MakeLine(color, parentTransform, hasGradient,startWidth, endWidth);
    }

    public static void SetLinePosition(int index, Vector3 start, Vector3 end) //add color
    {
        LineRenderer lr;
        if (!lineDict.ContainsKey(index))
        {
            lr = MakeLine();
            lineDict[index] = lr;
        }
        else
        {
            lr = lineDict[index];
        }

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    public static LineRenderer MakeLine() //Color color,
    {
        return MakeLine(defaultColor);
    }

    public static LineRenderer MakeLine(Color color, Transform parentTransform = null, bool hasGradient = true, float startWidth = -1, float endWidth = -1)
    {
        LineRenderer lr = GetLine();
        lr.transform.parent = parentTransform; //(parentTransform == null) ? defaultParentTransform :
        lr.material = GetMaterial();

        startWidth = (startWidth == -1) ? 0.05f : startWidth;
        if (hasGradient)
        {
            Color transparent = color;
            transparent.a = 0;
            lr.SetColors(color, transparent);
            endWidth = (endWidth == -1) ? startWidth * 2 : endWidth;
        }
        else
        {
            lr.SetColors(color, color);
            endWidth = (endWidth == -1) ? startWidth : endWidth;
        }

        lr.SetWidth(startWidth, endWidth);
        return lr;
    }

    public static void DisposeLine(int index)
    {
        LineRenderer lineRenderer = lineDict[index];
        lineDict.Remove(index);
        lineRenderer.gameObject.SetActive(false);
        lineStack.Push(lineRenderer);
    }


    private static LineRenderer GetLine()
    {
        if (lineStack.Count == 0)
        {
            GameObject myLine = new GameObject("line");
            myLine.AddComponent<LineRenderer>();
            return myLine.GetComponent<LineRenderer>();
        }
        else
        {
            LineRenderer lr = lineStack.Pop();
            lr.gameObject.SetActive(true);
            return lr;
        }
    }

    private static Material GetMaterial()
    {
        return (lineMaterial == null) ? new Material(Shader.Find("UI/Default")) : lineMaterial;
    }
    
}