using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace armipsSimpleGui
{
    class ArmipsUpdater
    {
        public static void Run(bool bSilent)
        {
            SevenZip.SevenZipBase.SetLibraryPath(@"data\7z.dll");

            WebRequest request;
            WebResponse response;
            Stream dataStream;
            StreamReader strReader;

            DialogResult dlgResult;

            if (!bSilent)
            {
                dlgResult = MessageBox.Show("Simple armips GUI will check http://buildbot.orphis.net/armips/ new armips versions.", "Armips update", MessageBoxButtons.OKCancel);
                if (dlgResult != DialogResult.OK)
                {
                    return;
                }
            }

            string armipsPath = @"data\armips.exe";
            string siteURL = "http://buildbot.orphis.net";
            string indexURL = siteURL + "/armips/";

            request = WebRequest.Create(indexURL);
            response = request.GetResponse();
            dataStream = response.GetResponseStream();
            strReader = new StreamReader(dataStream);

            string htmlPage = strReader.ReadToEnd();

            string pattern = @"(https://git.+?)'>"  + // commit url
                              "(.+?)<.+[\r\n]+.+>"  + // build version
                              "(.+?)<.+[\r\n]+.+>"  + // build date
                              "(.+?)<.+[\r\n]+.+?'" + // build url
                              "(.+?)'.+[\r\n]+.+>"  + // commit message
                              "(.+?)<";

            Regex rgx = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection matches = rgx.Matches(htmlPage);

            if (matches.Count == 0)
            {
                MessageBox.Show("A parsing error occurred while checking the build-bot page.");
                return;
            }

            GroupCollection groups = matches[0].Groups;

            string commitURL = groups[1].Value;
            string buildVersion = groups[2].Value;
            string commitAuthor = groups[3].Value;
            string buildDate = groups[4].Value;
            string buildURL = siteURL + groups[5].Value.Replace("&amp;", "&");
            string commitMsg = groups[6].Value;

            // Compare build time against current build's
            DateTime remoteBuildDate = DateTime.Parse(buildDate);
            DateTime localBuildDate;

            bool needUpdate;

            if (File.Exists(armipsPath))
            {
                localBuildDate = File.GetLastWriteTime(armipsPath);
                int dateCompare = DateTime.Compare(remoteBuildDate, localBuildDate);
                needUpdate = (dateCompare < 0);
            }
            else
            {
                needUpdate = true;
            }

            if (!needUpdate)
            {
                if (!bSilent)
                {
                    MessageBox.Show("armips is already up-to-date (Remote: " + buildDate + ").", "armips update");
                }
                return;
            }

            string updatePromptFmt = "A new version of armips is available:\n\n{0}\n{1}\n\n{2} ({3})\n\nWould you like to update now?";
            string updatePrompt = string.Format(updatePromptFmt, buildVersion, buildDate, commitMsg, commitAuthor);

            dlgResult = MessageBox.Show(updatePrompt, "armips update", MessageBoxButtons.OKCancel);

            if (dlgResult != DialogResult.OK)
            {
                return;
            }

            request = WebRequest.Create(buildURL);
            response = request.GetResponse();
            dataStream = response.GetResponseStream();

            MemoryStream memStream = new MemoryStream();

            // Copy to memStream
            byte[] buffer = new byte[1024];
            int nBytesRead;
            while ((nBytesRead = dataStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                memStream.Write(buffer, 0, nBytesRead);
            }

            memStream.Seek(0, SeekOrigin.Begin);

            SevenZip.SevenZipExtractor extractor = new SevenZip.SevenZipExtractor(memStream);
            extractor.ExtractArchive("data");

            memStream.Close();

            MessageBox.Show("armips updated successfully!", "armips update");
        }
    }
}
