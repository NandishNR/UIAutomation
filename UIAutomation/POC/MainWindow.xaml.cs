using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Windows;

namespace AutomationPOC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IWebDriver _driver = null;
        WebDriverWait _wait = null;

        public MainWindow()
        {
            InitializeComponent();
            //btnAudio.Visibility = Visibility.Hidden;
            //btnVideo.Visibility = Visibility.Hidden;
            //btnHand.Visibility = Visibility.Hidden;
            //btnChat.Visibility = Visibility.Hidden;
            //btnEnd.Visibility = Visibility.Hidden;
        }

        private void OnLaunchJioMeet(object sender, RoutedEventArgs e)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.BinaryLocation = @"C:\Users\nandish.nr\AppData\Local\Programs\JioMeet-Lite\JioMeet-Lite.exe";
            if (File.Exists(chromeOptions.BinaryLocation))
            {
                _driver = new ChromeDriver(chromeOptions);
                _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            }
            else
            {
                MessageBox.Show(string.Format("File not available in {0}", chromeOptions.BinaryLocation));
            }
        }

        private void OnCloseJioMeet(object sender, RoutedEventArgs e)
        {
            _driver.Quit();
        }

        private void OnNavigateToJoinPage(object sender, RoutedEventArgs e)
        {
            string newWindowHandle = _driver.WindowHandles[0];
            _driver.SwitchTo().Window(newWindowHandle);


            if (ClickElementByXPath("//div[@aria-label='Join a meeting']") == false)
                ClickElementById("headerJoinMeetingButton");


            _wait.Until(driver => driver.FindElement(By.Id("meetingId")).Displayed);
        }

        private void OnCopyMeetingDetails(object sender, RoutedEventArgs e)
        {
            WriteTextToElementById("meetingId", txbMeetingId.Text);


            WriteTextToElementById("pin", txbMeetingPassword.Text);


            WriteTextToElementById("name", txbName.Text);


            ClickElementByXPath("//button[normalize-space(text())='Join']");
        }

        private void OnAudio(object sender, RoutedEventArgs e)
        {
            ClickElementById("toggleMicButton");
        }

        private void OnVideo(object sender, RoutedEventArgs e)
        {
            ClickElementById("toggleCameraButton");
        }

        private void OnHandRaise(object sender, RoutedEventArgs e)
        {
            ClickElementById("toggleHandButton");
        }

        private void OnChat(object sender, RoutedEventArgs e)
        {
            ClickElementById("toggleChatButton");
        }

        private void OnEndMeeting(object sender, RoutedEventArgs e)
        {
            ClickElementById("endCallButton");
            string newWindowHandle = _driver.WindowHandles[0];
            _driver.SwitchTo().Window(newWindowHandle);
        }

        private void WriteTextToElementById(string elementId, string value)
        {
            try
            {
                IWebElement meetingIdElement = _driver.FindElement(By.Id(elementId));
                meetingIdElement.SendKeys(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickElementById(string elementId)
        {
            try
            {
                IWebElement element = _driver.FindElement(By.Id(elementId));
                element.Click();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ClickElementByXPath(string xPath)
        {
            try
            {
                IWebElement element = _driver.FindElement(By.XPath(xPath));
                element.Click();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}