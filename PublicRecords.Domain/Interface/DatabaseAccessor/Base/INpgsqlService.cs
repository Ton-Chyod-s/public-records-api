using Npgsql;

namespace PublicRecords.Domain.Interface.DatabaseAccessor.Base
{
    public interface INpgsqlService
    {
        Task<NpgsqlDataReader> ExecuteCommandAndReaderAsync(string query);
    }
}
