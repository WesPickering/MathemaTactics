using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public int score;
    public int[] operators_owned;
    public int[] points_system = { 1, 1, 1, 1 };
    private int answer;
    //[SerializeField]
    //[Tooltip("thus is a field")]
    private int operator_used;
    public int combo;
    public GameObject timer_text;
    public GameObject timer_radius;
    public GameObject equation_text;
    public GameObject points_text;
    public InputField input;
    public GameObject correct_image;
    public GameObject wrong_image;
    public bool timer_done;
    public GameObject canvas;

    public GameObject combat;
    public CombatManager combatScript;

    // Use this for initialization
    void Start () {
        timer_done = false;
        score = 0;
        combo = 0;
        operators_owned = new int[4];
        operators_owned[0] = 0;
        operators_owned[1] = 1;
        operators_owned[2] = 2;
        operators_owned[3] = 3;
        functionGenerator();
        StartCoroutine(TimerCountdown());
        input = GameObject.Find("InputField").GetComponent<InputField>();
        points_text.GetComponent<UnityEngine.UI.Text>().text = score.ToString();

        combatScript = combat.GetComponent<CombatManager>();

        //turn off X and checkmark for now
        wrong_image.SetActive(false);
        correct_image.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            functionGenerator();
        }
        if (timer_done == true) {
            if (score == 0) {
                combatScript.points = score;
            }
            combatScript.total_accrued_points = score;

            canvas.SetActive(false);
        }
	}

    public void Reset() {
        timer_done = false;
        score = 0;
        combo = 0;
        functionGenerator();
        StartCoroutine(TimerCountdown());
        points_text.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
    }

    private IEnumerator TimerCountdown() {
        float totalTime = 15f;
        float timer = totalTime;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            int remainingTime = Mathf.RoundToInt(timer);
            timer_text.GetComponent<UnityEngine.UI.Text>().text = remainingTime.ToString();
            timer_radius.GetComponent<Image>().fillAmount = timer / totalTime;
            yield return null;
        }
        timer_done = true;
    }

    private void functionGenerator() {
        int var_one = Random.Range(0, 20);
        int var_two = Random.Range(0, 20);
        int correct_answer = -1;
        string equation = " ";
        int operator_index = Random.Range(0, operators_owned.Length);
        operator_used = operators_owned[operator_index];
        switch (operators_owned[operator_index]) {
            case 0:
                correct_answer = var_one + var_two;
                equation = var_one.ToString() + " + " + var_two.ToString();
                break;

            case 1:
                if (var_one < var_two) {
                    int temp = var_one;
                    var_one = var_two;
                    var_two = temp;
                }
                correct_answer = var_one - var_two;
                equation = var_one.ToString() + " - " + var_two.ToString();
                break;

            case 2:
                var_one = Random.Range(0, 12);
                var_two = Random.Range(0, 12);
                correct_answer = var_one * var_two;
                equation = var_one.ToString() + " x " + var_two.ToString();
                break;

            case 3:
                correct_answer = Random.Range(0, 12);
                var_two = Random.Range(0, 12);
                while (var_two == 0)
                {
                    var_two = Random.Range(0, 12);
                }
                var_one = var_two * correct_answer;
                equation = var_one.ToString() + " / " + var_two.ToString();
                break;
        }
        equation_text.GetComponent<UnityEngine.UI.Text>().text = equation;
        answer = correct_answer;
        Debug.Log(var_one + " "+ var_two + " "+ operator_used +" " +correct_answer);

    }

    public void onAnswer(string user_answer) {
        Debug.Log("user answered: " + user_answer);
        if (user_answer == answer.ToString()) {
            score += points_system[operator_used] + (int)(combo/4);
            combo++;
            Debug.Log("Correct");
            StartCoroutine(feedback(true));
        } else {
            combo = 0;
            Debug.Log("Incorrect");
            StartCoroutine(feedback(false));
        }
        points_text.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
        combatScript.setPoints(score);
        input.text = "";
        functionGenerator();
    }

    public void numpadClick(string button_number) {
        if (button_number == "SKIP") {
            onAnswer("-1");
        } else if (button_number == "DEL") {
            if (input.text.Length > 0) {
                input.text = input.text.Substring(0, input.text.Length - 1);
            }
        } else if (button_number == "ENT") {
            if (input.text != "") {
                onAnswer(input.text);
            }
        } else {
            input.text += button_number;
        }
    }

    private IEnumerator feedback(bool isCorrect) {
        if (isCorrect)
        {
            //Display checkmark for t seconds
            correct_image.SetActive(true);
            yield return new WaitForSeconds(.75f);
            correct_image.SetActive(false);

        } else
        {
            //Display red x for t seconds
            wrong_image.SetActive(true);
            yield return new WaitForSeconds(.75f);
            wrong_image.SetActive(false);
        }
    }

}
