using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class KeypadController : MonoBehaviour
{
    public List<int> correctPassword = new List<int>();
    private List<int> inputPasswordList = new List<int>();
    [SerializeField] private TMP_InputField codeDisplay;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private string successText;
    [Space(5f)]
    [Header("Keypad Entry Events")]
    public UnityEvent onCorrectPassword;
    public UnityEvent onIncorrectPassword;

    public bool allowMultipleActivations = false;
    private bool hasUsedCorrectCode = false;
    public bool HasUsedCorrectCode { get { return hasUsedCorrectCode;  } }

    public void UserNumberEntry(int selectedNum)
    {
        if (inputPasswordList.Count >= 4)
            return;

        inputPasswordList.Add(selectedNum);

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        codeDisplay.text = null;
        for(int i = 0; i < inputPasswordList.Count; i++)
        {
            codeDisplay.text += inputPasswordList[i];
        }
    }

    public void DeleteEntry()
    {
        if (inputPasswordList.Count <= 0)
            return;

        var listposition = inputPasswordList.Count - 1;
        inputPasswordList.RemoveAt(listposition);

        UpdateDisplay();
    }
}
