using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[Serializable]
public class Avatar
{
    public string name;
    public string url;
    public string position;

    public Texture image;
}

[Serializable]
public class Dialogue
{
    public string name;
    public string text;
}

[Serializable]
public class Root
{
    public List<Dialogue> dialogue;
    public List<Avatar> avatars;
}

public class DialogueParser : MonoBehaviour
{
    private const string url = "https://private-624120-softgamesassignment.apiary-mock.com/v3/magicwords";

    void Start()
    {
        GetJSON();
    }

    private void GetJSON()
    {
        StartCoroutine(WebRequests.GetJSON(url, (result) => ParseJSON(result)));
    }

    private void ParseJSON(string json)
    {
        Root root = JsonUtility.FromJson<Root>(json);

        foreach (Avatar avatar in root.avatars)
        {
            StartCoroutine(WebRequests.GetTexture(avatar.url, (image) => avatar.image = image));
        }

        // Pass text emoji to unicode
        foreach (Dialogue dialogue in root.dialogue)
        {
            string emoji = Regex.Match(dialogue.text,
                        @"{(.+)}",
                        RegexOptions.Singleline)
            .Groups[1].Value;

            dialogue.text = Regex.Replace(dialogue.text, @"\{.*\}", TextEmojiToUnicode(emoji));
        }

        StartCoroutine(WaitAndStartDialogue(root));
    }

    private string TextEmojiToUnicode(string code)
    {
        switch (code)
        {
            case ("intrigued"): return @"\U00002639";
            case ("satisfied"): return @"\U0001F60B";
            case ("neutral"): return @"\U0000263A";
            case ("win"): return @"\U0001F60E";
            case ("affirmative"): return @"\U0001F603";
            case ("laughing"): return @"\U0001F602";

            default: return @"\U0000263A";
        }
    }
    private IEnumerator WaitAndStartDialogue(Root root)
    {
        yield return new WaitForSeconds(1f);

        DialogueManager.Instance.SetDialogueData(root.dialogue, root.avatars);
    }
}
