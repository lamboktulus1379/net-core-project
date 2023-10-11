namespace TestApplication.Infrastructure.EventBusKafka;

public class EventBusKafka
{
    private readonly string BootstrapServer;
    private readonly string SaslUsername;
    private readonly string SaslPassword;
    
    public EventBusKafka(string bootstrapServer, string saslUsername, string saslPassword)
    {
        BootstrapServer = bootstrapServer;
        SaslUsername = saslUsername;
        SaslPassword = saslPassword;
    }
}