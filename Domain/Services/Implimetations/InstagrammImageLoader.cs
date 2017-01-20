namespace Domain.Services.Implimetations
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using FileInfo = ValueObject.FileInfo;

    public class InstagrammImageLoader : ImageLoaderBase
    {
        private static readonly Regex ImgLinkRegex = new Regex(@"display_src"": ""(?<link>.+?)""");
        private static readonly Regex ImgCaptionRegex = new Regex(@"caption"": ""(?<descript>.*?)""");

        public override bool IsAccept(string url)
        {
            return url.StartsWith(@"https://www.instagram.com/p/");
        }

        public override async Task<FileInfo> GetFile(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)await request.GetResponseAsync();

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var receiveStream = response.GetResponseStream();
            StreamReader readStream = null;
            readStream = response.CharacterSet == null
                             ? new StreamReader(receiveStream)
                             : new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

            var html = await readStream.ReadToEndAsync();

            var descriptMatch = ImgCaptionRegex.Match(html);
            var descript = descriptMatch.Success ? DecodeUtf(descriptMatch.Groups["descript"].Value) : string.Empty;
            var imageContent = GetFileContent(ImgLinkRegex.Match(html).Groups["link"].Value);

            response.Close();
            readStream.Close();

            return new FileInfo {Content = imageContent, Description = descript, FileName = Guid.NewGuid() + ".jpg"};
        }

        private static string DecodeUtf(string value)
        {
            return Regex.Replace(value,
                                 @"\\u(?<Value>[a-zA-Z0-9]{4})",
                                 m => ((char) int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString());
        }
    }
}