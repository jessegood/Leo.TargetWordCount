namespace Leo.TargetWordCount
{
    using System.Windows.Forms;

    public class FileDialog
    {
        public string GetFile(string lastUsedDirectory)
        {
            string file = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml files (*.xml)|*.xml";
            ofd.InitialDirectory = lastUsedDirectory;
            ofd.Multiselect = false;

            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (ofd.FileName != null)
                {
                    file = ofd.FileName;
                }
            }

            return file;
        }

        public string SaveFile(string lastUsedDirectory)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "xml files (*.xml)|*.xml";
            sfd.InitialDirectory = lastUsedDirectory;
            sfd.DefaultExt = "xml";

            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                return sfd.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}