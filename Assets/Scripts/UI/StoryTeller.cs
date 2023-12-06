using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryTeller : MonoBehaviour
{
    private int currentStage;
    private Canvas canvas;
    private TMP_Text storyText;
    private TMP_Text skipText;
    private float fadeDuration = 1f;
    private float pauseDuration = 1f;
    private Color newcolor;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        currentStage = GameManager.stageCount;
        storyText = canvas.GetComponentsInChildren<TMP_Text>()[0];
        skipText = canvas.GetComponentsInChildren<TMP_Text>()[1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.ToNextStage();
        }
    }

    private void Start()
    {
        storyText.color = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(ShowSkipText());
        newcolor = storyText.color;
        switch (currentStage)
        {
            case 0:
                StartCoroutine(ShowStory0());
                break;

            case 1:
                StartCoroutine(ShowStory1());
                break;

            case 2:
                StartCoroutine(ShowStory2());
                break;

            case 3:
                StartCoroutine(ShowStory3());
                break;

            default: break;
        }
    }
    private IEnumerator ShowStory0()
    {
        storyText.color = newcolor;
        StartCoroutine(ShowText("이건 스토리1"));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        StartCoroutine(ShowText("이제 검수지옥으로 넘어간다"));
        yield return new WaitForSecondsRealtime(3);
        GameManager.ToNextStage();
    }

    private IEnumerator ShowStory1()
    {
        storyText.color = newcolor;
        StartCoroutine(ShowText("이건 스토리2"));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        StartCoroutine(ShowText("이제 나태지옥으로 넘어간다"));
        yield return new WaitForSecondsRealtime(3);

        GameManager.ToNextStage();
    }

    private IEnumerator ShowStory2()
    {
        storyText.color = newcolor;
        StartCoroutine(ShowText("이건 스토리3"));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        StartCoroutine(ShowText("이제 흑암지옥으로 넘어간다"));
        yield return new WaitForSecondsRealtime(3);

        GameManager.ToNextStage();
    }

    private IEnumerator ShowStory3()
    {
        storyText.color = newcolor;
        StartCoroutine(ShowText("이건 엔딩"));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        StartCoroutine(ShowText("이제 처음 화면으로 넘어간다"));
        yield return new WaitForSecondsRealtime(3);

        GameManager.ToNextStage();
    }

    private IEnumerator ShowText(string text)
    {
        Debug.Log(text + " Start");
        storyText.text = text;
        Color originalColor = storyText.color;
        Color transparentColor = originalColor;
        transparentColor.a = 0f;

        // 서서히 텍스트 나타나게
        float counter = 0;

        while (counter < fadeDuration)
        {
            counter += 0.01f;
            float alpha = Mathf.Lerp(transparentColor.a, originalColor.a, counter / fadeDuration);
            storyText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        Debug.Log(text + "Wait");
        // 일정 시간 대기
        yield return new WaitForSecondsRealtime(pauseDuration);

        // 서서히 텍스트 드러나게
        counter = 0;

        while (counter < fadeDuration)
        {
            counter += 0.01f;
            float alpha = Mathf.Lerp(originalColor.a, transparentColor.a, counter / fadeDuration);
            storyText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        Debug.Log(text + " End");
    }

    private IEnumerator ShowSkipText()
    {
        Color originalColor = skipText.color;
        Color transparentColor = originalColor;
        transparentColor.a = 0f;

        // 서서히 텍스트 나타나게
        float counter = 0;

        while (counter < fadeDuration)
        {
            counter += 0.01f;
            float alpha = Mathf.Lerp(transparentColor.a, originalColor.a, counter / fadeDuration);
            skipText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }
}
