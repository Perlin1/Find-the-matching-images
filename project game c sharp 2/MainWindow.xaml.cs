using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project_game_c_sharp_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;  
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + "- play again?";
            }

        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        { 
         if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
       

        private void SetUpGame()
        {
            List<string> devicesEmoji = new List<string>()
            {
             "📺", "📺",
             "📱", "📱",
             "💻", "💻",
             "🖨️", "🖨️",
             "⌚️", "⌚️",
             "🖥", "🖥",
             "⌨️", "⌨️",
             "📸", "📸",
            };

            Random random = new Random();
            foreach(TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                if(textblock.Name != "timeTextBlock")
                {
                    textblock.Visibility = Visibility.Visible;
                    int index = random.Next(devicesEmoji.Count);
                    string nextEmoji = devicesEmoji[index];
                    textblock.Text = nextEmoji;
                    devicesEmoji.RemoveAt(index);
                }               
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlcokClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock= sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlcokClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlcokClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlcokClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}