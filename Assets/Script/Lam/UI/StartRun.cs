using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRun : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeInTime = 1.0f;
    public float fadeOutTime = 1.0f;
    public float maxScale = 2.0f;
    public float scaleSpeed = 1.0f;

    private void Awake()
    {
        Gamemanager.OnStartGame += startFade;
    }


    public void startFade()
    {
        gameObject.GetComponent<Renderer>().enabled = true;    
        StartCoroutine(fade());
    }

    private IEnumerator fade()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Fade in
        yield return FadeIn();

        // Scale up
        yield return ScaleUp();

        // Fade out
        yield return FadeOut();

        // Destroy the GameObject after fading out
        gameObject.SetActive(false);
    }



    private IEnumerator FadeIn()
    {
        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, originalColor.a, elapsedTime / fadeInTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        spriteRenderer.color = originalColor;
    }

    private IEnumerator ScaleUp()
    {
        Vector3 originalScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < scaleSpeed)
        {
            elapsedTime += Time.deltaTime;
            float scale = Mathf.Lerp(1f, maxScale, elapsedTime / scaleSpeed);
            transform.localScale = originalScale * scale;
            yield return null;
        }

        transform.localScale = originalScale * maxScale;
    }

    private IEnumerator FadeOut()
    {
        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeOutTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
