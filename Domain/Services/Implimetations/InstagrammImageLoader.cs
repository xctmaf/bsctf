namespace Domain.Services.Implimetations
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using FileInfo = ValueObject.FileInfo;

    public class InstagrammImageLoader : ImageLoaderBase
    {
        private static readonly Regex ImgInfoRegex = new Regex(@"display_src"": ""(?<link>.+?)""");

        public override bool IsAccept(string url)
        {
            return url.StartsWith(@"https://www.instagram.com/p/") || url.StartsWith(@"http://www.instagram.com/p/");
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

            var match = ImgInfoRegex.Match(html);
            var descript = match.Groups["descript"].Value;
            var imageContent = GetFileContent(match.Groups["link"].Value);

            response.Close();
            readStream.Close();

            return new FileInfo {Content = imageContent, Description = descript, FileName = Guid.NewGuid() + ".jpg"};
        }
    }
}