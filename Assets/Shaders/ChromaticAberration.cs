using UnityEngine;

public class ChromaticAberration : MonoBehaviour
{
    [SerializeField]
    private Material _material;

    [Range(0.0f, 1.0f), SerializeField]
    private float _size = 0.025f;

    private void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        if (_material == null)
        {
            Graphics.Blit(source, dest);
            return;
        }
        _material.SetFloat("_Size", _size);
        Graphics.Blit(source, dest, _material);
    }
}