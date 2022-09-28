using System;
using System.Collections.Generic;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        SetUpGame();
    }

    private void SetUpGame()
    {
        List<string> animalEmoji = new List<string>()
        {
            "🐶","🐶",
            "🐱","🐱",
            "🐭","🐭",
            "🐰","🐰",
            "🦊","🦊",
            "🐼","🐼",
            "🐨","🐨",
            "🐯","🐯",
        };
        Random random = new Random();
        foreach (Gtk.Button button in table2.AllChildren)
        {
            int index = random.Next(animalEmoji.Count);
            string nextEmoji = animalEmoji[index];
            button.Label = nextEmoji;
            animalEmoji.RemoveAt(index);
        }
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    Button lastButtonClicked;
    bool findingMatch = false;

    protected void OnButtonClicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        if (findingMatch == false)
        {
            button.Visible = false;
            lastButtonClicked = button;
            findingMatch = true;
        }
        else if (button.Label == lastButtonClicked.Label)
        {
            button.Visible = false;
            findingMatch = false;
        }
        else
        {
            lastButtonClicked.Visible = true;
            findingMatch = false;
        }
        Console.WriteLine($"Button {button.Name} clicked!");
    }
}