﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics2
{
    internal class CubeMeshCreator
    {
        public static Mesh Create(Cube cube)
        {
            int verticesPerSide = cube.VerticesPerSide;
            int sidesPerCube = 6;
            int componentsPerVert = 3;
            float[] vertices = new float[sidesPerCube * verticesPerSide * componentsPerVert];

            int quadsPerSide = (int)(cube.Splitting * cube.Splitting);
            int indicesPerQuad = 6;
            int[] indices = new int[indicesPerQuad * quadsPerSide * sidesPerCube];

            var cubeVerticesArraySize = cube.GetSide(0).vertices.GetLength(0);

            // vertices
            int currSide = 0;
            int currVert = 0;
            for (int side = 0; side < 6; side++)
            {
                for (int i = 0; i < cubeVerticesArraySize; i++)
                {
                    for (int j = 0; j < cubeVerticesArraySize; j++)
                    {
                        vertices[currVert++] = cube.GetSide(currSide).vertices[i, j].X;
                        vertices[currVert++] = cube.GetSide(currSide).vertices[i, j].Y;
                        vertices[currVert++] = cube.GetSide(currSide).vertices[i, j].Z;
                    }
                }
                currSide++;
            }

            // indices
            int currIndex = 0;
            int index = 0;
            int nextVertIndex = cube.VerticesLines;
            for (int side = 0; side < 6; side++)
            {
                for (int i = 0; i < cube.Splitting; i++)
                {
                    for (int j = 0; j < cube.Splitting; j++)
                    {
                        //upper triangle
                        indices[currIndex++] = index;
                        indices[currIndex++] = index + 1;
                        indices[currIndex++] = index + nextVertIndex;

                        //lower triangle
                        indices[currIndex++] = (index + nextVertIndex);
                        indices[currIndex++] = (index + nextVertIndex) + 1;
                        indices[currIndex++] = index + 1;
                        index++;
                    }
                    index++;
                }
                index += cube.VerticesLines;
            }
            return new Mesh(vertices, indices);
        }
    }
}
