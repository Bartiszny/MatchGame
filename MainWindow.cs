using System;
using System.Collections.Generic;
using Gtk;
using System.Timers;

public partial class MainWindow : Gtk.Window
{
    Timer timer = new Timer();
    int tenthsOfSecondsElapsed;
    int matchesFound;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        timer.Interval = 0.1;
        timer.Elapsed += Timer_Tick;
        timer.Enabled = true;

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
        foreach (Gtk.Button button in table4.AllChildren)
        {
            if (button.Name != "Play")
            {
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Label = nextEmoji;
                button.Visible = true;
                animalEmoji.RemoveAt(index);
            }
        }
        timer.Start();
        tenthsOfSecondsElapsed = 0;
        matchesFound = 0;
    }

    void Timer_Tick(object sender, EventArgs e)
    {
        tenthsOfSecondsElapsed++;
        var tem = ((float)tenthsOfSecondsElapsed / 10F);
        if (matchesFound == 8)
        {
            timer.Stop();
            label1.LabelProp = (tem / 100).ToString();
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
        if (button.Name != "Play")
        {
            if (findingMatch == false)
            {
                button.Visible = false;
                lastButtonClicked = button;
                findingMatch = true;
            }
            else if (button.Label == lastButtonClicked.Label)
            {
                matchesFound++;
                button.Visible = false;
                findingMatch = false;
            }
            else
            {
                lastButtonClicked.Visible = true;
                findingMatch = false;
            }
        }
    }

    protected void OnPlayClicked(object sender, EventArgs e)
    {
        if (matchesFound == 8)
        {
            SetUpGame();
        }
    }
}