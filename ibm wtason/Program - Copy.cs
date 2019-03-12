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

                ////  open and read an audio file
                //using (FileStream fs = File.OpenRead(audioFile))
                //{
                //    //  get a transcript of the audio file.
                //    var results = _speechToText.Recognize(fs.GetMediaTypeFromFile(), fs);

                //    if (results?.Results[0]?.Alternatives[0]?.Confidence != null)
                //    {
                //        Console.WriteLine($"Confidence : {results.Results[0].Alternatives[0].Confidence} \n Transcript : {results.Results[0].Alternatives[0].Transcript}");
                //    }
                //    else
                //    {
                //        Console.WriteLine("Results not found");
                //    }
                //}


                var fileStream = File.OpenRead(audioFile);
                var stream = StreamToByteArray(fileStream);
                var results =
                    _speechToText.RecognizeSessionless(audio: stream, contentType: fileStream.GetMediaTypeFromFile(), keywords: new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" }, keywordsThreshold: 0.5f, model: "en-US_NarrowbandModel");
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


        //public void WAV_to_FLAC_converter()
        //{
        //    string inputFile = "inputFile.wav";
        //    //string outputFile = Path.Combine("flac", Path.ChangeExtension(input, ".flac"));
        //    string outputFile = "outputFile.flac";

        //    if (!File.Exists(inputFile))
        //        throw new ApplicationException("Input file " + inputFile + " cannot be found!");
        //    var stream = File.OpenRead(@"C:\inputFile.wav");
        //    WaveReader wav = new WaveReader(stream);

        //    using (var flacStream = File.Create(outputFile))
        //    {
        //        FlacWriter flac = new FlacWriter(flacStream, wav.BitDepth, wav.Channels, wav.SampleRate);
        //        // Buffer for 1 second's worth of audio data
        //        byte[] buffer = new byte[wav.Bitrate / 8];
        //        int bytesRead;//**I GET THE ABOVE ERROR HERE.**
        //        do
        //        {
        //            bytesRead = wav.InputStream.Read(buffer, 0, buffer.Length);
        //            flac.Write(buffer, 0, bytesRead);
        //        } while (bytesRead > 0);
        //        flac.Dispose();
        //        flac = null;
        //    }
        //}


        //static void Main(string[] args)
        //{
        //    Transcribe();
        //    Console.WriteLine("Press any key to exit");
        //    Console.ReadLine();
        //}

        //// http://www.ibm.com/smarterplanet/us/en/ibmwatson/developercloud/doc/getting_started/gs-credentials.shtml
        ////static String username = "<username>";
        ////static String password = "<password>";

        //static String file = @"c:\audio.wav";

        //static Uri url = new Uri("wss://stream.watsonplatform.net/speech-to-text/api/v1/recognize");

        //// these should probably be private classes that use DataContractJsonSerializer 
        //// see https://msdn.microsoft.com/en-us/library/bb412179%28v=vs.110%29.aspx
        //// or the ServiceState class at the end
        //static ArraySegment<byte> openingMessage = new ArraySegment<byte>(Encoding.UTF8.GetBytes(
        //    "{\"action\": \"start\", \"content-type\": \"audio/wav\", \"continuous\" : true, \"interim_results\": true}"
        //));
        //static ArraySegment<byte> closingMessage = new ArraySegment<byte>(Encoding.UTF8.GetBytes(
        //    "{\"action\": \"stop\"}"
        //));


        //static void Transcribe()
        //{
        //    var ws = new ClientWebSocket();
        //    /ws.Options.Credentials = new NetworkCredential(new TokenOptions(), );

        //    TokenOptions iamAssistantTokenOptions = new TokenOptions
        //    {
        //        IamApiKey = "5owIqX7QnuEoh8lfKW6w-dE_xIWRSbhpJGwUSoI2ww77",
        //        ServiceUrl = "https://gateway-lon.watsonplatform.net/speech-to-text/api",

        //    };


        //    ws.ConnectAsync(url, CancellationToken.None).Wait();

        //    // send opening message and wait for initial delimeter 
        //    Task.WaitAll(ws.SendAsync(openingMessage, WebSocketMessageType.Text, true, CancellationToken.None), HandleResults(ws));

        //    // send all audio and then a closing message; simltaneously print all results until delimeter is recieved
        //    Task.WaitAll(SendAudio(ws), HandleResults(ws));

        //    // close down the websocket
        //    ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None).Wait();
        //}

        //static async Task SendAudio(ClientWebSocket ws)
        //{

        //    using (FileStream fs = File.OpenRead(file))
        //    {
        //        byte[] b = new byte[1024];
        //        while (fs.Read(b, 0, b.Length) > 0)
        //        {
        //            await ws.SendAsync(new ArraySegment<byte>(b), WebSocketMessageType.Binary, true, CancellationToken.None);
        //        }
        //        await ws.SendAsync(closingMessage, WebSocketMessageType.Text, true, CancellationToken.None);
        //    }
        //}

        //// prints results until the connection closes or a delimeterMessage is recieved
        //static async Task HandleResults(ClientWebSocket ws)
        //{
        //    var buffer = new byte[1024];
        //    while (true)
        //    {
        //        var segment = new ArraySegment<byte>(buffer);

        //        var result = await ws.ReceiveAsync(segment, CancellationToken.None);

        //        if (result.MessageType == WebSocketMessageType.Close)
        //        {
        //            return;
        //        }

        //        int count = result.Count;
        //        while (!result.EndOfMessage)
        //        {
        //            if (count >= buffer.Length)
        //            {
        //                await ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None);
        //                return;
        //            }

        //            segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
        //            result = await ws.ReceiveAsync(segment, CancellationToken.None);
        //            count += result.Count;
        //        }

        //        var message = Encoding.UTF8.GetString(buffer, 0, count);

        //        // you'll probably want to parse the JSON into a useful object here,
        //        // see ServiceState and IsDelimeter for a light-weight example of that.
        //        Console.WriteLine(message);

        //        if (IsDelimeter(message))
        //        {
        //            return;
        //        }
        //    }
        //}


        //// the watson service sends a {"state": "listening"} message at both the beginning and the *end* of the results
        //// this checks for that
        //[DataContract]
        //internal class ServiceState
        //{
        //    [DataMember]
        //    public string state = "";
        //}
        //static bool IsDelimeter(String json)
        //{
        //    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ServiceState));
        //    ServiceState obj = (ServiceState)ser.ReadObject(stream);
        //    return obj.state == "listening";
        //}
    }
}
