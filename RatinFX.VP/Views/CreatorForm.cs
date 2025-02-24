using RatinFX.VP.General;
using RatinFX.VP.Helpers;
using System.Windows.Forms;

namespace RatinFX.VP.Views
{
    public partial class CreatorForm : Form
    {
        public CreatorForm()
        {
            InitializeComponent();

            Text = $"RatinFX information {Parameters.CurrentVersion}";
        }

        private void linkTwitch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Processes.OpenUrl("https://www.twitch.tv/RatinFX");
        }

        private void linkYTMain_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Processes.OpenUrl("https://www.youtube.com/@RatinFX?sub_confirmation=1");
        }

        private void linkYT2nd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Processes.OpenUrl("https://www.youtube.com/@RatinFX2?sub_confirmation=1");
        }

        private void linkTwitter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Processes.OpenUrl("https://twitter.com/RatinFX");
        }

        private void linkInstagram_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Processes.OpenUrl("https://instagram.com/RatnFX");
        }

        private void linkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Processes.OpenUrl("https://github.com/RatinFX");
        }

        private void linkWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Processes.OpenUrl("https://ratinfx.com");
        }

        private void linkKofiDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Processes.OpenUrl("https://ko-fi.com/RatinFX");
        }

        private void linkStreamlabsDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Processes.OpenUrl("https://streamlabs.com/RatinFX/tip");
        }
    }
}