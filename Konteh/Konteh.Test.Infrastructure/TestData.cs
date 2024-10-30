using Konteh.Domain;
using Konteh.Domain.Enumerations;

namespace Konteh.Test.Infrastructure;

public static class TestData
{
    public static List<Question> GetAllQuestions()
    {
        return new List<Question>
        {
            new CheckBoxQuestion {
                Text = "What is an interface?",
                Category = QuestionCategory.General,
                Answers =
                [
                    new() { Text = "A class that can have multiple methods", IsCorrect = true},
                    new() { Text = "A reference type used to define a contract for classes"},
                    new() { Text = "A class with no constructors"},
                    new() { Text = "A static class" }
                ],
            },
            new CheckBoxQuestion {
                Text = "What is abstraction?",
                Category = QuestionCategory.General,
                Answers =
                [
                    new() { Text = "Hiding complexity and showing only the essential features of an object", IsCorrect = true},
                    new() { Text = "Providing access to all details of an object"},
                    new() { Text = "Combining multiple classes into one"},
                ]
            },
            new CheckBoxQuestion {
                Text = "What is inheritance?",
                Category = QuestionCategory.OOP,
                Answers =
                [
                    new() { Text = "The ability of a class to inherit features from another class", IsCorrect = true},
                    new() { Text = "The ability to encapsulate data" },
                    new() { Text = "The ability to create objects" }
                ]
            },
            new CheckBoxQuestion {
                Text = "What is the purpose of a constructor?",
                Category = QuestionCategory.General,
                Answers =
                [
                    new() { Text = "To initialize the state of an object", IsCorrect = true},
                    new() { Text = "To define methods for the class"},
                    new() { Text = "To inherit properties from another class" }
                ]
            },
            new CheckBoxQuestion {
                Text = "What is encapsulation?",
                Category = QuestionCategory.OOP,
                Answers =
                [
                    new() { Text = "Hiding internal details of an object", IsCorrect = true},
                    new() { Text = "Allowing objects to inherit from other objects"},
                    new() { Text = "Separating object concerns" }
                ]
            },
        };
    }
}