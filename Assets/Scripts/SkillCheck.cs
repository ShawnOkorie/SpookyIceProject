using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillCheck : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI charText;

    private char[] characters;
    private int[] asciiValues;
    private bool isSolved;
    private Coroutine currentPlay;

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void StartSkillCheck() //int difficulty, int timeLimit
    {
        currentPlay = StartCoroutine(Play(4, 0));
        StartCoroutine(Timer(4)); //int timeLimit
        
        inputField.onSubmit.AddListener(CompareStrings);
    }
    
    private IEnumerator Play(int difficulty, int timeLimit)
    {
        isSolved = false;
        print(isSolved);
        asciiValues = new int[difficulty];
        characters = new char[difficulty];
        inputField.characterLimit = difficulty;
        inputField.text = null;
        charText.text = null;

        for (int i = 0; i < difficulty; i++)
            asciiValues[i] = Random.Range(97,123);
        
        for (int i = 0; i < asciiValues.Length; i++)
            characters[i] = Convert.ToChar(asciiValues[i]);
        
        for (int i = 0; i < characters.Length; i++)
            charText.text += $" {characters[i]}";
        
        
        yield return null;
    }
    

    private void CompareStrings(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            char[] inputChar;
            inputChar = new char[inputField.characterLimit];
            inputChar = input.ToCharArray();
            
            if (inputChar[i] != characters[i])
            {
                StopCoroutine(currentPlay);
                currentPlay = StartCoroutine(Play(4, 0));
                return;
            }
        }
        isSolved = true;
        
        print("Good Job");
        
        StopCoroutine(currentPlay);
        currentPlay = StartCoroutine(Play(4, 0));
    }
    
    private IEnumerator Timer(int timeLimit)
    {
        yield return new WaitForSeconds(timeLimit);
        yield return false;
    }
}
