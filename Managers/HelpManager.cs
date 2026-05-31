using lab1.Forms;
using TextEditor.Forms;

namespace lab1.Managers
{
    public class HelpManager
    {
        public void ShowHelp()
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        public void ShowAbout()
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }
    }
}