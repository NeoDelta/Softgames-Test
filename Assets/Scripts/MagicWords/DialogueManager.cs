using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private RawImage leftAvatar;
    [SerializeField] private RawImage rightAvatar;
    [SerializeField] private TextMeshProUGUI nameDisplay;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private Texture defaultAvatar;

    private List<Dialogue> dialogues;
    private List<Avatar> avatars;

    int curentDialogueId = 0;

    public void SetDialogueData(List<Dialogue> dialogues, List<Avatar> avatars)
    {
        this.avatars = avatars;
        this.dialogues = dialogues;

        InitializeDialogue();
    }

    private void InitializeDialogue()
    {
        curentDialogueId = 0;

        DisplayDialogue();
    }

    private void DisplayDialogue()
    {
        if (curentDialogueId >= dialogues.Count) curentDialogueId = 0;

        Dialogue dialogue = dialogues[curentDialogueId];

        nameDisplay.text = dialogue.name;
        textDisplay.text = dialogue.text;

        List<Avatar> avatarFound = avatars.FindAll((avatar) => avatar.name == dialogue.name);
        Avatar avatar = null;

        foreach (Avatar av in avatarFound)
        {
            if (av.image != null) avatar = av;
        }

        if (avatar == null)
            SetAvatar("left", defaultAvatar);
        else
            SetAvatar(avatar.position, avatar.image);
        
    }

    private void SetAvatar(string position, Texture image)
    {
        leftAvatar.gameObject.SetActive(position == "left");
        leftAvatar.texture = image;

        rightAvatar.gameObject.SetActive(position == "right");
        rightAvatar.texture = image;
    }

    public void NextLine()
    {
        if (dialogues.Count <= 0) return;

        curentDialogueId++;

        DisplayDialogue();
    }
}
