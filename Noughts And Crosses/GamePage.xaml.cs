using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Noughts_And_Crosses
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class GamePage : Noughts_And_Crosses.Common.LayoutAwarePage
    {
        public GamePage()
        {
            this.InitializeComponent();
            Player1Name.Text = MainPage.GlobalVars.Player1Name;
            Player2Name.Text = MainPage.GlobalVars.Player2Name;
            Player1Avatar.Text = MainPage.GlobalVars.Player1Avatar;
            Player2Avatar.Text = MainPage.GlobalVars.Player2Avatar;
            Player1Score.Text = "0";
            Player2Score.Text = "0";
            tieScore.Text = "0";
            GlobalV.super = false;
            if (MainPage.GlobalVars.amode == "auto")
            {
                swap.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                
            }
            if (Player1Avatar.Text == "O")
            {
                GlobalV.player[0] = Player1Name.Text;
                GlobalV.player[1] = Player2Name.Text;
            }
            else
            {
                GlobalV.player[0] = Player2Name.Text;
                GlobalV.player[1] = Player1Name.Text;
            }
            GlobalV.turn=1;
            if (MainPage.GlobalVars.GameMode == "single" && GlobalV.player[0] == "Computer")
            {
                turnLabel.Text = "Computer's turn";
                CompChance();
            }
            else
                turnLabel.Text = GlobalV.player[0] + "\'s turn";
            
        }

        public static class GlobalV
        {
            public static int turn;
            public static string[] player = new string[2];
            public static bool super;
            public static int cturn;
        }

        private void CompChance()
        {
            //increase turn after move , change turn label after move, make a move, call check after move
            Button[,] arr = new Button[,] { { button0, button1, button2 }, { button3, button4, button5 }, { button6, button7, button8 } };
            Button[] corners = new Button[] { button0, button2, button6, button8 };
            Button[] left = new Button[8];
            string val, xal;
            int r = -1, c = -1,varun,counter=0;
            bool got = false;
            Random rnd = new Random();
            if (GlobalV.turn == 1 || GlobalV.turn == 2)
                GlobalV.cturn = 1;
            
            if (GlobalV.turn % 2 != 0)
            {
                val = "O";
                xal = "X";
            }
            else
            {
                val = "X";
                xal = "O";
            }

            //first turn
            if (GlobalV.turn == 1)
            {
                
                r = rnd.Next(0, 3);
                c = rnd.Next(0, 3);
                got = true;
            }

            //second turn
            if ((!got) && GlobalV.turn == 2)
            {
                if (button0.Content.ToString() == xal || button2.Content.ToString() == xal || button6.Content.ToString() == xal || button8.Content.ToString() == xal)
                {
                    r = 1;
                    c = 1;
                    got = true;
                }
                else if (button4.Content.ToString() == xal)
                {
                    r = rnd.Next(0, 4);
                    switch (r)
                    {
                        case 0:  r = 0;
                                 c = 0;
                                 got = true;
                                 break;
                        case 1:  r = 0;
                                 c = 2;
                                 got = true;
                                 break;
                        case 2:  r = 2;
                                 c = 0;
                                 got = true;
                                 break;
                        case 3:  r = 2;
                                 c = 2;
                                 got = true;
                                 break;
                    }
                }
            }

            //try to win move
            if (!got)
            {
                //row
                for (int i = 0; i < 3; i++)
                {
                    if (got)
                        break;
                    else if (((arr[i, 0].Content.Equals(val) && arr[i, 1].Content.Equals(val)) && (arr[i, 2].Content.ToString() == ".")))
                    {
                        got = true;
                        r = i;
                        c = 2;

                    }
                    else if (((arr[i, 0].Content.Equals(val) && arr[i, 2].Content.Equals(val)) && (arr[i, 1].Content.ToString() == ".")))
                    {

                        got = true;
                        r = i;
                        c = 1;

                    }
                    else if (((arr[i, 2].Content.Equals(val) && arr[i, 1].Content.Equals(val)) && (arr[i, 0].Content.ToString() == ".")))
                    {
                        got = true;
                        r = i;
                        c = 0;
                    }
                }

                //col
                for (int i = 0; i < 3; i++)
                {
                    if (got)
                        break;
                    else if (((arr[0, i].Content.Equals(val) && arr[1, i].Content.Equals(val)) && (arr[2, i].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 2;
                        c = i;
                    }
                    else if (((arr[0, i].Content.Equals(val) && arr[2, i].Content.Equals(val)) && (arr[1, i].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 1;
                        c = i;
                    }
                    else if ((arr[2, i].Content.Equals(val) && arr[1, i].Content.Equals(val)) && (arr[0, i].Content.ToString() == "."))
                    {
                        got = true;
                        r = 0;
                        c = i;
                    }
                }

                //main diagonal
                if (!got)
                {
                    if (((arr[0, 0].Content.Equals(val) && arr[1, 1].Content.Equals(val)) && (arr[2, 2].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 2;
                        c = 2;
                    }
                    else if (((arr[1, 1].Content.Equals(val) && arr[2, 2].Content.Equals(val)) && (arr[0, 0].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 0;
                        c = 0;
                    }
                    else if ((arr[2, 2].Content.Equals(val) && arr[0, 0].Content.Equals(val)) && (arr[1, 1].Content.ToString() == "."))
                    {
                        got = true;
                        r = 1;
                        c = 1;
                    }
                }

                //secondary diagonal 
                if (!got)
                {
                    if (((arr[0, 2].Content.Equals(val) && arr[1, 1].Content.Equals(val)) && (arr[2, 0].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 2;
                        c = 0;
                    }
                    else if (((arr[1, 1].Content.Equals(val) && arr[2, 0].Content.Equals(val)) && (arr[0, 2].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 0;
                        c = 2;
                    }
                    else if ((arr[2, 0].Content.Equals(val) && arr[0, 2].Content.Equals(val)) && (arr[1, 1].Content.ToString() == "."))
                    {
                        got = true;
                        r = 1;
                        c = 1;
                    }
                }
            }

            //try to stop from winning move
            if (!got)
            {
                //row
                for (int i = 0; i < 3; i++)
                {
                    if (got)
                        break;
                    else if (((arr[i, 0].Content.Equals(xal) && arr[i, 1].Content.Equals(xal)) && (arr[i, 2].Content.ToString() == ".")))
                    {
                        got = true;
                        r = i;
                        c = 2;

                    }
                    else if (((arr[i, 0].Content.Equals(xal) && arr[i, 2].Content.Equals(xal)) && (arr[i, 1].Content.ToString() == ".")))
                    {
                        got = true;
                        r = i;
                        c = 1;
                    }
                    else if (((arr[i, 2].Content.Equals(xal) && arr[i, 1].Content.Equals(xal)) && (arr[i, 0].Content.ToString() == ".")))
                    {
                        got = true;
                        r = i;
                        c = 0;
                    }
                }

                //col
                for (int i = 0; i < 3; i++)
                {
                    if (got)
                        break;
                    else if (((arr[0, i].Content.Equals(xal) && arr[1, i].Content.Equals(xal)) && (arr[2, i].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 2;
                        c = i;
                    }
                    else if (((arr[0, i].Content.Equals(xal) && arr[2, i].Content.Equals(xal)) && (arr[1, i].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 1;
                        c = i;
                    }
                    else if ((arr[2, i].Content.Equals(xal) && arr[1, i].Content.Equals(xal)) && (arr[0, i].Content.ToString() == "."))
                    {
                        got = true;
                        r = 0;
                        c = i;
                    }
                }
                //main diagonal
                if (!got)
                {
                    if (((arr[0, 0].Content.Equals(xal) && arr[1, 1].Content.Equals(xal)) && (arr[2, 2].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 2;
                        c = 2;
                    }
                    else if (((arr[1, 1].Content.Equals(xal) && arr[2, 2].Content.Equals(xal)) && (arr[0, 0].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 0;
                        c = 0;
                    }
                    else if ((arr[2, 2].Content.Equals(xal) && arr[0, 0].Content.Equals(xal)) && (arr[1, 1].Content.ToString() == "."))
                    {
                        got = true;
                        r = 1;
                        c = 1;
                    }
                }

                //secondary diagonal 
                if (!got)
                {
                    if (((arr[0, 2].Content.Equals(xal) && arr[1, 1].Content.Equals(xal)) && (arr[2, 0].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 2;
                        c = 0;
                    }
                    else if (((arr[1, 1].Content.Equals(xal) && arr[2, 0].Content.Equals(xal)) && (arr[0, 2].Content.ToString() == ".")))
                    {
                        got = true;
                        r = 0;
                        c = 2;
                    }
                    else if ((arr[2, 0].Content.Equals(xal) && arr[0, 2].Content.Equals(xal)) && (arr[1, 1].Content.ToString() == "."))
                    {
                        got = true;
                        r = 1;
                        c = 1;
                    }
                }
            }

            //4th turn
            varun = rnd.Next(0, 2);
            if ((!got) && GlobalV.turn == 4 && GlobalV.cturn == 2 && varun == 1)
            {
                if ((button0.Content.ToString() == xal && button8.Content.ToString() == xal && button4.Content.ToString() == val))
                {
                    varun = rnd.Next(0, 4);
                    switch (varun)
                    {
                        case 0: r = 0;
                            c = 1;
                            got = true;
                            break;
                        case 1: r = 0;
                            c = 2;
                            got = true;
                            break;
                        case 2: r = 1;
                            c = 0;
                            got = true;
                            break;
                        case 3: r = 2;
                            c = 0;
                            got = true;
                            break;
                        
                    }
                }
                else if ((button4.Content.ToString() == val && button2.Content.ToString() == xal && button6.Content.ToString() == xal))
                {
                    varun = rnd.Next(0, 4);
                    switch (varun)
                    {
                        case 0: r = 0;
                            c = 0;
                            got = true;
                            break;
                        case 1: r = 0;
                            c = 1;
                            got = true;
                            break;
                        case 2: r = 1;
                            c = 0;
                            got = true;
                            break;
                        case 3: r = 2;
                            c = 2;
                            got = true;
                            break;
                        
                    }
                }
            }
            //3rd turn
            if ((!got) && GlobalV.turn == 3 && GlobalV.cturn == 2 && varun == 1)
            {
                if (button0.Content.ToString() == xal || button2.Content.ToString() == xal || button6.Content.ToString() == xal || button8.Content.ToString() == xal)
                {
                    if (!(button0.Content.ToString() == val || button2.Content.ToString() == val || button6.Content.ToString() == val || button8.Content.ToString() == val))
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (corners[z].Content.ToString() == xal)
                            {
                                switch (z)
                                {
                                    case 0: r = 2;
                                            c = 2;
                                            got = true;
                                            break;
                                    case 1: r = 2;
                                            c = 0;
                                            got = true;
                                            break;
                                    case 2: r = 0;
                                            c = 2;
                                            got = true;
                                            break;
                                    case 3: r = 0;
                                            c = 0;
                                            got = true;
                                            break;
                                }
                            }
                        }                    
                    }
                }
            }
            
            //last hatiyaar
            if (!got)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (arr[i, j].Content.ToString() == ".")
                        {
                            left[counter] = arr[i, j];
                            counter++;
                        }     
                    }
                }
                varun = rnd.Next(0,counter);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (arr[i, j] == left[varun])
                        {
                            r = i;
                            c = j;
                            got = true;
                            break;
                        }
                    }
                }
                
            }
            
            GlobalV.turn++;
            GlobalV.cturn++;
           //await Task.Delay(TimeSpan.FromSeconds(0.6));      //for a pause before comp plays
            if (val.Equals("O"))
                turnLabel.Text = GlobalV.player[1]+"\'s turn";
            else
                turnLabel.Text = GlobalV.player[0]+"\'s turn";
            arr[r, c].Content = val;
            if (check() == 1)
            {
                turnLabel.Text = GlobalV.player[0] + " wins";
                if(Player1Avatar.Text=="O")
                    Player1Score.Text = (Convert.ToInt32(Player1Score.Text) + 1).ToString();
                else
                    Player2Score.Text = (Convert.ToInt32(Player2Score.Text) + 1).ToString();
                GlobalV.super = true;
            }
            else if (check() == 2)
            {
                turnLabel.Text = GlobalV.player[1] + " wins";
                if (Player1Avatar.Text == "O")
                    Player2Score.Text = (Convert.ToInt32(Player2Score.Text) + 1).ToString();
                else
                    Player1Score.Text = (Convert.ToInt32(Player1Score.Text) + 1).ToString();
                GlobalV.super = true;
            }
            else if (GlobalV.turn > 9)
            {
                turnLabel.Text = "Its a tie";
                tieScore.Text = (Convert.ToInt32(tieScore.Text) + 1).ToString();
                GlobalV.super = true;
            }
        }


        

        static private int[,] Winners = new int[,]          //used in check function only
        {
            {0,1,2},
            {3,4,5},
            {6,7,8},
            {0,3,6},
            {1,4,7},
            {2,5,8},
            {0,4,8},
            {2,4,6}
        };


        int check()                 //returns 1 if player 0 wins, 2 if player X wins, else 0 ; no input
        {
            Button[] brr = new Button[9] {button0,button1,button2,button3,button4,button5,button6,button7,button8};
            int gameOver = 0;
            for (int i = 0; i < 8; i++)
            {
                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];
                Button b1 = brr[a], b2 = brr[b], b3 = brr[c];
                if (b1.Content.ToString() == "." || b2.Content.ToString() == "." || b3.Content.ToString() == ".")
                    continue;
                if (b1.Content.ToString() == b2.Content.ToString() && b2.Content.ToString() == b3.Content.ToString())
                {
                    b1.Foreground = b2.Foreground = b3.Foreground = new SolidColorBrush(Colors.Green);
                    if (b1.Content.ToString() == "O")
                        gameOver = 1;
                    else
                        gameOver = 2;
                    
                    break;
                }
            }
            return gameOver;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void cell_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button pressed = (Button)sender;
            if (GlobalV.super)
            {
                MessageDialog end = new MessageDialog("This game is already over.", "Noughts And Crosses");
                await end.ShowAsync();
            }

            else
            {
                //if (MainPage.GlobalVars.GameMode == "single" && GlobalV.player[0] != "Computer" && GlobalV.turn % 2 == 0) //player cant click in comp's chance
                //{
                //}

                //else if (MainPage.GlobalVars.GameMode == "single" && GlobalV.player[0] == "Computer" && GlobalV.turn % 2 != 0)    //player cant click in comp's chance
                //{
                //}
                if(pressed.Content.ToString() == "O" || pressed.Content.ToString() == "X")
                {
                    MessageDialog occupied = new MessageDialog("This cell is already occupied.", "Noughts And Crosses");
                    await occupied.ShowAsync();
                }
                else
                {
                    if (GlobalV.turn % 2 != 0)
                        pressed.Content = "O";
                    else
                        pressed.Content = "X";
                    GlobalV.turn++;
                    if (check() == 1)
                    {
                        turnLabel.Text = GlobalV.player[0] + " wins";
                        if (Player1Avatar.Text == "O")
                            Player1Score.Text = (Convert.ToInt32(Player1Score.Text) + 1).ToString();
                        else
                            Player2Score.Text = (Convert.ToInt32(Player2Score.Text) + 1).ToString();
                        GlobalV.super = true;
                    }
                    else if (check() == 2)
                    {
                        turnLabel.Text = GlobalV.player[1] + " wins";
                        if (Player1Avatar.Text == "O")
                            Player2Score.Text = (Convert.ToInt32(Player2Score.Text) + 1).ToString();
                        else
                            Player1Score.Text = (Convert.ToInt32(Player1Score.Text) + 1).ToString();
                        GlobalV.super = true;
                    }
                    else if (GlobalV.turn > 9)
                    {
                        turnLabel.Text = "Its a tie";
                        tieScore.Text = (Convert.ToInt32(tieScore.Text) + 1).ToString();
                        GlobalV.super = true;
                    }
                    else
                    {
                        if (MainPage.GlobalVars.GameMode == "single")
                        {
                            turnLabel.Text = "Computer's turn";
                            CompChance();

                        }
                        else
                        {
                            if (GlobalV.turn % 2 != 0)
                                turnLabel.Text = GlobalV.player[0] + "\'s turn";
                            else
                                turnLabel.Text = GlobalV.player[1] + "\'s turn";
                        }
                    }
                }
            }
        }
        
        private async void end_Tapped(object sender, TappedRoutedEventArgs e)
        {

                MessageDialog winner = new MessageDialog("", "Noughts And Crosses");
                if (Convert.ToInt32(Player1Score.Text) > Convert.ToInt32(Player2Score.Text))
                {
                    winner.Content = Player1Name.Text.ToString() + " wins by " + Player1Score.Text.ToString() + " - " + Player2Score.Text.ToString();
                    
                }
                else if (Convert.ToInt32(Player1Score.Text) < Convert.ToInt32(Player2Score.Text))
                {
                    winner.Content = Player2Name.Text.ToString() + " wins by " + Player2Score.Text.ToString() + " - " + Player1Score.Text.ToString();
                    
                }
                else
                {
                    winner.Content = "Scores level at " + Player1Score.Text.ToString() + " - " + Player2Score.Text.ToString();
                    
                    
                }
                winner.Commands.Add(new UICommand("Oops! Continue", uiCommandInvokedHandler =>
                {
                    //do nothing
                }));
                winner.Commands.Add(new UICommand("New Game", uiCommandInvokedHandler =>
                {
                    Player1Score.Text = "0";
                    Player2Score.Text = "0";
                    tieScore.Text = "0";
                    button0.Content = ".";
                    button1.Content = ".";
                    button2.Content = ".";
                    button3.Content = ".";
                    button4.Content = ".";
                    button5.Content = ".";
                    button6.Content = ".";
                    button7.Content = ".";
                    button8.Content = ".";
                    button0.Foreground = button1.Foreground = button2.Foreground = button3.Foreground = button4.Foreground = button5.Foreground = button6.Foreground = button7.Foreground = button8.Foreground = new SolidColorBrush(Colors.White);
                    GlobalV.super = false;
                    GlobalV.turn = 1;
                    if (GlobalV.player[0] == "Computer")
                    {
                        turnLabel.Text = "Computer's turn";
                        CompChance();
                    }
                    else
                        turnLabel.Text = GlobalV.player[0] + "\'s turn";
                }));

                winner.Commands.Add(new UICommand("Main Menu", uiCommandInvokedHandler =>
                {
                    this.Frame.Navigate(typeof(MainPage));
                    //var rootFrame = (Frame)Window.Current.Content;
                    //rootFrame.Navigate(typeof(MainPage));
                }));
                await winner.ShowAsync();
                
        }

        private async void once_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //add dialog if user wants to refresh in between a game
            if (check() == 1 || check() == 2 || GlobalV.turn > 9)
            {
                button0.Content = ".";
                button1.Content = ".";
                button2.Content = ".";
                button3.Content = ".";
                button4.Content = ".";
                button5.Content = ".";
                button6.Content = ".";
                button7.Content = ".";
                button8.Content = ".";
                button0.Foreground = button1.Foreground = button2.Foreground = button3.Foreground = button4.Foreground = button5.Foreground = button6.Foreground = button7.Foreground = button8.Foreground = new SolidColorBrush(Colors.White);
                GlobalV.super = false;
                GlobalV.turn = 1;
                if(MainPage.GlobalVars.amode=="auto")
                    swapped();
                if (GlobalV.player[0] == "Computer")
                {
                    turnLabel.Text = "Computer's turn";
                    CompChance();
                }
                else
                    turnLabel.Text = GlobalV.player[0] + "\'s turn";
            }
            else
            {
                MessageDialog sure = new MessageDialog("The game is not yet over. Are you sure you want to start new game.", "Noughts And Crosses");
                sure.Commands.Add(new UICommand("Yes", uiCommandInvokedHandler =>
                {
                    button0.Content = ".";
                    button1.Content = ".";
                    button2.Content = ".";
                    button3.Content = ".";
                    button4.Content = ".";
                    button5.Content = ".";
                    button6.Content = ".";
                    button7.Content = ".";
                    button8.Content = ".";
                    button0.Foreground = button1.Foreground = button2.Foreground = button3.Foreground = button4.Foreground = button5.Foreground = button6.Foreground = button7.Foreground = button8.Foreground = new SolidColorBrush(Colors.White);
                    GlobalV.super = false;
                    GlobalV.turn = 1;
                    if (MainPage.GlobalVars.amode == "auto")
                        swapped();
                    if (GlobalV.player[0] == "Computer")
                    {
                        turnLabel.Text = "Computer's turn";
                        CompChance();
                    }
                    else
                        turnLabel.Text = GlobalV.player[0] + "\'s turn";
                }));

                sure.Commands.Add(new UICommand("No", uiCommandInvokedHandler =>
                {
                    //do nothing
                }));
                await sure.ShowAsync();
            }
        }

        private void swap_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainPage.GlobalVars.Player1Avatar = Player2Avatar.Text;
            MainPage.GlobalVars.Player2Avatar = Player1Avatar.Text;
            Player1Avatar.Text = MainPage.GlobalVars.Player1Avatar;
            Player2Avatar.Text = MainPage.GlobalVars.Player2Avatar;
            if (Player1Avatar.Text == "O")
            {
                GlobalV.player[0] = Player1Name.Text;
                GlobalV.player[1] = Player2Name.Text;
            }
            else
            {
                GlobalV.player[0] = Player2Name.Text;
                GlobalV.player[1] = Player1Name.Text;
            }
            button0.Content = ".";
            button1.Content = ".";
            button2.Content = ".";
            button3.Content = ".";
            button4.Content = ".";
            button5.Content = ".";
            button6.Content = ".";
            button7.Content = ".";
            button8.Content = ".";
            button0.Foreground = button1.Foreground = button2.Foreground = button3.Foreground = button4.Foreground = button5.Foreground = button6.Foreground = button7.Foreground = button8.Foreground = new SolidColorBrush(Colors.White);
            GlobalV.super = false;
            GlobalV.turn = 1;
            if (MainPage.GlobalVars.GameMode == "single" && GlobalV.player[0] == "Computer")
            {
                turnLabel.Text = "Computer's turn";
                CompChance();
            }
            else
                turnLabel.Text = GlobalV.player[0] + "\'s turn";
        }

        private void swapped()
        {
            MainPage.GlobalVars.Player1Avatar = Player2Avatar.Text;
            MainPage.GlobalVars.Player2Avatar = Player1Avatar.Text;
            Player1Avatar.Text = MainPage.GlobalVars.Player1Avatar;
            Player2Avatar.Text = MainPage.GlobalVars.Player2Avatar;
            if (Player1Avatar.Text == "O")
            {
                GlobalV.player[0] = Player1Name.Text;
                GlobalV.player[1] = Player2Name.Text;
            }
            else
            {
                GlobalV.player[0] = Player2Name.Text;
                GlobalV.player[1] = Player1Name.Text;
            }
           
        }

        private async void home_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TapAnimation.Stop();
            TapAnimation.SetValue(Storyboard.TargetNameProperty, "home");
            TapAnimation.Begin();
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            this.Frame.Navigate(typeof(MainPage));
        }

       

        

       
    }
}
