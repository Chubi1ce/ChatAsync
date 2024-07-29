using System.Text.Json;

namespace Chat_Threads;

internal class Message
    {
        public string? NickName { get; set; }
        public string? Text { get; set; }
        public DateTime Time { get; set; }

        public Message(string? nickName, string? text, DateTime time)
        {
            this.NickName = nickName;
            this.Text = text;
            this.Time = time;
        }

        public override string ToString()
        {
            return $"{Time} {NickName}: {Text}";
        }

        public string ConvertToJSON()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Message? ConvertFromJSON(string message)
        {
            return JsonSerializer.Deserialize<Message>(message);
        }
    }
