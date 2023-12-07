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
    private float pauseDuration = 1.5f;
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
        storyText.fontSize = 80;
        StartCoroutine(ShowText("\"여긴 어디지?\""));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        storyText.fontSize = 40;
        StartCoroutine(ShowText("나는 분명... 그래..! 평소처럼 수강생들에게..\r\n\nTIL을 제출하라고 하다가.."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 80;
        StartCoroutine(ShowText("\"그만.. 랜섬웨어에 걸려서.. 죽었던가..\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 40;
        StartCoroutine(ShowText("제기랄 아직 하고싶은데 많은데..! \r\n\n쿠키.. 밥도 줘야하고.."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 80;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"정신차려요!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"누구야!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 40;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"그걸 설명할 시간이 없어요! 매니저님! 여긴 지옥이에요! \n\n하도 수강생을 괴롭혀서 염라대왕께서 노하셨어요!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 40;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"저희가 도와 드릴 테니 \n\n어서 이곳을 탈출해야해요!\""));
        yield return new WaitForSecondsRealtime(3);

        GameManager.ToNextStage();
    }

    private IEnumerator ShowStory1()
    {
        storyText.color = newcolor;
        storyText.fontSize = 60;
        StartCoroutine(ShowText("\"허억..허억.. 겨우 해냈다! \n\n여기까지 도와주러 오다니..고마워.. 십장생들\""));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"아니에요. 매니저님. 다 원하는게 있습니다. \n\n돌아가시면 알죠? ^^*\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"염라대왕을 물리치면 환생할 수 있다고? 그게 가능해?\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"게임이잖아요. 안되는 건 없어요!\"\r\n\n\n이자식 제 4의 벽은 어따 팔아먹은거야.."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"그건 그렇고! 얼른 움직여요! \n\n중심부로 다가갈 수록 더 강력한 놈들이 나타나요! \n\n조심해요!!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 80;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"알았어!!\""));
        yield return new WaitForSecondsRealtime(3);
        GameManager.ToNextStage();
    }

    private IEnumerator ShowStory2()
    {
        storyText.color = newcolor;
        storyText.fontSize = 80;
        StartCoroutine(ShowText("\"...해치웠나?\""));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"제정신이에요?! 그 말을 하면 다시 살아난다구요! \n\n취소취소!!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"이제 마지막이에요. \n\n염라대왕을 물리쳐서 원래 세상으로 돌아가자구요!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"그래.. 난 염라대왕을 물리치면.. 그녀에게.. 고백할거야..\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"아 진짜! 그런 말 하지 말라구요!! 플래그 서잖아요!!!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.red;
        StartCoroutine(ShowText("\"무엄하다! 여기가 어디라고 오느냐!! \n\n염라의 힘을 보여주마!!!\""));
        yield return new WaitForSecondsRealtime(3);

        GameManager.ToNextStage();
    }

    private IEnumerator ShowStory3()
    {
        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("염라대왕이 쓰러지고 지옥은 무너져갔다."));
        yield return new WaitForSecondsRealtime(4);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"하아..하아.. 이제 어떻게 되는거야..? \n\n환생되는거 확실해?\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"네.. 이제 매니저님은 이승으로 돌아가실 거에요..\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"그래 고마웠어.. \n\n어? 정말 내 몸이 희미해져 가고있어\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"...\"\r\n\n\n십장생들은 말이 없었다."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("내 몸은 거의 사라져 가고 있었지만 \n\n십장생의 몸은 그대로다. 어째서..?"));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 80;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"설마.. 아니지? 너희는 ..?\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 50;
        storyText.color = Color.blue;
        StartCoroutine(ShowText("\"..저희는 힘을 쓴 대가로 여기 남게 되었어요.. \n\n하지만 미안해하지 마세요.. \n\n저흰 괜찮으니까!\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"뭐..!?\"\r\n\n내 몸은 이제 거의 투명한 상태다."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("\"행복하세요.\""));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 60;
        storyText.color = Color.white;
        StartCoroutine(ShowText("눈을 뜨니 낯선 천장\r\n\n삐삐삐 낯선 기계음이 들린다."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("십장생들... 정말..."));
        yield return new WaitForSecondsRealtime(3);

        storyText.color = newcolor;
        storyText.fontSize = 70;
        storyText.color = Color.white;
        StartCoroutine(ShowText("...알빠노"));
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

        // 서서히 텍스트 사라지게
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
