namespace Ru1t3rl.ChalkHunter.Data
{
    [System.Serializable]
    public class Message
    {
        public MessageType messageType;
        public float duration;
        public string message;

        public Message(string message, float duration = 5f, MessageType messageType = MessageType.Info)
        {
            this.message = message;
            this.duration = duration;
            this.messageType = messageType;
        }
    }
}
