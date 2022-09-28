using System;
using System.Collections.Generic;
using Gtk;
using System.Windows.Threading;

public partial class MainWindow : Gtk.Window
{

    DispatcherTimer timer = new DispatcherTimer();
    int tenthsOfSecondsElapsed;
    int matchesFound;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        timer.IsEnabled = true;

        timer.Interval = TimeSpan.FromSeconds(.1);

        timer.Tick += Timer_Tick;
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
            if (button.Name != "button18")
            {
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Label = nextEmoji;
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
        button18.Label = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
        Console.WriteLine(tenthsOfSecondsElapsed);
        if (matchesFound == 8)
        {
            timer.Stop();
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
        if (button.Name != "button18")
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
            Console.WriteLine($"Button {button.Name} clicked!");
        }
    }

    protected void OnButton18Clicked(object sender, EventArgs e)
    {
        if (matchesFound == 8)
        {
            SetUpGame();
        }
    }
}