using System.Windows.Forms;

namespace Specs2Tests
{
    public class Clipboard : IClipboard
    {
        public string GetData()
        {
            return (string) System.Windows.Forms.Clipboard.GetDataObject().GetData(DataFormats.Text, true);
        }

        public void SetData(string data)
        {
            System.Windows.Forms.Clipboard.SetText(data);
        }
    }
}