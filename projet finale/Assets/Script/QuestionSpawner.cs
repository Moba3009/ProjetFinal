using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionSpawner : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _questionUI;  // Panel contenant l'UI des questions
    [SerializeField] private TextMeshProUGUI _questionText;  // Le texte de la question
    [SerializeField] private Button[] _answerButtons;  // Les boutons pour les réponses
    [SerializeField] private TextMeshProUGUI[] _answerTexts;  // Les textes des réponses
    [SerializeField] private TextMeshProUGUI _feedbackText;  // Texte pour afficher le feedback

    [Header("Questions")]
    [SerializeField] private List<Question> _questions;  // Liste des questions

    [Header("End Game")]
    [SerializeField] private Door _door;  // Référence à la porte

    private bool _isQuestionActive = false;
    private List<int> _answeredQuestions = new List<int>();  // Liste des indices des questions répondues correctement

    public void StartQuestionnaire()
    {
        if (_isQuestionActive) return;

        _isQuestionActive = true;
        _questionUI.SetActive(true);  // Affiche l'UI

        // Choisir une question non répondue correctement
        int randomIndex = GetNextQuestionIndex();
        if (randomIndex == -1) return;  // Si toutes les questions ont été répondues correctement

        Question selectedQuestion = _questions[randomIndex];

        // Affiche la question et les réponses
        _questionText.text = selectedQuestion.QuestionText;
        for (int i = 0; i < selectedQuestion.Answers.Count; i++)
        {
            _answerTexts[i].text = selectedQuestion.Answers[i].AnswerText;
            _answerButtons[i].interactable = true;  // Active les boutons
        }
    }

    public void OnAnswerSelected(Button selectedButton)
    {
        // Désactiver tous les boutons pour éviter plusieurs clics
        foreach (Button button in _answerButtons)
        {
            button.interactable = false;
        }

        int selectedIndex = System.Array.IndexOf(_answerButtons, selectedButton);
        bool isCorrect = _questions[0].Answers[selectedIndex].IsCorrect;

        if (isCorrect)
        {
            ShowFeedback("Bonne réponse !", Color.green);
            int currentQuestionIndex = GetNextQuestionIndex();
            _answeredQuestions.Add(currentQuestionIndex);  // Marquer la question comme répondue correctement
        }
        else
        {
            ShowFeedback("Mauvaise réponse !", Color.red);
            // La question sera reposée, rien n'est ajouté à _answeredQuestions
        }

        // Mettre le jeu en pause
        Time.timeScale = 0;

        // Cache l'UI après une courte pause
        StartCoroutine(HideQuestionUI());
    }

    private void ShowFeedback(string message, Color color)
    {
        _feedbackText.text = message;  // Met à jour le texte
        _feedbackText.color = color;  // Change la couleur du texte
        _feedbackText.gameObject.SetActive(true);  // Active le feedback
    }

    private IEnumerator HideQuestionUI()
    {
        yield return new WaitForSecondsRealtime(2f);  // Utiliser WaitForSecondsRealtime pour que le temps passe même quand timeScale = 0
        _feedbackText.gameObject.SetActive(false);  // Masque le feedback
        _questionUI.SetActive(false);  // Masquer le panneau UI
        _isQuestionActive = false;

        // Vérifie si toutes les questions ont été répondues correctement
        if (_answeredQuestions.Count == _questions.Count)
        {
            _door.OpenDoor();  // Ouvre la porte
        }

        // Reprendre le jeu
        Time.timeScale = 1;
    }

    private int GetNextQuestionIndex()
    {
        List<int> unansweredQuestions = new List<int>();

        // Récupérer toutes les questions non répondues
        for (int i = 0; i < _questions.Count; i++)
        {
            if (!_answeredQuestions.Contains(i))  // Si la question n'a pas encore été répondue correctement
            {
                unansweredQuestions.Add(i);
            }
        }

        // Si toutes les questions ont été répondues, retourner -1
        if (unansweredQuestions.Count == 0) return -1;

        // Retourner un index de question non répondue
        return unansweredQuestions[Random.Range(0, unansweredQuestions.Count)];
    }
}
