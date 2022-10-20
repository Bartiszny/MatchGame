using System;
using System.Collections.Generic;
using Gtk;
using System.Windows.Threading;
using System.Timers;

public partial class MainWindow : Gtk.Window
{

    //DispatcherTimer timer = new DispatcherTimer();
    Timer timer = new Timer();
    int tenthsOfSecondsElapsed;
    int matchesFound;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {

        timer.Interval = 0.1;
        timer.Elapsed += Timer_Tick;
        timer.Enabled = true;

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
        //label1.LabelProp = ((float)tenthsOfSecondsElapsed / 10F).ToString();
        var tem = ((float)tenthsOfSecondsElapsed / 10F);
        //Console.WriteLine("Time:" + tenthsOfSecondsElapsed);
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
            Console.WriteLine($"Button {button.Name} clicked!");
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