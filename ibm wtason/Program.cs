using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Util;
using IBM.WatsonDeveloperCloud.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace ibm_wtason
{
    /// <summary>
    /// https://stackoverflow.com/questions/46179447/watson-speech-to-text-live-stream-c-sharp-code-example
    /// https://gist.github.com/nfriedly/0240e862901474a9447a600e5795d500
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("IBM watson sample");

                TokenOptions iamAssistantTokenOptions = new TokenOptions
                {
                    IamApiKey = "5owIqX7QnuEoh8lfKW6w-dE_xIWRSbhpJGwUSoI2ww77",
                    ServiceUrl = "https://gateway-lon.watsonplatform.net/speech-to-text/api",

                };

                SpeechToTextService _speechToText = new SpeechToTextService(iamAssistantTokenOptions);



                //var audioFile = @"D:\Projects_Mine\ibm wtason\ibm wtason\audio\audio-file.flac";
                //var audioFile = @"D:\Projects_Mine\ibm wtason\ibm wtason\audio\sample.flac";
                //var audioFile = @"D:\Projects_Mine\ibm wtason\ibm wtason\audio\sample.wav";
                //var audioFile = @"D:\Projects_Mine\ibm wtason\ibm wtason\audio\oneTwoThree.wav";
                //var audioFile = @"D:\Projects_Mine\ibm wtason\ibm wtason\audio\speaker1Enroll.wav";
                var audioFile = @"D:\Projects_Mine\ibm wtason\ibm wtason\audio\speakerVerification1.wav";
                //var audioFile =
                //    "https://api.twilio.com/2010-04-01/Accounts/ACdfe342ddbecf65b044a7e098180a75e3/Recordings/REe46d1db388878b920507837a656d4e02.wav";

                
                var fileStream = File.OpenRead(audioFile);
                var stream = StreamToByteArray(fileStream);
                //var results =
                //    _speechToText.RecognizeSessionless(audio: stream, contentType: fileStream.GetMediaTypeFromFile(), keywords:new List<string> {"A", "B", "C", "D","E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"}, keywordsThreshold: 0.5f, model: "en-US_NarrowbandModel");

                var streamFromUrl = GetStreamFromUrl("https://api.twilio.com/2010-04-01/Accounts/ACdfe342ddbecf65b044a7e098180a75e3/Recordings/REe46d1db388878b920507837a656d4e02.wav");
                var results =
                    _speechToText.Recognize(audio: streamFromUrl, contentType: fileStream.GetMediaTypeFromFile(), keywords: new string [] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" }, keywordsThreshold: 0.5f, model: "en-US_NarrowbandModel");


                if (results?.Results[0]?.Alternatives[0]?.Confidence != null)
                {
                    Console.WriteLine($"Confidence : {results.Results[0].Alternatives[0].Confidence} \n Transcript : {results.Results[0].Alternatives[0].Transcript}");
                }
                else
                {
                    Console.WriteLine("Results not found");
                }

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static Stream GetStreamFromUrl(string url)
        {
            byte[] imageData = null;

            using (var wc = new System.Net.WebClient())
                imageData = wc.DownloadData(url);

            return new MemoryStream(imageData);
        }


        public static byte[] StreamToByteArray(Stream stream)
        {
            if (stream is MemoryStream)
            {
                return ((MemoryStream)stream).ToArray();
            }
            else
            {
                // Jon Skeet's accepted answer 
                return ReadFully(stream);
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
