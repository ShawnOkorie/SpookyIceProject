using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Minigames
{
    public class SkillCheck : Singleton<SkillCheck>, IMinigames, IShouldForceAwake
    {
        public delegate void MinigameFail();
        public event MinigameFail OnMinigameEnd;
        
        [SerializeField] private Canvas myCanvas;
        
        [Header("Skill Check")]
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private TextMeshProUGUI charTMPPrefab;
    
        private TextMeshProUGUI[] charTMPArray;
        private char[] characters;
        private int[] asciiValues;
        private Coroutine currentPlay;
        private int counter;

        [SerializeField] private int myDifficulty;
        [SerializeField] private int myTimeLimit;
        
        [Header("Timer")]
        [SerializeField] private TextMeshProUGUI timertext;
        int seconds, milliseconds;
        

        /*public void StartMinigame() //int difficulty, int timeLimit
        {
            myCanvas.gameObject.SetActive(true);
            currentPlay = StartCoroutine(Play(difficulty));
        }*/
    
        private IEnumerator Play(int length, int timeLimit)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Timer(timeLimit));
        
            asciiValues = new int[length];
            characters = new char[length];
            charTMPArray = new TextMeshProUGUI[length];

            if (gridLayoutGroup.transform.childCount != 0)
            {
                for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
                    Destroy(gridLayoutGroup.transform.GetChild(i).gameObject);
            }
        
            for (int i = 0; i < length; i++)
                asciiValues[i] = Random.Range(97,123);
        
            for (int i = 0; i < asciiValues.Length; i++)
                characters[i] = Convert.ToChar(asciiValues[i]);

            for (int i = 0; i < characters.Length; i++)
            {
                charTMPArray[i] = Instantiate(charTMPPrefab,gridLayoutGroup.transform);

                charTMPArray[i].text = characters[i].ToString();
            }
        
            yield return null;
        }
        private void Update()
        {
            foreach (char c in Input.inputString)
            {
                if (c == characters[counter])
                {
                    //animation true

                    counter++;
                    return;
                }

                //animation false

                counter = 0;
                
                StopCoroutine(currentPlay);
                OnMinigameEnd?.Invoke();
                break;
            }

            if (characters != null)
            {
                if (counter == characters.Length)
                {
                    print("Good Job");
                    counter = 0;
                }
            }
        }

        private IEnumerator Timer(float timeLimit)
        {
            while (timeLimit >= 0)
            {
                timeLimit -= Time.deltaTime;

                timertext.text = MathF.Round(timeLimit,1).ToString("0.0");

                yield return new WaitForEndOfFrame();
            }
            print("Fail");
            OnMinigameEnd?.Invoke();
        }

        public void StartMinigame(int difficulty, int timeLimit)
        {
            myCanvas.gameObject.SetActive(true);

            myDifficulty = difficulty;
            myTimeLimit = timeLimit;
            
            currentPlay = StartCoroutine(Play(difficulty, timeLimit));
        }
        
        public void ExitCanvas()
        {
            myCanvas.sortingOrder = 9;
        }
    }
}
