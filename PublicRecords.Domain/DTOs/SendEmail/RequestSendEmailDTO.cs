namespace PublicRecords.Domain.DTOs.SendEmail
{
    public record RequestSendEmailDTO
        (
            string From,
            string Subject,
            Dictionary<string, string> Body
        );
    
}
