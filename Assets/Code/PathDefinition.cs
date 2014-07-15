using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathDefinition : MonoBehaviour 
{
    public Transform[] Points;

    public IEnumerator<Transform> GetPathEnumerator()
    {
        if (Points == null || Points.Length < 1)  // Da bi slijedili put put treba nam min 1 točka
            yield break;

        var direction = 1;
        var index = 0;

        while (true)
        {
            yield return Points[index];

            if (Points.Length == 1)
                continue;

            if (index <= 0)
                direction = 1;
            else if (index >= Points.Length - 1)
                direction = -1;

            index = index + direction;
        }
    }

    public void OnDrawGizmos()
    {
        if (Points == null || Points.Length < 2)  // Pregledavamo imamo li uopće dovoljno točaka za kreirati liniju,
            return;                              // ako imamo dovoljno točaka vraćamo vrijednost

        for (var i = 1; i < Points.Length; i++)     // Za sve točke počinjemo u 1, počinjemo gledati u indeks 2
        {                                          // tako da možemo gledati u prijašnju točku te se referencirati prilikom iscrtavanja na nju [i - 1]
            Gizmos.DrawLine(Points[i - 1].position, Points[i].position);
        }
    }
}
