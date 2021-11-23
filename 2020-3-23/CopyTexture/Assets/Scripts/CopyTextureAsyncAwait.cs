using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CopyTextureAsyncAwait : MonoBehaviour
{
    public Material OutputMaterial;
    private Material inputMaterial;
    private RenderTexture outputRT;
    private Texture inputTexture;

    // -----------------------------------------------------------------------------------------------------
    void Start()
    {
        this.GetComponent<ImagePlayer>().OpenMedia(Application.streamingAssetsPath + "/chart-rv-1920-1080.png");
        inputMaterial = this.GetComponent<ImagePlayer>().ImageMaterial;
        inputTexture = inputMaterial.GetTexture("_MainTex");
        OutputMaterial = new Material(inputMaterial);

        outputRT = new RenderTexture(5664, 128, 32);
        Copy();

        OutputMaterial.SetTexture("_MainTex", outputRT);
        this.GetComponent<MeshRenderer>().sharedMaterial = OutputMaterial;
    }

    // -----------------------------------------------------------------------------------------------------
    void Update()
    {
        var tmp = CopyAsync();
    }


    //---------------------------------------------------------------------------------
    async Task CopyAsync()
    {
        await Task.Run(() => {
            Copy();
        });
    }

    // -----------------------------------------------------------------------------------------------------
    void Copy()
    {
        Graphics.CopyTexture(inputTexture, 0, 0, 0, inputTexture.height - 128, 1920, 128, outputRT, 0, 0, 0, 0);
        Graphics.CopyTexture(inputTexture, 0, 0, 0, inputTexture.height - 256, 1920, 128, outputRT, 0, 0, 1920, 0);
        Graphics.CopyTexture(inputTexture, 0, 0, 0, inputTexture.height - 384, 1824, 128, outputRT, 0, 0, 3840, 0);
    }
}
