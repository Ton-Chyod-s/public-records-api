namespace PublicRecords.Domain.DTOs.SaveAndNotify
{
    public record SaveAndNotifyDTO
        (
            string Name,
            string Year,
            string From,
            string Subject,
            Dictionary<string, string> Body
        );
    
}
