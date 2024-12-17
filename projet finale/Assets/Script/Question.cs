using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public string QuestionText;  // Le texte de la question
    public List<Answer> Answers;  // Liste des réponses possibles
}
