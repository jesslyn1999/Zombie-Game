using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;

    public float templateHeight = 40f;
    public float initTemplateHeight = 15f;

    private Dictionary<string, string>[] listScores;

    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (Transform child in entryContainer)
        {
            Destroy(child.gameObject);
        }
        fillContainer();
    }

    // Update is called once per frame
    void Update()
    {
    }

    async void fillContainer()
    {
        listScores = await RestApi.GetScores();
        for (int i = 0; i < listScores.Length; i++)
        {
            string username = listScores[i]["username"];
            string score = listScores[i]["score"];
            if (username == null || score == null)
            {
                continue;
            }
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i - initTemplateHeight);
            entryTransform.gameObject.SetActive(true);
            entryTransform.Find("Username").GetComponent<TextMeshProUGUI>().text = username;
            entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = score;
        }
    }
}
