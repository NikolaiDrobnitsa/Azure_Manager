

using Azure_Manager.Controller;

namespace Azure_Manager
{
    public partial class Form1 : Form
    {
        private BlobStorageManager _blobStorageManager;
        public Form1()
        {
            InitializeComponent();
            _blobStorageManager = new BlobStorageManager("DefaultEndpointsProtocol=https;AccountName=blobssss;AccountKey=xUo4h88wgJq1YoVWWIU0NxY0oHaYhOwYkoOg9jvYqHW1fPQn4+CAsV5EfTxiumQcBjqNu/HARGwm+ASt4CnaKA==;EndpointSuffix=core.windows.net");
            RefreshFileList();
        }
        private void UploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _blobStorageManager.UploadFile("blobs", openFileDialog.FileName, openFileDialog.SafeFileName);
                RefreshFileList();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            
            if (listBox2.SelectedIndex >= 0)
            {
                string ?fileName = listBox2.SelectedItem.ToString();
                if (fileName != null)
                {
                    _blobStorageManager.DeleteFile("blobs", fileName);
                    RefreshFileList();
                }
                else
                {
                    MessageBox.Show("Exist file");  
                }
                
            }
            else
            {
                MessageBox.Show("Select some file");
            }
            
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
           
            if (SearchTextBox.Text != "")
            {
                string searchText = SearchTextBox.Text;
                List<string> fileList = _blobStorageManager.GetFileList("blobs");
                List<string> filteredFileList = fileList.Where(f => f.Contains(searchText)).ToList();
                listBox2.Items.Clear();
                foreach (var item in filteredFileList)
                {
                    listBox2.Items.Add(item);
                }
                //FileListTextBox.Text = string.Join(Environment.NewLine, filteredFileList);
            }
            else
            {
                MessageBox.Show("Enter value");
            }

        }

        private void FileListTextBox_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                string ?fileName = listBox2.SelectedItem.ToString();
                if (fileName != null)
                {
                    string fileContent = _blobStorageManager.GetFileContent("blobs", fileName);
                    FileContentTextBox.Text = fileContent;
                }
            }
            else
            {
                MessageBox.Show("Select some file");
            }

        }

        

        private void RefreshFileList()
        {
            listBox2.Items.Clear();
            List<string> fileList = _blobStorageManager.GetFileList("blobs");
            foreach (var item in fileList)
            {
                listBox2.Items.Add(item);
            }
            //FileListTextBox.Text = string.Join(Environment.NewLine, fileList);
            FileContentTextBox.Text = "";
        }
        

        

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                string? fileName = listBox2.SelectedItem.ToString();
                if (fileName != null)
                {
                    _blobStorageManager.DownloadAsync("blobs", fileName);
                    
                }
            }
            else
            {
                MessageBox.Show("Select some file");
            }
           
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show("File download!");
        }
    }
}