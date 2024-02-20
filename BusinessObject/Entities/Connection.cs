namespace BusinessObject.Entities
{
	public class Connection
	{
        public Connection()
        {
            
        }

        public Connection(string connectionId, string emailAddress)
        {
            ConnectionId = connectionId;
            EmailAddress = emailAddress;
        }

        public string ConnectionId { get; set; }
        public string EmailAddress { get; set; }
    }
}
