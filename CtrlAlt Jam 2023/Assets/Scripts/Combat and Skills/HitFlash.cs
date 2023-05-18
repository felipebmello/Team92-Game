using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{

    [SerializeField] private Material flashMaterial;
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float duration;
    private Material defaultMaterial;
    private Color defaultColor;
    private SpriteRenderer mySpriteRenderer;
    private Coroutine flashRoutine;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = mySpriteRenderer.material;
        defaultColor = mySpriteRenderer.color;
    }
    private IEnumerator FlashRoutine()
    {
        mySpriteRenderer.material = flashMaterial;
        mySpriteRenderer.color = flashColor;
        yield return new WaitForSeconds(duration);
        mySpriteRenderer.material = defaultMaterial;
        mySpriteRenderer.color = defaultColor;
        flashRoutine = null;
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }
}
