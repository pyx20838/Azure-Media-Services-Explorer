﻿//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//---------------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class ImportHttp : Form
    {
        private bool _AzureStorageContainerSASListMode;

        public Uri GetURL
        {
            get
            {
                return new Uri(textBoxURL.Text);
            }

            set
            {
                textBoxURL.Text = value.ToString();
            }
        }

        public string GetAssetFileName
        {
            get
            {
                return textBoxAssetFileName.Text;
            }

        }

        public string GetAssetName
        {
            get
            {
                return textBoxAssetName.Text;
            }
        }


        public ImportHttp(bool AzureStorageContainerSASListMode = false)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            _AzureStorageContainerSASListMode = AzureStorageContainerSASListMode;
        }

        private void ImportHttp_Load(object sender, EventArgs e)
        {
            labelURLFileNameWarning.Text = string.Empty;

            if (_AzureStorageContainerSASListMode)
            {
                label4.Visible = textBoxAssetFileName.Visible = false;
                labelExamples.Visible = false;
                labelSASListExample.Visible = true;
                labelTitle.Text = this.Text = "Import from SAS Container Path";
            }
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {
           
            bool Error = false;
            try
            {
                Uri myUri = this.GetURL;
            }
            catch
            {
                Error = true;
                labelURLFileNameWarning.Text = "Error detected in the URL";
                buttonImport.Enabled = false;
                return;
            }

            buttonImport.Enabled = true;
            if (!_AzureStorageContainerSASListMode)
            {
                string filename = null;
                try
                {
                    filename = System.IO.Path.GetFileName(this.GetURL.LocalPath);
                }
                catch
                {
                    Error = true;
                    labelURLFileNameWarning.Text = "File name not found in the URL";
                }
                if (!Error)
                {
                    textBoxAssetName.Text = filename;
                    textBoxAssetFileName.Text = filename;
                }
            }

            if (!Error)
            {
                labelURLFileNameWarning.Text = string.Empty;
            }
        }
    }
}
