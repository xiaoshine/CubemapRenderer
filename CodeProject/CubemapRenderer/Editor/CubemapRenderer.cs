/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CubemapRenderer.cs
 *  Description  :  Render a scene into a static Cubemap asset.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/7/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.CubemapRenderer
{
    public class CubemapRenderer : ScriptableWizard
    {
        #region Field and Property
        [Tooltip("Source camera to render Cubemap.")]
        public Camera camera;

        [Tooltip("Width and height of a cube face in pixels.")]
        public int size = 128;

        [Tooltip("Pixel data format to be used for the Cubemap.")]
        public TextureFormat format = TextureFormat.ARGB32;

        [Tooltip("Should mipmaps be created?")]
        public bool mipmap = false;
        #endregion

        #region Private Method
        [MenuItem("Tool/Cubemap Renderer &C")]
        private static void ShowEditor()
        {
            DisplayWizard("Cubemap Renderer", typeof(CubemapRenderer), "Render");
        }

        private void OnEnable()
        {
            camera = Camera.main;
        }

        private void OnWizardUpdate()
        {
            if (camera && size > 0)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
        }

        private void OnWizardCreate()
        {
            var newCubemapPath = EditorUtility.SaveFilePanelInProject(
                "Save New Render Cubemap",
                "NewRenderCubemap",
                "cubemap",
                "Enter a file name to save the new render cubemap.");

            if (newCubemapPath == string.Empty)
            {
                return;
            }

            var newRenderCubemap = new Cubemap(size, format, mipmap);
            camera.RenderToCubemap(newRenderCubemap);

            AssetDatabase.CreateAsset(newRenderCubemap, newCubemapPath);
            AssetDatabase.Refresh();
            Selection.activeObject = newRenderCubemap;
        }
        #endregion
    }
}