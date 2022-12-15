using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class MeshUtils
{
    public static GameObject CreatePolygonShape(Vector3[] vertices)
    {
        ProBuilderMesh mesh = ProBuilderMesh.Create();

        mesh.CreateShapeFromPolygon(vertices, 0f, false);

        mesh.ToMesh();
        mesh.Refresh();

        return mesh.gameObject;
    }
}
