using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Util
{
    public class FileChooser
    {
        public string OpenFileChooser(string directory = "c:\\", string filter = "")
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = directory;

                if (!filter.Equals(""))
                {
                    openFileDialog.Filter = filter;
                }
                
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    return openFileDialog.FileName;
                }

                return "";
            }
        }    
    }
}
