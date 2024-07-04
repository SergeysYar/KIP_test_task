using Microsoft.EntityFrameworkCore;

namespace KIP_test_task
{
    public class UserStatisticsRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestTime { get; set; }
        public bool IsCompleted { get; set; }
        public string? Result { get; set; }
    }

    public class UserStatisticsDbContext : DbContext
    {
        public UserStatisticsDbContext(DbContextOptions<UserStatisticsDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserStatisticsRequest> UserStatisticsRequests { get; set; }
    }
}
