using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PowerBallZ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int RANGELOWERVALUE = 1;
        private static int RANGEUPPERVALUE = 10;
        private static int RANGEPOWERBALLUPPERVALUE = 6;
        private int m_iTicketNumber;

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(MainWindow));

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();            
            this.DataContext = this;
        }

        private void Initialize()
        {
            Angle = 0.0;
            m_iTicketNumber = 65; // for char A
        }

        private async void GenerateTicketBtnClick(object sender, RoutedEventArgs e)
        {
            Initialize();

            int iNumTickets  = 0;

            // Text must int from IsTextAllowed
            if (NumberOfTickets.Text != null && NumberOfTickets.Text != "")
                iNumTickets = int.Parse(NumberOfTickets.Text);

            // Generate buyer's ticket(s)
            LotteryNumbers.Document.Blocks.Clear();
            List<Lottery> LotteryList = new List<Lottery>();
            for (int i = 0; i < iNumTickets; i++)
            {
                Lottery lottery = GenerateTicket();
                LotteryList.Add(lottery);
                PrintTicket(lottery);
            }

            // Generate winning ticket
            await GenerateWinningTicket();

            // Pop message
            System.Threading.Thread.Sleep(1500);
            MessageBox.Show("I hope you won something. Thank you for playing!", "Ethan says", MessageBoxButton.OK);

            Application.Current.Shutdown();
        }

        private Lottery GenerateTicket()
        {
            Lottery Ticket = new Lottery();
            Random rnd = new Random();
            
            int iCount = 0;
            while (true)
            {
                // Generate a random number
                int iGeneratedNumber = rnd.Next(RANGELOWERVALUE, RANGEUPPERVALUE);

                // If number is not in list, add it
                if (!Ticket.Numbers.Contains(iGeneratedNumber))
                {
                    Ticket.Add(iGeneratedNumber);
                    iCount++;
                }

                // Get out when we have five random different generated numbers
                if (iCount == 5)
                    break;
            }

            Ticket.PowerBallNumber = rnd.Next(RANGELOWERVALUE, RANGEPOWERBALLUPPERVALUE);

            return Ticket;
        }

        private void PrintTicket(Lottery Ticket)
        {         
            // In PowerBall, the numbers are sorted
            Ticket.Numbers.Sort();

            // Paragraph block
            Paragraph para = new Paragraph();

            // Get ticket letter
            char c = Convert.ToChar(m_iTicketNumber);
            string sTicket = c.ToString() + ".";
            para.Inlines.Add(new Run(sTicket.PadLeft(5)));

            foreach (int num in Ticket.Numbers)
            {
                para.Inlines.Add(new Run(num.ToString().PadLeft(5)));
            }
            
            para.Inlines.Add(new Run(Ticket.PowerBallNumber.ToString().PadLeft(5)) { Foreground = Brushes.Red });

            Dispatcher.Invoke(() => LotteryNumbers.Document.Blocks.Add(para), DispatcherPriority.Background);

            m_iTicketNumber++;

            System.Threading.Thread.Sleep(400);
        }

        private async Task GenerateWinningTicket()
        {
            Random rnd = new Random();
            int[] WinningTicket = new int[10] {8, 5, 2, 9, 6, 3, 10, 7, 4, 1 }; // Counter clockwise values of the wheel image
            int iValue;
            double dAngle = 0;

            for (int i = 0; i < 6; i++)
            {
                if (i != 0) System.Threading.Thread.Sleep(1000);

                Angle = rnd.Next(144, 720);
                dAngle += Angle;

                Storyboard sb = (Storyboard)this.RotateImage.FindResource("Spin");
                if (sb != null)
                    await Story.BeginAsync(sb);

                int iIndex = (int)Math.Ceiling(dAngle / 36) % 10;
                iIndex = iIndex == 0 ? 1 : iIndex; // Need to consider zero modulus
                iValue = WinningTicket[iIndex - 1];

                ShowWinningNumber(SwitchBlock(i), iValue.ToString());

            }
        }

        private TextBlock SwitchBlock(int iNum)
        {
            switch (iNum)
            {
                case 0:
                    return (TextBlock)WinningNumberPanel.FindName("FirstNumber");
                case 1:
                    return (TextBlock)WinningNumberPanel.FindName("SecondNumber");
                case 2:
                    return (TextBlock)WinningNumberPanel.FindName("ThirdNumber");
                case 3:
                    return (TextBlock)WinningNumberPanel.FindName("FourthNumber");
                case 4:
                    return (TextBlock)WinningNumberPanel.FindName("FifthNumber");
                case 5:
                    return (TextBlock)WinningNumberPanel.FindName("PowerNumber");
                default:
                    return null;
            }
        }

        private void ShowWinningNumber(TextBlock tb, string text)
        {
            // Text
            tb.Text = text;

            // Fading animation
            Storyboard sb2 = (Storyboard)this.FindResource("FadeInBlocks");
            Storyboard.SetTarget(sb2, tb);
            sb2.Begin();
            sb2.SetSpeedRatio(15);
        }

        private void NumberOfTickets_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }
    }

    public class Lottery
    {
        public List<int> Numbers;

        public Lottery()
        {
            this.Numbers = new List<int>();
            this.PowerBallNumber = 0;
        }

        public int PowerBallNumber { get; set; }
        public void Add(int num) { this.Numbers.Add(num); }
    }
    
    public static class Story
    {
        public static Task BeginAsync(this Storyboard storyboard)
        {            
            System.Threading.Tasks.TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            if (storyboard == null)
                tcs.SetException(new ArgumentNullException());
            else
            {
                EventHandler onComplete = null;
                onComplete = (s, e) =>
                {
                    storyboard.Completed -= onComplete;
                    tcs.SetResult(true);
                };
                storyboard.Completed += onComplete;
                storyboard.Begin();
                storyboard.SetSpeedRatio(85);
            }

            return tcs.Task;
        }
    }
}
