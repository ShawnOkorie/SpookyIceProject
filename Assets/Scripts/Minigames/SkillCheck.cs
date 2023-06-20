using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Minigames
{
    public class SkillCheck : Singleton<SkillCheck>, IMinigames
    {
        public delegate void MinigameEnd(bool solved);
        public event MinigameEnd OnMinigameEnd;
        
        [SerializeField] private Canvas myCanvas;
        private Animator charAnimator;

        [Header("Skill Check")]
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private TextMeshProUGUI charTMPPrefab;
    
        private TextMeshProUGUI[] charTMPArray;
        private char[] characters;
        private int[] asciiValues;
        private Coroutine currentPlay;
        private Coroutine currentWait;
        private int counter;
        private bool invoked;
        private bool failed;
        private bool started;

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
            
            invoked = false;
            failed = false;
            started = false;
            asciiValues = new int[length];
            characters = new char[length];
            charTMPArray = new TextMeshProUGUI[length];
            gridLayoutGroup.enabled = true;

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
                    charAnimator = gridLayoutGroup.transform.GetChild(counter).gameObject.GetComponent<Animator>();
                    charAnimator.SetTrigger("Sucess");
                    
                    counter++;
                    return;
                }

                for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
                {
                    charAnimator = gridLayoutGroup.transform.GetChild(i).gameObject.GetComponent<Animator>();
                    charAnimator.SetTrigger("Fail");
                    failed = true;
                }
                currentWait = StartCoroutine(Wait(2));
                break;
            }

            if (currentWait == null && failed)
            {
                counter = 0;
                StopCoroutine(currentPlay);
                if (invoked)
                {
                    return;
                }
                OnMinigameEnd?.Invoke(false);
                invoked = true;
            }
            
            if (characters != null)
            {
                if (counter == characters.Length)
                {
                    print("Good Job");
                    HeatManager.Instance.ResetTimer();
                    if (started == false)
                    {
                        currentWait = StartCoroutine(Wait(2));
                        started = true;
                    }
                    
                    if (currentWait != null)
                    {
                        return;
                    }
                    if (invoked)
                    {
                        return;
                    }
                    OnMinigameEnd.Invoke(true);
                    invoked = true;
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
            OnMinigameEnd?.Invoke(false);
        }
        
        private IEnumerator Wait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            currentWait = null;
        }

        public void StartMinigame(int difficulty, int timeLimit)
        {
            myDifficulty = difficulty;
            myTimeLimit = timeLimit;
            
            currentPlay = StartCoroutine(Play(difficulty, timeLimit));
            
            myCanvas.gameObject.SetActive(true);
        }
        
        public void ExitCanvas()
        {
           gameObject.SetActive(false);
        }
    }
}
